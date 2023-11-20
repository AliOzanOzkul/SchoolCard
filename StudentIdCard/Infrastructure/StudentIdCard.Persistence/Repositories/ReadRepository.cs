using Microsoft.EntityFrameworkCore;
using StudentIdCard.Application.Repositories;
using StudentIdCard.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class
    {
        private readonly StudentIdCardContext _context;

        public ReadRepository(StudentIdCardContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll()
        {
            return Table;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await Table.FindAsync(Guid.Parse(id));
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> metod)
        {
            return await Table.FirstOrDefaultAsync(metod);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> metod)
        {
            return Table.Where(metod);
        }
    }
}
