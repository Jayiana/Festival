using System.ComponentModel.DataAnnotations;
using System;

namespace ShowTime_BusinessLogic.Dtos.Accommodation
{
    public class AccommodationCreateDto
    {
        [Required]
        public int FestivalId { get; set; }
        
        [Required]
        public string AccommodationType { get; set; } = string.Empty;
        
        [Required]
        public string RoomType { get; set; } = string.Empty;
        
        [Required]
        [Range(1, 10, ErrorMessage = "Capacity must be between 1 and 10")]
        public int Capacity { get; set; }
        
        [Required]
        [Range(1, 30, ErrorMessage = "Number of nights must be between 1 and 30")]
        public int NumberOfNights { get; set; }
        
        [Required]
        public DateTime CheckInDate { get; set; }
        
        [Required]
        public DateTime CheckOutDate { get; set; }
        
        [Required]
        public string CustomerName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; } = string.Empty;
        
        [Required]
        [Phone]
        public string CustomerPhone { get; set; } = string.Empty;
        
        public string SpecialRequests { get; set; } = string.Empty;
        
        // Additional Services
        public bool IncludeBreakfast { get; set; }
        public bool IncludeDinner { get; set; }
        public bool IncludeShuttleService { get; set; }
        public bool IncludeCleaningService { get; set; }
        public bool IncludeEquipmentRental { get; set; }
    }
} 