using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Chat
{
    public sealed class ChatRoom : Entity
    {
        public string UserAClerkId { get; private set; }
        public User UserA { get; private set; }
        public string UserBClerkId { get; private set; }
        public User UserB { get; private set; }
        public Guid PetId { get; private set; }
        public Pet Pet { get; private set; }
        public ICollection<ChatMessage> Messages { get; private set; }
        public ChatRoom() { }

        public ChatRoom(string userAClerkId, string userBClerkId, Guid petId)
        {
            Id = Guid.NewGuid();
            UserAClerkId = userAClerkId;
            UserBClerkId = userBClerkId;
            PetId = petId;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Messages = new List<ChatMessage>();
        }
    }
}
