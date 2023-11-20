using StudentIdCard.Application.Repositories.User;
using StudentIdCard.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Persistence.Repositories.User
{
    public class UserReadRepository : ReadRepository<StudentIdCard.Domain.Entites.User>, IUserReadRepository
    {
        public UserReadRepository(StudentIdCardContext context) : base(context)
        {
        }
    }
}
