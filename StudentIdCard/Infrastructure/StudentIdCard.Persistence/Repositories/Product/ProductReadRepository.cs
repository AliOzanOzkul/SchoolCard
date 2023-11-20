using StudentIdCard.Application.Repositories.Product;
using StudentIdCard.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Persistence.Repositories.Product
{
    public class ProductReadRepository : ReadRepository<StudentIdCard.Domain.Entites.Product>, IProductReadRepository
    {
        public ProductReadRepository(StudentIdCardContext context) : base(context)
        {
        }
    }
}
