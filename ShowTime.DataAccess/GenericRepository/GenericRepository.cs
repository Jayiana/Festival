using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.DataAccess.Models;

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


    }
}
