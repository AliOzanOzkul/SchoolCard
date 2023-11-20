using StudentIdCard.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Application.Repositories.Card
{
    public interface ICardWriteRepository : IWriteRepository<StudentIdCard.Domain.Entites.Card>
    {
        Task<string> CafeteriaRight(string id);
        public Task<string> StudentsEntry(string id);
        public Task<string> StudentsLeaving(string id);
    }
}
