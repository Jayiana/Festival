using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShowTime_BusinessLogic.Dtos.Festival
{
    public class FestivalCreateDto : IValidatableObject
    {
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "StartDate is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "EndDate is required.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [MinLength(3, ErrorMessage = "Location must be at least 3 characters.")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "SplashArt URL is required.")]
        [Url(ErrorMessage = "SplashArt must be a valid URL.")]
        public string SplashArt { get; set; } = string.Empty;

        [Range(10, 1000000, ErrorMessage = "Capacity must be between 10 and 1,000,000.")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Theme is required.")]
        [MaxLength(100, ErrorMessage = "Theme can't exceed 100 characters.")]
        public string Theme { get; set; } = string.Empty;

        public bool IsIndoor { get; set; }
        public bool HasCamping { get; set; }
        public bool HasFoodCourt { get; set; }
        public bool HasAfterParty { get; set; }

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1000, ErrorMessage = "Description can't exceed 1000 characters.")]
        public string Description { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate > EndDate)
            {
                yield return new ValidationResult(
                    "Start date must be before end date.",
                    new[] { nameof(StartDate), nameof(EndDate) });
            }

            if (StartDate.Year < DateTime.Now.Year)
            {
                yield return new ValidationResult(
                    $"Start year must be in the current year ({DateTime.Now.Year}) or later.",
                    new[] { nameof(StartDate) });
            }
        }
    }
}
