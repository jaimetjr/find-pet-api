using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? include = null);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,
                                                    Func<IQueryable<T>, IQueryable<T>>? include = null);
        Task AddAsync(T entity);
        Task Update(T entity);
        Task Remove(T entity);
        IQueryable<T> AsQueryable();
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
