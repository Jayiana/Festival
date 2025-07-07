using System;

namespace ShowTime_BusinessLogic.Dtos.Accommodation
{
    public class AccommodationResponseDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? BookingId { get; set; }
        public string? ConfirmationNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public string? FestivalName { get; set; }
        public DateTime? FestivalDate { get; set; }
        public string? Location { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public string? AccommodationType { get; set; }
        public string? RoomType { get; set; }
    }
} 