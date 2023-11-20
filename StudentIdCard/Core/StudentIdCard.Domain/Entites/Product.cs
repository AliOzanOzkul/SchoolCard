using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Domain.Entites
{
    public class Product
    {
        public Product()
        {
            Baskets = new();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Stoct { get; set; }
        public decimal Price { get; set; }
        public List<Basket> Baskets { get; set; }
    }
}
