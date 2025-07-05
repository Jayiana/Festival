using System;
using System.ComponentModel.DataAnnotations;

namespace ShowTime_BusinessLogic.Dtos
{
    public class LineupUpdateDto
    {
        [Required(ErrorMessage = "Stage is required.")]
        public string Stage { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start time is required.")]
        public DateTime StartTime { get; set; } = DateTime.Now;

        public bool IsMainStage { get; set; }
        public bool IsLivePerformance { get; set; }

        [MinLength(5, ErrorMessage = "Description must be at least 5 characters.")]
        public string? Description { get; set; }
        public string? StageTheme { get; set; }
    }
}