using Application.DTOs.Chat;
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
    public class ChatMessageReadRepository : IChatMessageReadRepository
    {
        private readonly AppDataContext _context;

        public ChatMessageReadRepository(AppDataContext context)
        {
            _context = context;
        }

        public async Task<MessagePageDto> GetBeforeAsync(Guid roomId, string? before, int limit, CancellationToken cancellationToken = default)
        {
            var q = _context.ChatMessages.AsNoTracking().Where(m => m.ChatRoomId == roomId);

            DateTime? cutoff = null;
            Guid cutoffId = default;

            if (!string.IsNullOrWhiteSpace(before))
            {
                try
                {
                    var (ts, id) = ParseBefore(before);
                    cutoff = ts;
                    cutoffId = id;
                }
                catch
                {
                    // Invalid cursor -> pretend no more
                    return new MessagePageDto { Items = new List<ChatMessage>(), HasMore = false, NextBefore = null };
                }
            }

            if (cutoff.HasValue)
            {
                q = q.Where(m => m.SentAt < cutoff.Value
                              || (m.SentAt == cutoff.Value && m.Id.CompareTo(cutoffId) < 0));
            }

            var pageDesc = await q.OrderByDescending(m => m.SentAt).ThenByDescending(m => m.Id)
                                  .Take(limit).ToListAsync(cancellationToken);

            pageDesc.Reverse();

            var hasMore = pageDesc.Count == limit;
            var nextBefore = hasMore
                ? MakeBeforeCursor(pageDesc[0].SentAt, pageDesc[0].Id)
                : null;

            return new MessagePageDto { Items = pageDesc, HasMore = hasMore, NextBefore = nextBefore };
        }

        public async Task<MessagePageDto> GetLatestAsync(Guid roomId, int limit, CancellationToken cancellationToken = default)
        {
            return await GetBeforeAsync(roomId, null, limit, cancellationToken);
        }

        private string MakeBeforeCursor(DateTime sendAt, Guid id) => $"{sendAt.Ticks}:{id:N}";
        private (DateTime sendAt, Guid id) ParseBefore(string c)
        {
            var p = c.Split('-', 2);
            return (new DateTime(long.Parse(p[0]), DateTimeKind.Utc), Guid.ParseExact(p[1], "N"));
        }
    }
}
