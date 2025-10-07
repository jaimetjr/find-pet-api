using Application.DTOs.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IChatMessageReadService
    {
        Task<MessagePageDto> GetLatestAsync(Guid roomId, int limit, CancellationToken cancellationToken = default);
        Task<MessagePageDto> GetBeforeAsync(Guid roomId, string? before, int limit, CancellationToken cancellationToken = default);
    }
}
