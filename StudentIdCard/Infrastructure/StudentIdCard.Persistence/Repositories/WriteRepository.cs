using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StudentIdCard.Application.Repositories;
using StudentIdCard.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class
    {
        private readonly StudentIdCardContext _context;

        public WriteRepository(StudentIdCardContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> model = await Table.AddAsync(entity);
            return model.State == EntityState.Added;
        }

        public async Task<bool> AddAsync(IEnumerable<T> entities)
        {
            await Table.AddRangeAsync(entities);
            return true;
        }

        public bool Remove(T entity)
        {
            EntityEntry model = Table.Remove(entity);
            return model.State == EntityState.Deleted;
        }

        public bool Remove(string id)
        {
            var selected = Table.Find(Guid.Parse(id));
            EntityEntry model = Table.Remove(selected);
            return model.State == EntityState.Deleted;
        }

        public bool RemoveRange(IEnumerable<T> entities)
        {
            Table.RemoveRange(entities);
            return true;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public bool Update(T model)
        {
            EntityEntry entity = Table.Update(model);
            return entity.State == EntityState.Modified;
        }
    }
}
