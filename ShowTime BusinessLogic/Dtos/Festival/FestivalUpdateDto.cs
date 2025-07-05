using System;
using System.ComponentModel.DataAnnotations;

namespace ShowTime_BusinessLogic.Dtos.Festival
{
    public class FestivalUpdateDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "StartDate is required.")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "EndDate is required.")]
        public DateTime EndDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Location is required.")]
        [MinLength(3, ErrorMessage = "Location must be at least 3 characters.")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "SplashArt URL is required.")]
        [Url(ErrorMessage = "SplashArt must be a valid URL.")]
        public string SplashArt { get; set; } = string.Empty;

        [Range(10, 1000000, ErrorMessage = "Capacity must be between 10 and 1,000,000.")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Theme is required.")]
        public string Theme { get; set; } = string.Empty;

        public bool IsIndoor { get; set; }
        public bool HasCamping { get; set; }
        public bool HasFoodCourt { get; set; }
        public bool HasAfterParty { get; set; }

        [Range(0, 100, ErrorMessage = "Rating must be between 0 and 100.")]
        public int Rating { get; set; }

        [MinLength(5, ErrorMessage = "Description must be at least 5 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}