using Domain.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IChatRepository
    {
        Task<ChatRoom?> GetPrivateRoomAsync(string userAClerkId, string userBClerkId);
        Task<ChatRoom> CreatePrivateRoomAsync(string userAClerkId, string userBClerkId, Guid petId);
        Task AddMessageAsync(ChatMessage message);
        Task<List<ChatMessage>> GetMessagesAsync(Guid chatRoomId, int page, int pageSize);
        Task SaveChangesAsync();
        Task<List<ChatRoom>> GetChats(string clerkId);
        Task<ChatRoom> GetChat(Guid chatId);
        Task<List<ChatMessage>> MarkMessageAsSeenAsync(Guid chatRoomId, string viewerClerkId);
        Task<ChatMessage?> MarkMessageAsDeliveredAsync(Guid messageId, string recipientClerkId);
        Task<List<ChatMessage>> GetUndeliveredMessagesAsync(Guid roomId, string recipientId);
    }
}
