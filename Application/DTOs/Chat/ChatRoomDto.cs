using Application.DTOs.Pet;
using Application.DTOs.User;
using Domain.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Chat
{
    public class ChatRoomDto
    {
        public Guid Id { get; set; }
        public string UserAClerkId { get; set; }
        public string UserBClerkId { get; set; }
        public UserDto UserA { get; set; }
        public UserDto UserB { get; set; }
        public Guid PetId;
        public PetDto Pet { get; set; }
        public List<ChatMessageDto> Messages { get; set; } = new List<ChatMessageDto>();

        public ChatRoomDto(ChatRoom entity)
        {
            Id = entity.Id;
            UserAClerkId = entity.UserAClerkId;
            UserA = entity.UserA != null ? new UserDto(entity.UserA) : null;
            UserBClerkId = entity.UserBClerkId; 
            UserB = entity.UserB != null ? new UserDto(entity.UserB) : null;
            PetId = entity.PetId;
            Pet = entity.Pet != null ? new PetDto(entity.Pet) : null;
            Messages = entity.Messages?.OrderBy(m => m.SentAt).Select(m => new ChatMessageDto(m)).ToList() ?? new List<ChatMessageDto>();
        }
    }
}
