using StudentIdCard.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Application.Repositories.BaseCard
{
    public interface IBaseCardReadRepository : IReadRepository<StudentIdCard.Domain.Entites.BaseCard>
    {
    }
}
