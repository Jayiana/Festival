namespace ShowTime_BusinessLogic.Dtos
{
    public class LineupGetDto
    {
        public int FestivalId { get; set; }
        public int ArtistId { get; set; }
        public string Stage { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public string ArtistName { get; set; } = string.Empty;
        public string FestivalName { get; set; } = string.Empty;
        public bool IsMainStage { get; set; }
        public bool IsLivePerformance { get; set; }
        public string? Description { get; set; }
        public string? StageTheme { get; set; }
    }
}