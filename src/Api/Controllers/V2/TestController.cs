using Microsoft.AspNetCore.Mvc;
using SupplierRegServer.Business.Interfaces;

namespace SupplierRegServer.Api.Controllers.V2;

[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/test")]
public class TestController : MainController
{
    private readonly ILogger _logger;
    public TestController(INotifier notifier, ILogger<TestController> logger) : base(notifier)
    {
        _logger = logger;
    }

    [HttpGet]
    public string GetVersion()
    {
        return "API Version 2";
    }

    [HttpGet("logger")]
    public string TestLogger()
    {
        _logger.LogTrace("Log Trace");
        _logger.LogDebug("Log Debug");
        _logger.LogInformation("Log Information");
        _logger.LogWarning("Log Warning");
        _logger.LogError("Log Error");
        _logger.LogCritical("Log Critical");

        return "Generated logs";
    }
}
