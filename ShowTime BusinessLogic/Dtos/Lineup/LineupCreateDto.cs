using System;
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
        public DateTime StartTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartTime.Date < DateTime.Now.Date)
            {
                yield return new ValidationResult(
                    "Start Time cannot be in the past.",
                    new[] { nameof(StartTime) });
            }

            if (StartTime.Date == DateTime.Now.Date && StartTime.TimeOfDay < DateTime.Now.TimeOfDay)
            {
                yield return new ValidationResult(
                    "Start Time cannot be in the past for today.",
                    new[] { nameof(StartTime) });
            }
        }
    }
}
