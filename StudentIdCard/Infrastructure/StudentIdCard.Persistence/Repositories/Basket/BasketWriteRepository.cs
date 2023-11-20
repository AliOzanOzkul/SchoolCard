using StudentIdCard.Application.Repositories.Basket;
using StudentIdCard.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Persistence.Repositories.Basket
{
    public class BasketWriteRepository : WriteRepository<StudentIdCard.Domain.Entites.Basket>, IBasketWriteRepository
    {
        public BasketWriteRepository(StudentIdCardContext context) : base(context)
        {
        }
    }
}
