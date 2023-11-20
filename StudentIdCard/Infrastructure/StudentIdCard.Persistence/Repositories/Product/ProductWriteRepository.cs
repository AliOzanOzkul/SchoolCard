using StudentIdCard.Application.Repositories.Product;
using StudentIdCard.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Persistence.Repositories.Product
{
    public class ProductWriteRepository : WriteRepository<StudentIdCard.Domain.Entites.Product>, IProductWriteRepository
    {
        public ProductWriteRepository(StudentIdCardContext context) : base(context)
        {
        }
    }
}
