using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace SupplierRegServer.Api.Extensions;

public class ClaimsAuthorizeAttribute : TypeFilterAttribute
{
    public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequirementClaimFilter))
    {
        Arguments = new object[] { new Claim(claimName, claimValue) };
    }
}
