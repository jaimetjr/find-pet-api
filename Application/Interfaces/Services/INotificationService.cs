using Application.DTOs.Notification;
using Application.Helpers;

namespace Application.Interfaces.Services;

public interface INotificationService
{
    Task<Result<IEnumerable<NotificationDto>>> GetNotificationsPaged(string clerkId, int? page = 0, int? limit = 0);
    Task<Result<IEnumerable<NotificationDto>>> GetUnreadNotifications(string clerkId);
    Task<Result<string>> MarkNotificationAsRead(string clerkId, Guid id);
    Task<Result<string>> MarkAllNotificationAsRead(string clerkId);
}
