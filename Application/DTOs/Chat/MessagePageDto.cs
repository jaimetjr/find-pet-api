using Domain.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Chat
{
    public class MessagePageDto
    {
        public IReadOnlyList<ChatMessage> Items { get; init; }
        public string? NextBefore { get; init; }
        public bool HasMore { get; init; }
    }
}
