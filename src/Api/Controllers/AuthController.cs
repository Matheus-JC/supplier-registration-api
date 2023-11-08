using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Api.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SupplierRegServer.Api.DTOs;
using SupplierRegServer.Api.Extensions;
using SupplierRegServer.Business.Interfaces;

namespace SupplierRegServer.Api.Controllers;

[Route("api/account")]
public class AuthController : MainController
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtSettings _jwtSettings;

    public AuthController(INotifier notifier, SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager, IOptions<JwtSettings> jwtSettings) : base(notifier)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterUserDTO registerUser)
    {
        if (!ModelState.IsValid) return HandleResponse(ModelState);

        var user = new IdentityUser()
        {
            UserName = registerUser.Email,
            Email = registerUser.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, registerUser.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return HandleResponse(HttpStatusCode.OK, await GenerateJwt(user.Email));
        }

        foreach (var error in result.Errors)
        {
            NotifyError(error.Description);
        }

        return HandleResponse(HttpStatusCode.OK, registerUser);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginUserDTO loginUser)
    {
        if (!ModelState.IsValid) return HandleResponse(ModelState);

        var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

        if (result.Succeeded)
        {
            return HandleResponse(HttpStatusCode.OK, await GenerateJwt(loginUser.Email));
        }

        if (result.IsLockedOut)
        {
            NotifyError("user temporarily blocked due to invalid attempts");
        }
        else
        {
            NotifyError("username or password is invalid");
        }

        return HandleResponse(HttpStatusCode.OK, loginUser);
    }

    private async Task<LoginResponseDTO?> GenerateJwt(string email)
    {
        LoginResponseDTO? response = null;

        var user = await _userManager.FindByEmailAsync(email);

        if (user != null)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            if (user.Email != null)
                claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            response = new LoginResponseDTO
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_jwtSettings.ExpirationHours).TotalSeconds,
                User = new UserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new ClaimDTO { Value = c.Value, Type = c.Type })
                }
            };
        }

        return response;
    }

    private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
}
