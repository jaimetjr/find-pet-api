using Domain.Entities.Chat;

namespace Domain.Interfaces.Repositories;

public interface IChatRepository
{
    Task<ChatRoom?> GetPrivateRoomAsync(string userAClerkId, string userBClerkId, Guid petId, CancellationToken ct = default);
    Task<ChatRoom> CreatePrivateRoomAsync(string userAClerkId, string userBClerkId, Guid petId, CancellationToken ct = default);
    Task AddMessageAsync(ChatMessage message, CancellationToken ct = default);
    Task<List<ChatMessage>> GetMessagesAsync(Guid chatRoomId, int page, int pageSize, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
    Task<List<ChatRoom>> GetChats(string clerkId, CancellationToken ct = default);
    Task<ChatRoom> GetChat(Guid chatId, CancellationToken ct = default);
    Task<List<ChatMessage>> MarkMessageAsSeenAsync(Guid chatRoomId, string viewerClerkId, CancellationToken ct = default);
    Task<ChatMessage?> MarkMessageAsDeliveredAsync(Guid messageId, string recipientClerkId, CancellationToken ct = default);
    Task<List<ChatMessage>> GetUndeliveredMessagesAsync(Guid roomId, string recipientId, CancellationToken ct = default);
}

