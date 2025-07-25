using Domain.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Chat
{
    public class ChatMessageDto
    {
        public Guid Id { get; set; }
        public Guid ChatRoomId { get; set; }
        public ChatRoomDto ChatRoom { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool WasSeen { get; set; }
        public DateTime? WasSeenAt { get; set; }
        public string SeenByClerkId { get; set; }
        public bool WasDelivered { get; set; }
        public DateTime? WasDeliveredAt { get; set; }
        public string RecipientId { get; set; }

        public ChatMessageDto(ChatMessage entity)
        {
            Id = entity.Id;
            ChatRoomId = entity.ChatRoomId;
            SenderId = entity.SenderId;
            SenderName = entity.Sender?.Name ?? "Unknown";
            Content = entity.Content;
            SentAt = entity.SentAt;
            WasSeen = entity.WasSeen;
            WasSeenAt = entity.WasSeenAt;
            SeenByClerkId = entity.SeenByClerkId;
            WasDelivered = entity.WasDelivered;
            WasDeliveredAt = entity.WasDeliveredAt;
            SenderId = entity.SenderId;
            RecipientId = entity.RecipientId;

            if (entity.ChatRoom != null)    
                ChatRoom = new ChatRoomDto
                {
                    UserAClerkId = entity.ChatRoom.UserAClerkId,
                    UserBClerkId = entity.ChatRoom.UserBClerkId,
                };
        }
    }
}
