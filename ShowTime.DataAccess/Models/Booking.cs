using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Models
{
    public class Booking
    {
        public int FestivalId { get; set; }
        public Festival Festival { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string Type { get; set; } = string.Empty;
        public int Price { get; set; }
    }
}
