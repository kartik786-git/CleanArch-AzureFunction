using Application.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal readonly BlogDbContext _blogDbContext;

        public Repository(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
          return  await _blogDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T? result = await _blogDbContext.Set<T>().FindAsync(id);
            return result;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _blogDbContext.Set<T>().AddAsync(entity);
            await _blogDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _blogDbContext.Set<T>().Remove(entity);
            await _blogDbContext.SaveChangesAsync();
        }

       

        public async Task UpdateAsync(T entity)
        {
            _blogDbContext.Entry(entity).State = EntityState.Modified;
            await _blogDbContext.SaveChangesAsync();
        }
    }
}
