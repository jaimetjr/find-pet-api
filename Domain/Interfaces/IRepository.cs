using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Abstractions;

namespace Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? include = null, CancellationToken ct = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,
                                                Func<IQueryable<T>, IQueryable<T>>? include = null, CancellationToken ct = default);
    Task AddAsync(T entity, CancellationToken ct = default);
    Task Update(T entity, CancellationToken ct = default);
    Task Remove(T entity, CancellationToken ct = default);
    IQueryable<T> AsQueryable();
    Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
    
    // Specification-based methods
    Task<T?> GetSingleAsync(ISpecification<T> spec, CancellationToken ct = default);
    Task<List<T>> ListAsync(ISpecification<T>? spec = null, CancellationToken ct = default);
    Task<int> CountAsync(ISpecification<T>? spec = null, CancellationToken ct = default);
    Task<bool> AnyAsync(ISpecification<T> spec, CancellationToken ct = default);
}

