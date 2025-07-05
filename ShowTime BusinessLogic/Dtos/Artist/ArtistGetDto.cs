using ShowTime.DataAccess.Models.ArtistInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime_BusinessLogic.Dtos.Artist
{
    public class ArtistGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public double Rating { get; set; } = 0.0;
        public bool IsTrending { get; set; } = false;
        public string? TrendingReason { get; set; }
        public string? Origin { get; set; }
        public string? Description { get; set; }
        public string? Instagram { get; set; }
        public string? YouTube { get; set; }
        public int FansCount { get; set; } = 0;
        public int DebutYear { get; set; } = 0;
        public ArtistCategory Category { get; set; }
    }
}