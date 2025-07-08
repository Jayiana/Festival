using System.ComponentModel.DataAnnotations;

namespace ShowTime_BusinessLogic.Dtos.Booking
{
    public class BookingCreateDto
    {
        [Required]
        public int FestivalId { get; set; }
        
        [Required]
        public string TicketType { get; set; } = string.Empty;
        
        [Required]
        [Range(1, 10, ErrorMessage = "Quantity must be between 1 and 10")]
        public int Quantity { get; set; }
        
        [Required]
        public string CustomerName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; } = string.Empty;
        
        [Required]
        [Phone]
        public string CustomerPhone { get; set; } = string.Empty;
        
        public string SpecialRequests { get; set; } = string.Empty;
        
        public bool IncludeCamping { get; set; }
        
        public bool IncludeFoodPackage { get; set; }
        
        public bool IncludeTransportation { get; set; }

        public int UserId { get; set; }
    }
}
