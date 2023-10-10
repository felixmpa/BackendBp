using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bp.Common
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(int id);
        Task<T> GetAsync(int id, params Expression<Func<T, object>>[] includeProperties);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(int id);
    }
}