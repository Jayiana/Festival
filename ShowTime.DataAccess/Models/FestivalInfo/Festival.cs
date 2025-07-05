using ShowTime.DataAccess.Models.LineupInfo;
using ShowTime.DataAccess.Models.ArtistInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Models.FestivalInfo
{
    public class Festival
    {
        public int Id {  get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public string SplashArt { get; set; } =  string.Empty;
        public int Capacity { get; set; }
        public string Theme { get; set; } = string.Empty; 
        public bool IsIndoor { get; set; }                
        public bool HasCamping { get; set; }              
        public bool HasFoodCourt { get; set; }            
        public bool HasAfterParty { get; set; }           
        public int Rating { get; set; }                  
        public string Description { get; set; } = string.Empty;
        public ICollection<Lineup> Lineups { get; set; } = new List<Lineup>();
        public ICollection<Artist> Artists { get; set; } = new List<Artist>();
    }
}
