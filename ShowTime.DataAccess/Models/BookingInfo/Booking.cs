using ShowTime.DataAccess.Models.FestivalInfo;
using ShowTime.DataAccess.Models.UserInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Models.BookingInfo
{
    public class Booking
    {
        public int Id { get; set; }
        public int FestivalId { get; set; }
        public Festival Festival { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        
        public string TicketType { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string TicketNumber { get; set; } = string.Empty;
        public decimal Price { get; set; }
        
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string SpecialRequests { get; set; } = string.Empty;
        
        public bool IncludeCamping { get; set; }
        public bool IncludeFoodPackage { get; set; }
        public bool IncludeTransportation { get; set; }
        
        public string Status { get; set; } = "Pending"; 
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public DateTime? ConfirmationDate { get; set; }
        public DateTime? CancellationDate { get; set; }
        
        public string PaymentMethod { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = "Pending"; 
        public string TransactionId { get; set; } = string.Empty;
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
