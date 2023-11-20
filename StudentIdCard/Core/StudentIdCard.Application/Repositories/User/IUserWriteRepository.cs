using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Application.Repositories.User
{
    public interface IUserWriteRepository : IWriteRepository<StudentIdCard.Domain.Entites.User>
    {
    }
}
