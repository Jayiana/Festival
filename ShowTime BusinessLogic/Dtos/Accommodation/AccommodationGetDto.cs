using System;

namespace ShowTime_BusinessLogic.Dtos.Accommodation
{
    public class AccommodationGetDto
    {
        public int Id { get; set; }
        public string FestivalName { get; set; } = string.Empty;
        public DateTime FestivalDate { get; set; }
        public string FestivalLocation { get; set; } = string.Empty;
        public DateTime FestivalStartDate { get; set; }
        public DateTime FestivalEndDate { get; set; }
        
        // Accommodation Details
        public string AccommodationType { get; set; } = string.Empty;
        public string RoomType { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public int NumberOfNights { get; set; }
        public decimal TotalPrice { get; set; }
        
        // Location and Amenities
        public string Location { get; set; } = string.Empty;
        public bool HasShower { get; set; }
        public bool HasElectricity { get; set; }
        public bool HasWiFi { get; set; }
        public bool HasParking { get; set; }
        public bool HasSecurity { get; set; }
        public bool HasFoodService { get; set; }
        public bool HasTransportation { get; set; }
        
        // Customer Information
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string SpecialRequests { get; set; } = string.Empty;
        
        // Booking Details
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public DateTime? CancellationDate { get; set; }
        
        // Additional Services
        public bool IncludeBreakfast { get; set; }
        public bool IncludeDinner { get; set; }
        public bool IncludeShuttleService { get; set; }
        public bool IncludeCleaningService { get; set; }
        public bool IncludeEquipmentRental { get; set; }
        
        // Payment Information
        public string PaymentStatus { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
    }
} 