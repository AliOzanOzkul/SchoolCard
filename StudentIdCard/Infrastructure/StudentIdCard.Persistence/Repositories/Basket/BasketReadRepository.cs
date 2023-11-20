using StudentIdCard.Application.Repositories.Basket;
using StudentIdCard.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Persistence.Repositories.Basket
{
    public class BasketReadRepository : ReadRepository<StudentIdCard.Domain.Entites.Basket>, IBasketReadRepository
    {
        public BasketReadRepository(StudentIdCardContext context) : base(context)
        {
        }
    }
}
