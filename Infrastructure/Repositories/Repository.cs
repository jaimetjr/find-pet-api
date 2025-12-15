using Domain.Interfaces;
using Domain.Abstractions;
using Infrastructure.Data;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T: class
    {
        protected readonly AppDataContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDataContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task AddAsync(T entity, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }

        /// <summary>
        /// This is the default get all without includes or predicates
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default)
        {
            return await _dbSet.ToListAsync(ct);
        }

        /// <summary>
        /// This is the complete get all method with optional includes and predicates
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,
                                                    Func<IQueryable<T>, IQueryable<T>>? include = null, CancellationToken ct = default)
        {
            IQueryable<T> query = _dbSet;

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);
            return await query.ToListAsync(ct);
        }

        /// <summary>
        /// This is just a generic get by id method. It assumes that the primary key is of type Guid.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _dbSet.FindAsync(new object[] { id }, ct);
        }

        /// <summary>
        /// This is a more complete get single method with optional includes and predicates
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public virtual async Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? include = null, CancellationToken ct = default)
        {
            IQueryable<T> query = _dbSet;

            if (include is not null)
                query = include(query);
            return await query.FirstOrDefaultAsync(predicate, ct);
        }

        public virtual async Task Remove(T entity, CancellationToken ct = default)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }

        public virtual async Task Update(T entity, CancellationToken ct = default)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(ct);
        }

        public virtual IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        {
            return await _dbSet.Where(predicate).ToListAsync(ct);
        }

        // Specification-based methods
        public virtual async Task<T?> GetSingleAsync(ISpecification<T> spec, CancellationToken ct = default)
        {
            var query = SpecificationEvaluator.GetQuery(_dbSet, spec);
            return await query.FirstOrDefaultAsync(ct);
        }

        public virtual async Task<List<T>> ListAsync(ISpecification<T>? spec = null, CancellationToken ct = default)
        {
            var query = SpecificationEvaluator.GetQuery(_dbSet, spec);
            return await query.ToListAsync(ct);
        }

        public virtual async Task<int> CountAsync(ISpecification<T>? spec = null, CancellationToken ct = default)
        {
            var query = SpecificationEvaluator.GetQuery(_dbSet, spec);
            return await query.CountAsync(ct);
        }

        public virtual async Task<bool> AnyAsync(ISpecification<T> spec, CancellationToken ct = default)
        {
            var query = SpecificationEvaluator.GetQuery(_dbSet, spec);
            return await query.AnyAsync(ct);
        }
    }
}
