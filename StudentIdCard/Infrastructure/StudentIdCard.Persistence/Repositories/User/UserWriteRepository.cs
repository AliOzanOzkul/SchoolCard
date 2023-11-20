using StudentIdCard.Application.Repositories.User;
using StudentIdCard.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Persistence.Repositories.User
{
    public class UserWriteRepository : WriteRepository<StudentIdCard.Domain.Entites.User>, IUserWriteRepository
    {
        public UserWriteRepository(StudentIdCardContext context) : base(context)
        {
        }
    }
}
