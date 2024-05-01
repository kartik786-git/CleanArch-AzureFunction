using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<List<Author>> GetByNameAsync(string name);
    }
}
