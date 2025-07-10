using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ShowTime_BusinessLogic.Dtos
{
    public class LineupCreateDto : IValidatableObject
    {
        [Required(ErrorMessage = "FestivalId is required.")]
        public int FestivalId { get; set; }

        [Required(ErrorMessage = "ArtistId is required.")]
        public int ArtistId { get; set; }

        [Required(ErrorMessage = "Stage is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Stage must be between 2 and 100 characters.")]
        [RegularExpression(@"^(?!\d+$).+", ErrorMessage = "Stage cannot be just numbers.")]
        public string Stage { get; set; } = string.Empty;

        [Required(ErrorMessage = "StartTime is required.")]
        public DateTime StartTime { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "IsMainStage is required.")]
        public bool IsMainStage { get; set; }

        [Required(ErrorMessage = "IsLivePerformance is required.")]
        public bool IsLivePerformance { get; set; }

        [StringLength(500, ErrorMessage = "Description can't exceed 500 characters.")]
        public string? Description { get; set; }

        [StringLength(100, ErrorMessage = "Stage theme can't exceed 100 characters.")]
        public string? StageTheme { get; set; }

        public DateTime EndTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var now = DateTime.Now;

            if (StartTime.Date < now.Date)
            {
                yield return new ValidationResult(
                    "Start Time cannot be in the past.",
                    new[] { nameof(StartTime) });
            }

            if (StartTime.Date == now.Date && StartTime.TimeOfDay < now.TimeOfDay)
            {
                yield return new ValidationResult(
                    "Start Time cannot be earlier than current time today.",
                    new[] { nameof(StartTime) });
            }

            if (StartTime.Year < now.Year)
            {
                yield return new ValidationResult(
                    $"Start Time must be in the current year ({now.Year}) or later.",
                    new[] { nameof(StartTime) });
            }
        }
    }
}
