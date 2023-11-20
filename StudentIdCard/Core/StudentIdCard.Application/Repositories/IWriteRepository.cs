using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Application.Repositories
{
    public interface IWriteRepository <T> : IRepository<T> where T : class
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddAsync(IEnumerable<T> entities);
        bool Remove(T entity);
        bool Remove(string id);
        bool RemoveRange(IEnumerable<T> entities);
        bool Update(T model);
        Task<int> SaveAsync();
    }
}
