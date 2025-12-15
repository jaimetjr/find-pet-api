using Domain.Interfaces.Repositories;
using Domain.Entities.Chat;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDataContext _context;

        public ChatRepository(AppDataContext context)
        {
            _context = context;
        }

        public async Task<ChatRoom?> GetPrivateRoomAsync(string userAClerkId, string userBClerkId, Guid petId, CancellationToken ct = default)
        {
            return await _context.ChatRooms
                .Include(r => r.Messages)
                .FirstOrDefaultAsync(r => r.UserAClerkId == userAClerkId && r.UserBClerkId == userBClerkId
                                       && r.PetId == petId, ct);
        }

        public async Task<ChatRoom> CreatePrivateRoomAsync(string userAClerkId, string userBClerkId, Guid petId, CancellationToken ct = default)
        {
            var room = new ChatRoom(userAClerkId, userBClerkId, petId);
            await _context.ChatRooms.AddAsync(room, ct);
            return room;
        }

        public async Task AddMessageAsync(ChatMessage message, CancellationToken ct = default)
        {
            await _context.ChatMessages.AddAsync(message, ct);
        }

        public async Task<List<ChatMessage>> GetMessagesAsync(Guid chatRoomId, int page, int pageSize, CancellationToken ct = default)
        {
            return await _context.ChatMessages
                .Include(x => x.ChatRoom)
                .Where(m => m.ChatRoomId == chatRoomId)
                .OrderBy(m => m.SentAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }

        public async Task<List<ChatRoom>> GetChats(string clerkId, CancellationToken ct = default)
        {
            return await _context.ChatRooms.Where(x => x.UserAClerkId == clerkId || x.UserBClerkId == clerkId)
                .Include(r => r.Messages)
                .Include(x => x.UserA)
                .Include(x => x.UserB)
                .Include(x => x.Pet)
                .Include(x => x.Pet.PetImages)
                .ToListAsync(ct);
        }

        public async Task<ChatRoom> GetChat(Guid chatId, CancellationToken ct = default)
        {
            return await _context.ChatRooms
                .Include(r => r.Messages)
                .Include(x => x.UserA)
                .Include(x => x.UserB)
                .Include(x => x.Pet)
                .Include(x => x.Pet.PetImages)
                .FirstOrDefaultAsync(r => r.Id == chatId, ct);
        }

        public async Task<ChatMessage?> MarkMessageAsDeliveredAsync(Guid messageId, string recipientClerkId, CancellationToken ct = default)
        {
            var message = await _context.ChatMessages
                .FirstOrDefaultAsync(m => m.Id == messageId && m.RecipientId == recipientClerkId, ct);

            if (message != null && !message.WasDelivered)
            {
                message.MarkAsDelivered(recipientClerkId);
                await _context.SaveChangesAsync(ct);
            }
            return message;
        }

        public async Task<List<ChatMessage>> MarkMessageAsSeenAsync(Guid chatRoomId, string viewerClerkId, CancellationToken ct = default)
        {
            try
            {
                var messages = await _context.ChatMessages
                    .Where(m => m.ChatRoomId == chatRoomId && m.SenderId != viewerClerkId && !m.WasSeen)
                    .ToListAsync(ct);

                foreach (var message in messages)
                {
                    message.MarkAsSeen(viewerClerkId);
                }
                await _context.SaveChangesAsync(ct);

                return messages;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ChatMessage>> GetUndeliveredMessagesAsync(Guid roomId, string recipientId, CancellationToken ct = default)
        {
            return await _context.ChatMessages
                           .Where(x => x.ChatRoomId == roomId && x.RecipientId == recipientId && !x.WasDelivered).ToListAsync(ct);
        }
    }
}
