using StudentIdCard.Application.Repositories.BaseCard;
using StudentIdCard.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Persistence.Repositories.BaseCard
{
    public class BaseCardReadRepository : ReadRepository<StudentIdCard.Domain.Entites.BaseCard>, IBaseCardReadRepository
    {
        public BaseCardReadRepository(StudentIdCardContext context) : base(context)
        {
        }
    }
}
