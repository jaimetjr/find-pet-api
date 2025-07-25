using Application.DTOs.Chat;
using Application.Helpers;
using Domain.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IChatService
    {
        Task<ChatRoom> GetOrCreatePrivateChatAsync(string userAClerkId, string userBClerkId, Guid petId);
        Task<ChatMessage> SendMessageAsync(Guid chatRoomId, string senderId, string recipientId, string content);
        Task<List<ChatMessage>> GetMessagesAsync(Guid chatRoomId, int page = 1, int pageSize = 50);
        Task<Result<List<ChatRoomDto>>> GetChats(string clerkId);
        Task<Result<ChatRoomDto>> GetChat(Guid chatRoomId);
        Task<Result<List<ChatMessageDto>>> MarkMessageAsSeenAsync(Guid chatRoomId, string viewerClerkId);
        Task<Result<ChatMessageDto>> MarkMessageAsDeliveredAsync(Guid messageId, string recipientClerkId);
        Task<List<ChatMessageDto>> GetUndeliveredMessagesAsync(Guid roomId, string recipientId);
    }
}
