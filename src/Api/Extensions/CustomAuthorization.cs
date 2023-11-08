namespace SupplierRegServer.Api.Extensions;

public class CustomAuthorization
{
    public static bool ValidateUserClaims(HttpContext context, string claimName, string claimValue)
    {
        var isValid = false;

        if (context.User.Identity != null)
        {
            isValid = context.User.Identity.IsAuthenticated &&
                    context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }

        return isValid;
    }

}
