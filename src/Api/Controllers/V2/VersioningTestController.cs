using Microsoft.AspNetCore.Mvc;
using SupplierRegServer.Business.Interfaces;

namespace SupplierRegServer.Api.Controllers.V2;

[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/suppliers")]
public class VersioningTestController : MainController
{
    public VersioningTestController(INotifier notifier) : base(notifier) { }

    [HttpGet]
    public string Value()
    {
        return "Version 2";
    }
}
