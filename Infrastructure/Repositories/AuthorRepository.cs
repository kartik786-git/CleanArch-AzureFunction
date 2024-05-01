using Application.Interface;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(BlogDbContext blogDbContext): base(blogDbContext)
        {
            
        }
        public async Task<List<Author>> GetByNameAsync(string name)
        {
          return await _blogDbContext.Authors.Where(x => x.Name.Contains(name)).ToListAsync();
        }
    }
}
