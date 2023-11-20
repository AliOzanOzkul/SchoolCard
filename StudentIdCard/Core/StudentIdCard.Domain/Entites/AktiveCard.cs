using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Domain.Entites
{
    public class AktiveCard
    {
        public Guid Id { get; set; }
        public string CardNo { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
