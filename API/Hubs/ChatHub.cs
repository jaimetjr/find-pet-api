using Application.DTOs.Chat;
using Application.Interfaces.Services;
using Application.Helpers;
using Domain.Entities;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Collections.Concurrent;

namespace API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        private readonly IPushService _pushService;
        private readonly IUserService _userService;
        private readonly ConcurrentDictionary<string, string> OnlineUsers = new();

        public ChatHub(IChatService chatService, IPushService pushService, IUserService userService)
        {
            _chatService = chatService;
            _pushService = pushService;
            _userService = userService;
        }

        public async Task<ChatRoomDto> JoinPrivateChat(string userAId, string userBId, Guid petId)
        {
            var room = await _chatService.GetOrCreatePrivateChatAsync(userAId, userBId, petId);
            return new ChatRoomDto(room);
        }

        public async Task SendMessage(Guid chatRoomId, string senderId, string recipientId, string content)
        {
            var message = await _chatService.SendMessageAsync(chatRoomId, senderId, recipientId, content);

            var chatMessage = new ChatMessageDto(message);

            var chatRoom = await _chatService.GetChat(chatRoomId);
            // Broadcast to all users in the room
            await Clients.Group(chatRoomId.ToString())
                .SendAsync("ReceiveMessage", chatMessage);
            await Clients.Group(chatRoomId.ToString()).SendAsync("NewMessage", chatRoom.Value);

            if (!IsUserOnline(recipientId))
            {
                var expoPushToken = await _userService.GetExpoPushTokenAsync(recipientId);
                if (!string.IsNullOrEmpty(expoPushToken.Value))
                {
                    await _pushService.SendNotificationAsync(expoPushToken.Value, "Nova mensagem", 
                                                             content.Substring(0, 10));
                }
            }
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

        public async Task MarkMessagesAsSeen(Guid chatRoomId, string viewerClerkId)
        {
            var seenMessages = await _chatService.MarkMessageAsSeenAsync(chatRoomId, viewerClerkId);

            if (seenMessages.Value == null)
                return;

            if (seenMessages.Value.Any())
            {
                await Clients.Group(chatRoomId.ToString())
                    .SendAsync("MessagesMarkedAsSeen", chatRoomId, seenMessages.Value, viewerClerkId);
            }
        }

        public async Task JoinAllUserRooms(string userClerkId)
        {
            var rooms = await _chatService.GetChats(userClerkId);

            if (rooms.Value == null || !rooms.Value.Any())
                return;

            foreach (var room in rooms.Value)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, room.Id.ToString());
            }
        }

        public async Task AcknowledgeDelivery(Guid messageId, string recipientId)
        {
            var deliveredMessage = await _chatService.MarkMessageAsDeliveredAsync(messageId, recipientId);
            if (deliveredMessage.Value == null)
                return;
            
            await Clients.Group(deliveredMessage.Value.ChatRoomId.ToString())
                .SendAsync("MessageDelivered", messageId, recipientId, deliveredMessage.Value);
        }

        public async Task MarkAllMessagesAsDelivered(string recipientId)
        {
            var rooms = await _chatService.GetChats(recipientId);
            if (rooms.Value == null) return;

            foreach (var room in rooms.Value)
            {
                var undelivered = await _chatService.GetUndeliveredMessagesAsync(room.Id, recipientId);

                foreach (var message in undelivered)
                {
                    await _chatService.MarkMessageAsDeliveredAsync(message.Id, recipientId);
                    await Clients.Group(message.ChatRoomId.ToString())
                                 .SendAsync("MessageDelivered", message.Id, recipientId, message);
                }
            }
        }

        public override Task OnConnectedAsync()
        {
            var clerkId = GetCurrentUserId();
            if (!string.IsNullOrEmpty(clerkId))
                OnlineUsers[clerkId] = Context.ConnectionId;
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var clerkId = GetCurrentUserId();
            if (!string.IsNullOrEmpty(clerkId))
                OnlineUsers.TryRemove(clerkId, out _);
            return base.OnDisconnectedAsync(exception);
        }

        public bool IsUserOnline(string clerkId)
        {
            return OnlineUsers.ContainsKey(clerkId);
        }

        private string? GetCurrentUserId()
        {
            if (Context.User == null)
                return null;
            return Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
