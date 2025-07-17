using Application.Interfaces.Repositories;
using Domain.Entities.Chat;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDataContext _context;

        public ChatRepository(AppDataContext context)
        {
            _context = context;
        }

        public async Task<ChatRoom?> GetPrivateRoomAsync(string userAClerkId, string userBClerkId)
        {
            return await _context.ChatRooms
                .Include(r => r.Messages)
                .FirstOrDefaultAsync(r => r.UserAClerkId == userAClerkId && r.UserBClerkId == userBClerkId);
        }

        public async Task<ChatRoom> CreatePrivateRoomAsync(string userAClerkId, string userBClerkId, Guid petId)
        {
            var room = new ChatRoom(userAClerkId, userBClerkId, petId);
            await _context.ChatRooms.AddAsync(room);
            return room;
        }

        public async Task AddMessageAsync(ChatMessage message)
        {
            await _context.ChatMessages.AddAsync(message);
        }

        public async Task<List<ChatMessage>> GetMessagesAsync(Guid chatRoomId, int page, int pageSize)
        {
            return await _context.ChatMessages
                .Where(m => m.ChatRoomId == chatRoomId)
                .OrderBy(m => m.SentAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<ChatRoom>> GetChats(string userClerkId)
        {
            return await _context.ChatRooms.Where(x => x.UserAClerkId == userClerkId || x.UserBClerkId == userClerkId)
                .Include(r => r.Messages)
                .Include(x => x.UserA)
                .Include(x => x.UserB)
                .Include(x => x.Pet)
                .Include(x => x.Pet.PetImages)
                .ToListAsync();
        }
    }
}
