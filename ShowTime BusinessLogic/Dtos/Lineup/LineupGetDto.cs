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
    }
}
