using ShowTime.DataAccess.Models.FestivalInfo;
using ShowTime.DataAccess.Models.UserInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Models.AccommodationInfo
{
    public class Accommodation
    {
        public int Id { get; set; }
        public int FestivalId { get; set; }
        public Festival Festival { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public string AccommodationType { get; set; } = string.Empty;
        public string RoomType { get; set; } = string.Empty; 
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public int NumberOfNights { get; set; }
        public decimal TotalPrice { get; set; }     
        public string Location { get; set; } = string.Empty; 
        public bool HasShower { get; set; }
        public bool HasElectricity { get; set; }
        public bool HasWiFi { get; set; }
        public bool HasParking { get; set; }
        public bool HasSecurity { get; set; }
        public bool HasFoodService { get; set; }
        public bool HasTransportation { get; set; } 
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string SpecialRequests { get; set; } = string.Empty;
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public DateTime? ConfirmationDate { get; set; }
        public DateTime? CancellationDate { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = "Pending";
        public string TransactionId { get; set; } = string.Empty;
        public bool IncludeBreakfast { get; set; }
        public bool IncludeDinner { get; set; }
        public bool IncludeShuttleService { get; set; }
        public bool IncludeCleaningService { get; set; }
        public bool IncludeEquipmentRental { get; set; } 
    }
} 