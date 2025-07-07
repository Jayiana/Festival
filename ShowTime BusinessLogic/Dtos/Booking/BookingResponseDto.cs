namespace ShowTime_BusinessLogic.Dtos.Booking
{
    public class BookingResponseDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? BookingId { get; set; }
        public string? TicketNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public string? FestivalName { get; set; }
        public DateTime? FestivalDate { get; set; }
        public string? Location { get; set; }
    }
} 