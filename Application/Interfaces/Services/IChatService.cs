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
        Task<ChatMessage> SendMessageAsync(Guid chatRoomId, string senderId, string content);
        Task<List<ChatMessage>> GetMessagesAsync(Guid chatRoomId, int page = 1, int pageSize = 50);
        Task<Result<List<ChatRoomDto>>> GetChats(string clerkId);
    }
}
