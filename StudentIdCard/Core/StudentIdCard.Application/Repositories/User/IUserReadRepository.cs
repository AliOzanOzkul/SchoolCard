using StudentIdCard.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Application.Repositories.User
{
    public interface IUserReadRepository : IReadRepository<StudentIdCard.Domain.Entites.User>
    {
    }
}
