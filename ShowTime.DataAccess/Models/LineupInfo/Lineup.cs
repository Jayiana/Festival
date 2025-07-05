using ShowTime.DataAccess.Models.FestivalInfo;
using ShowTime.DataAccess.Models.ArtistInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Models.LineupInfo
{
    public class Lineup
    {
        public int FestivalId { get; set; }
        public int ArtistId {  get; set; }
        public string Stage {  get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public bool IsMainStage { get; set; }          
        public bool IsLivePerformance { get; set; }    
        public string? Description { get; set; }      
        public string? StageTheme { get; set; }       
        public Festival Festival { get; set; } = null!;
        public Artist Artist { get; set; } = null!;

    }
}
