using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Application.Repositories.Card
{
    public interface ICardReadRepository : IReadRepository<StudentIdCard.Domain.Entites.Card>
    {
    }
}
