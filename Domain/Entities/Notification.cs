using Domain.Enums;

namespace Domain.Entities;

public sealed class Notification : Entity
{
    public string UserClerkId { get; set; }
    public User User { get; set; }
    public NotificationType Type { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public string RelatedEntityId { get; set; } // AdoptionRequest ID, Pet ID, etc.
    public string ActionUrl { get; set; } // Deep link URL
    public bool IsRead { get; set; }

    public Notification()
    {
        
    }

    public Notification(string userClerkId, NotificationType type, string title, string message, string relatedEntityId, string actionUrl)
    {
        UserClerkId = userClerkId;
        Type = type;
        Title = title;
        Message = message;
        RelatedEntityId = relatedEntityId;
        ActionUrl = actionUrl;
        IsRead = false;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsRead()
    {
        IsRead = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
