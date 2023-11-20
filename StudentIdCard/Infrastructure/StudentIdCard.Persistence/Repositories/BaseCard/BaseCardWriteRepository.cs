using StudentIdCard.Application.Repositories.BaseCard;
using StudentIdCard.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Persistence.Repositories.BaseCard
{
    public class BaseCardWriteRepository : WriteRepository<StudentIdCard.Domain.Entites.BaseCard>, IBaseCardWriteRepository
    {
        public BaseCardWriteRepository(StudentIdCardContext context) : base(context)
        {
        }
    }
}
