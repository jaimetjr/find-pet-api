using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class NotificationRepository(AppDataContext context) : Repository<Notification>(context), INotificationRepository
{

    /// <summary>
    /// Returns a paginated list of notifications for the authenticated user, ordered by most recent first.
    /// </summary>
    /// <param name="clerkId"></param>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Notification>> GetNotificationsPaged(string clerkId, int? page = 0, int? limit = 0)
    {
        var notifications = _context.Notifications
            .Where(n => n.UserClerkId == clerkId)
            .Skip(page ?? 0)
            .Take(limit ?? 0)
            .OrderByDescending(n => n.CreatedAt);
        return await notifications.ToListAsync();
    }

    /// <summary>
    /// Returns the count of unread notifications for the authenticated user. Used to display the badge on the notification bell icon.
    /// </summary>
    /// <param name="clerkId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Notification>> GetUnreadNotifications(string clerkId)
    {
        var notifications = await _context.Notifications
            .Where(n => n.UserClerkId == clerkId && !n.IsRead)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
        return notifications;
    }

    /// <summary>
    /// Marks all notifications as read for the authenticated user. Used when user taps "Mark all as read" button.
    /// </summary>
    /// <param name="clerkId"></param>
    /// <returns></returns>
    public async Task MarkAllNotificationAsRead(string clerkId)
    {
        await _context.Notifications
                      .Where(n => n.UserClerkId == clerkId && !n.IsRead)
                      .ExecuteUpdateAsync(setters => setters
                          .SetProperty(n => n.IsRead, true)
                          .SetProperty(n => n.UpdatedAt, DateTime.UtcNow));
    }

    /// <summary>
    /// Marks a specific notification as read for the authenticated user.
    /// </summary>
    /// <param name="clerkId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task MarkNotificationAsRead(string clerkId, Guid id)
    {
        var notification = await _context.Notifications
            .FirstOrDefaultAsync(n => n.UserClerkId == clerkId && n.Id == id);
        if (notification is null)
            return;

        notification.MarkAsRead();
        await _context.SaveChangesAsync();
    }
}
