using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SupplierRegServer.Business.Interfaces;
using SupplierRegServer.Business.Notifications;

namespace SupplierRegServer.Api.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    private readonly INotifier _notifier;

    protected MainController(INotifier notifier)
    {
        _notifier = notifier;
    }

    protected bool IsOperationValid()
    {
        return !_notifier.HasNotification();
    }

    protected ActionResult HandleResponse(HttpStatusCode statusCode = HttpStatusCode.OK, object? result = null)
    {
        if (IsOperationValid())
        {
            return new ObjectResult(result)
            {
                StatusCode = Convert.ToInt32(statusCode)
            };
        }

        return BadRequest(new
        {
            errors = _notifier.GetNotifications().Select(n => n.Message)
        });
    }

    protected ActionResult HandleResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) NotifyErrorModelStateInvalid(modelState);
        return HandleResponse();
    }

    protected void NotifyErrorModelStateInvalid(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);
        foreach (var error in errors)
        {
            var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
            NotifyError(errorMessage);
        }
    }

    protected void NotifyError(string errorMessage)
    {
        _notifier.Handle(new Notification(errorMessage));
    }
}
