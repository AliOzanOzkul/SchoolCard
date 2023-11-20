using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Domain.Entites
{
    public class Attendance
    {
        public Guid Id { get; set; }
        public DateTime? EntranceTime { get; set; }
        public DateTime? LeavingTime { get; set; }
        public Card Card { get; set; }
    }
}
