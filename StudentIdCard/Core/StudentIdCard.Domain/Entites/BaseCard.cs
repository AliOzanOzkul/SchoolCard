using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Domain.Entites
{
    public class BaseCard
    {
        public Guid Id { get; set; }
        public string CardNo { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        //[ForeignKey("Card")]
        public Guid CardId { get; set; }
        public Card? Card { get; set; }
    }
}
