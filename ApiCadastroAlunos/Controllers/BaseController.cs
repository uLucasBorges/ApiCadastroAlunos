using _2___CadastroAlunos.Domain.Notification;
using Microsoft.AspNetCore.Mvc;

namespace ApiCadastroAlunos.Controllers
{
    public class BaseController : ControllerBase
{
    protected readonly INotificationContext _notificationContext;
    protected BaseController(INotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }

    protected bool IsValidOperation()
    {
        return !_notificationContext.HasNotifications();
    }

    protected new IActionResult Response(object result = null)
    {
        if (IsValidOperation())
        {
            return Ok(result);
        }

        var notifications = _notificationContext.GetNotifications();

        return StatusCode(_notificationContext.Code, notifications);
    }
}
}
