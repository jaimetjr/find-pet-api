using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Chat
{
    public sealed class ChatMessage
    {
        public Guid Id { get; private set; }
        public Guid ChatRoomId { get; private set; }
        public ChatRoom ChatRoom { get; private set; }

        public string SenderId { get; private set; }
        public User Sender { get; private set; }
        public string Content { get; private set; }
        public DateTime SentAt { get; private set; }

        public ChatMessage() { }

        public ChatMessage(Guid chatRoomId, string senderId, string content)
        {
            ChatRoomId = chatRoomId;
            SenderId = senderId;
            Content = content;
            SentAt = DateTime.UtcNow;
        }
    }
}
