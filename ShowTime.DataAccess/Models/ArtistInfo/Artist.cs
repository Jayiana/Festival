using ShowTime.DataAccess.Models.FestivalInfo;
using ShowTime.DataAccess.Models.LineupInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Models.ArtistInfo
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Genre {  get; set; } = string.Empty;
        public string Image {  get; set; } = string.Empty;
        public ICollection<Lineup> Lineups { get; set; } =  new List<Lineup>();
        public ICollection<Festival> Festivals { get; set; } = new List<Festival>();
        public int Capacity { get; set; }
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
