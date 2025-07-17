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
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }

        public ChatMessageDto(ChatMessage entity)
        {
            Id = entity.Id;
            ChatRoomId = entity.ChatRoomId;
            SenderId = entity.SenderId;
            SenderName = entity.Sender?.Name ?? "Unknown";
            Content = entity.Content;
            SentAt = entity.SentAt;
        }
    }
}
