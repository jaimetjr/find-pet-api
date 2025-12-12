using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Notification;

public class NotificationDto
{
    public string UserClerkId { get; set; } = default!;
    public NotificationType Type { get; set; }
    public string Title { get; set; } = default!;
    public string Message { get; set; } = default!;
    public string RelatedEntityId { get; set; } = default!;
    public string ActionUrl { get; set; } = default!;
    public bool IsRead { get; set; }
}
