using System;

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
        public string Theme { get; set; } = string.Empty;
        public bool IsIndoor { get; set; }
        public bool HasCamping { get; set; }
        public bool HasFoodCourt { get; set; }
        public bool HasAfterParty { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}