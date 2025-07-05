using System;
using System.ComponentModel.DataAnnotations;

namespace ShowTime_BusinessLogic.Dtos
{
    public class LineupUpdateDto
    {
        [Required(ErrorMessage = "Stage is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Stage must be between 2 and 100 characters.")]
        public string Stage { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start time is required.")]
        public DateTime StartTime { get; set; } = DateTime.Now;

        public bool IsMainStage { get; set; }
        public bool IsLivePerformance { get; set; }

        [StringLength(500, ErrorMessage = "Description can't exceed 500 characters.")]
        public string? Description { get; set; }
        
        [StringLength(100, ErrorMessage = "Stage theme can't exceed 100 characters.")]
        public string? StageTheme { get; set; }
    }
}