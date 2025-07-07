using System.ComponentModel.DataAnnotations;

namespace ShowTime_BusinessLogic.Dtos.Booking
{
    public class BookingUpdateDto
    {
        [Required]
        public int Id { get; set; }
        
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
        
        [Required]
        public string Status { get; set; } = "Pending";
        
        public string PaymentMethod { get; set; } = string.Empty;
        
        public string PaymentStatus { get; set; } = "Pending";
        
        public string TransactionId { get; set; } = string.Empty;
        
        public DateTime? ConfirmationDate { get; set; }
        
        public DateTime? CancellationDate { get; set; }
    }
}
