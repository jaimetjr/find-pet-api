using Application.DTOs.Chat;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<ChatRoomDto> JoinPrivateChat(string userAId, string userBId, Guid petId)
        {
            var room = await _chatService.GetOrCreatePrivateChatAsync(userAId, userBId, petId);
            return new ChatRoomDto(room);
        }

        public async Task SendMessage(Guid chatRoomId, string senderId, string content)
        {
            var message = await _chatService.SendMessageAsync(chatRoomId, senderId, content);

            // Broadcast to all users in the room
            await Clients.Group(chatRoomId.ToString())
                .SendAsync("ReceiveMessage", new ChatMessageDto(message));
        }

        public async Task JoinRoomGroup(Guid chatRoomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId.ToString());
        }

        public async Task<List<ChatMessageDto>> GetMessages(Guid chatRoomId, int page = 1, int pageSize = 50)
        {
            var messages = await _chatService.GetMessagesAsync(chatRoomId, page, pageSize);
            return messages.Select(m => new ChatMessageDto(m)).ToList();
        }
    }
}
