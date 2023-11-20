using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Domain.Entites
{
    public class Basket
    {
        public Basket()
        {
            Products = new List<Product>();
        }
        public Guid Id { get; set; }
        public List<Product> Products { get; set; }
        public bool PaymentStatus { get; set; }
        public DateTime CreatedTime {  get; set; } = DateTime.Now;
        public Card Card { get; set; }
    }
}
