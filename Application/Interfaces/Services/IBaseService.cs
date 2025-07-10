using Application.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IBaseService<TEntity, TDto, TCreateDto, TUpdateDto>
    {
        Task<Result<TDto>> GetByIdAsync(Guid id);
        Task<Result<List<TDto>>> GetAllAsync();
        Task<Result<TDto>> CreateAsync(TCreateDto createDto);
        Task<Result<TDto>> UpdateAsync(Guid id, TUpdateDto updateDto);
        Task<Result<bool>> DeleteAsync(Guid id);
    }
} 