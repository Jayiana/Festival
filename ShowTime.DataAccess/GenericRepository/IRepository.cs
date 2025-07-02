using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.GenericRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteAsync(int id);
        Task<T> GetByIdsAsync(object key1, object key2);
        Task DeleteByIdsAsync(object key1, object key2);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] include);
        Task<T> GetByIdsAsync(object key1, object key2, params Expression<Func<T, object>>[] include);


    }
}
