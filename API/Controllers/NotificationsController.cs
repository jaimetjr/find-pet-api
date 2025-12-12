using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController : BaseController
{
    private readonly INotificationService _notificationService;
    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetNotificationsPaged([FromQuery] int? page = 0, [FromQuery] int? limit = 0)
    {
        var userId = GetCurrentUserId();
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
        var result = await _notificationService.GetNotificationsPaged(userId, page, limit);
        return HandleResult(result);
    }

    [HttpGet("unread-count")]
    public async Task<IActionResult> GetUnreadNotifications()
    {
        var userId = GetCurrentUserId();
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
        var result = await _notificationService.GetUnreadNotifications(userId);
        return HandleResult(result);
    }

    [HttpPatch("{id}/read")]
    public async Task<IActionResult> MarkNotificationAsRead(Guid id)
    {
        var userId = GetCurrentUserId();
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
        var result = await _notificationService.MarkNotificationAsRead(userId, id);
        return HandleResult(result);
    }

    [HttpPatch("read-all")]
    public async Task<IActionResult> MarkAllNotificationAsRead()
    {
        var userId = GetCurrentUserId();
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
        var result = await _notificationService.MarkAllNotificationAsRead(userId);
        return HandleResult(result);
    }
}
