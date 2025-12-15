using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface INotificationRepository : IRepository<Notification>
{
    Task<IEnumerable<Notification>> GetNotificationsPaged(string clerkId, int? page = 0, int? limit = 0, CancellationToken ct = default);
    Task<IEnumerable<Notification>> GetUnreadNotifications(string clerkId, CancellationToken ct = default);
    Task MarkNotificationAsRead(string clerkId, Guid id, CancellationToken ct = default);
    Task MarkAllNotificationAsRead(string clerkId, CancellationToken ct = default);
}

