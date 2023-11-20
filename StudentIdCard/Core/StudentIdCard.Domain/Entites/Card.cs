using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Domain.Entites
{
    public class Card
    {
        public Card()
        {
            Baskets = new List<Basket>();
            Attendances = new List<Attendance>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ParentName { get; set; }
        public long ParentPhoneNumber { get; set; }
        public string SchoolId { get; set; }
        public decimal Balance { get; set; }
        public string ParentUserName { get; set; }
        public string ParentPassword { get; set; }
        public int Cafeteria {  get; set; } = 0;
        public string? PhotoPath {  get; set; }
        public List<Attendance> Attendances { get; set; }
        public List<Basket> Baskets { get; set; }
        //[ForeignKey("BaseCard")]
        public Guid BaseCardId { get; set; }
        public BaseCard BaseCard { get; set; }
    }
}
