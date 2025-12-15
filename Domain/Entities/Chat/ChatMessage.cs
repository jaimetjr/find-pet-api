namespace Domain.Entities.Chat;

public sealed class ChatMessage
{
    public Guid Id { get; private set; }
    public Guid ChatRoomId { get; private set; }
    public ChatRoom ChatRoom { get; private set; }

    public string SenderId { get; private set; }
    public User Sender { get; private set; }

    public string RecipientId { get; private set; }
    public User Recipient { get; private set; }
    public string Content { get; private set; }
    public DateTime SentAt { get; private set; }

    public bool WasSeen { get; private set; }
    public DateTime? WasSeenAt { get; private set; }
    public string? SeenByClerkId { get; private set; }

    public bool WasDelivered { get; private set; }
    public DateTime? WasDeliveredAt { get; private set; }

    public ChatMessage() { }

    public ChatMessage(Guid chatRoomId, string senderId, string recipientId, string content)
    {
        ChatRoomId = chatRoomId;
        SenderId = senderId;
        Content = content;
        RecipientId = recipientId;
        SentAt = DateTime.UtcNow;

    }

    public void MarkAsSeen(string seenClerkId)
    {
        WasSeen = true;
        WasSeenAt = DateTime.UtcNow;
        SeenByClerkId = seenClerkId;
    }

    public void MarkAsDelivered(string recipientId)
    {
        RecipientId = recipientId;
        WasDelivered = true;
        WasDeliveredAt = DateTime.UtcNow;
    }
}
