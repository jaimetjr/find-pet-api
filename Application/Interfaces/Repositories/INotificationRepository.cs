using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface INotificationRepository : IRepository<Notification>
{
    Task<IEnumerable<Notification>> GetNotificationsPaged(string clerkId, int? page = 0, int? limit = 0);
    Task<IEnumerable<Notification>> GetUnreadNotifications(string clerkId);
    Task MarkNotificationAsRead(string clerkId, Guid id);
    Task MarkAllNotificationAsRead(string clerkId);

}
