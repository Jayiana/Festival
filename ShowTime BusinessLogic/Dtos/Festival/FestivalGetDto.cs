using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime_BusinessLogic.Dtos.Festival
{
    public class FestivalGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public string SplashArt { get; set; } = string.Empty;
        public int Capacity { get; set; }
    }
}
