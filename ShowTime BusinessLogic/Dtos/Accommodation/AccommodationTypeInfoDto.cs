using System.Collections.Generic;

namespace ShowTime_BusinessLogic.Dtos.Accommodation
{
    public class AccommodationTypeInfoDto
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public string[] Features { get; set; } = new string[0];
    }
} 