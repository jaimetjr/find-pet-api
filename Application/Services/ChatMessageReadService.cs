using Application.DTOs.Chat;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ChatMessageReadService : IChatMessageReadService
    {
        private readonly IChatMessageReadRepository _chatMessageReadRepository;

        public ChatMessageReadService(IChatMessageReadRepository chatMessageReadRepository)
        {
            _chatMessageReadRepository = chatMessageReadRepository;
        }

        public Task<MessagePageDto> GetBeforeAsync(Guid roomId, string? before, int limit, CancellationToken cancellationToken = default)
        {
            return _chatMessageReadRepository.GetBeforeAsync(roomId, before, limit, cancellationToken);
        }

        public Task<MessagePageDto> GetLatestAsync(Guid roomId, int limit, CancellationToken cancellationToken = default)
        {
            return _chatMessageReadRepository.GetLatestAsync(roomId, limit, cancellationToken);
        }
    }
}
