using StudentIdCard.Application.Repositories.Card;
using StudentIdCard.Domain.Entites;
using StudentIdCard.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Persistence.Repositories.Card
{
    public class CardReadRepository : ReadRepository<StudentIdCard.Domain.Entites.Card>, ICardReadRepository
    {
        public CardReadRepository(StudentIdCardContext context) : base(context)
        {
        }
    }
}
