using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.GenericRepository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
       
            private readonly ShowTimeDbContext _context;

            public GenericRepository(ShowTimeDbContext context)
            {
                _context = context;
            }

            public async Task<T> GetByIdAsync(int id)
            {
                return await _context.Set<T>().FindAsync(id);
            }

            public async Task<IEnumerable<T>> GetAllAsync()
            {
                return await _context.Set<T>().ToListAsync();
            }

            public async Task AddAsync(T obj)
            {
                if (obj == null)
                    throw new ArgumentNullException(nameof(obj), "Object to add cannot be null");

                _context.Set<T>().Add(obj);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(T obj)
            {
                if (obj == null)
                    throw new ArgumentNullException(nameof(obj), "Object to update cannot be null");

                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }



            public async Task DeleteAsync(int id)
            {
                if (id <= 0)
                    throw new ArgumentException("ID must be greater than zero");

                var product = await _context.Set<T>().FindAsync(id);
                if (product != null)
                {
                    _context.Set<T>().Remove(product);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new InvalidOperationException("Item not found");
                }
            }

            public async Task<T> GetByIdsAsync(object key1, object key2)
            {
                return await _context.Set<T>().FirstOrDefaultAsync(e =>
                    EF.Property<object>(e, "FestivalId").Equals(key1) &&
                    EF.Property<object>(e, "ArtistId").Equals(key2));
            }

            public async Task DeleteByIdsAsync(object key1, object key2)
            {
                var entity = await _context.Set<T>().FirstOrDefaultAsync(e =>
                    EF.Property<object>(e, "FestivalId").Equals(key1) &&
                    EF.Property<object>(e, "ArtistId").Equals(key2));

                if (entity != null)
                {
                    _context.Set<T>().Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }

            public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] include)
            {
                IQueryable<T> query = _context.Set<T>();

                foreach (var includeProperty in include)
                {
                    query = query.Include(includeProperty);
                }

                return await query.ToListAsync();
            }

            public async Task<T> GetByIdsAsync(object key1, object key2, params Expression<Func<T, object>>[] include)
            {
                IQueryable<T> query = _context.Set<T>();

                foreach (var includeProperty in include)
                {
                    query = query.Include(includeProperty);
                }

                return await query.FirstOrDefaultAsync(e =>
                    EF.Property<object>(e, "FestivalId").Equals(key1) &&
                    EF.Property<object>(e, "ArtistId").Equals(key2));
            }


    }
}
