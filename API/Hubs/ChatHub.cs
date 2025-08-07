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
        private static readonly ConcurrentDictionary<string, string> OnlineUsers = new();
        private static readonly ConcurrentDictionary<string, DateTime> LastSeen = new();

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
                    try
                    {
                        string avatar = "";
                        if (chatRoom.Value != null)
                        {
                            if (chatRoom.Value.UserA != null && chatRoom.Value.UserA.ClerkId == recipientId && !string.IsNullOrEmpty(chatRoom.Value.UserA.Avatar))
                                avatar = chatRoom.Value.UserA.Avatar;
                            else if (chatRoom.Value.UserB != null && chatRoom.Value.UserB.ClerkId == recipientId && !string.IsNullOrEmpty(chatRoom.Value.UserB.Avatar))
                                avatar = chatRoom.Value.UserB.Avatar;

                        }

                        await _pushService.SendNotificationAsync(expoPushToken.Value, "Nova mensagem", 
                                                                 content, new {
                                screen = "chat",
                                userId = senderId,
                                userName = chatMessage.SenderName,
                                userAvatar = avatar,
                                petId = chatRoom.Value?.PetId,
                        });
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
            }
        }

        public async Task SendPetNotification(string clerkId, Guid petId)
        {
            var tokens = await _userService.GetExpoTokenWithoutMe(clerkId);
            if (tokens.Value == null || !tokens.Value.Any())
                return;
            foreach (var user in tokens.Value)
            {
                try
                {
                    await _pushService.SendNotificationAsync(user.ExpoPushToken, 
                                                            "Novo Pet chegando!!!", 
                                                            "Novidade boa, há um novo pet que está esperando um lar!", 
                                                            new 
                                                            { 
                                                                screen = "pet-detail",
                                                                petId 
                                                            });
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task OnlineStatus(Guid chatRoomId, string recipientId)
        {
            var userOnline = IsUserOnline(recipientId);
            var lastSeen = GetLastSeen(recipientId);
            await Clients.Group(chatRoomId.ToString())
                .SendAsync("UserOffline", userOnline, lastSeen);
            
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
            {
                OnlineUsers[clerkId] = Context.ConnectionId;
                LastSeen.TryRemove(clerkId, out _);
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var clerkId = GetCurrentUserId();
            if (!string.IsNullOrEmpty(clerkId))
            {
                OnlineUsers.TryRemove(clerkId, out _);
                LastSeen[clerkId] = DateTime.UtcNow;
            }
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

        private DateTime? GetLastSeen(string clerkId)
        {
            if (LastSeen.TryGetValue(clerkId, out var lastSeen))
                return lastSeen;
            return null;
        }
    }
}
