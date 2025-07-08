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
        public string? TicketType { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public decimal Price { get; set; }
        public bool IncludeCamping { get; set; }
        public bool IncludeFoodPackage { get; set; }
        public bool IncludeTransportation { get; set; }
        public DateTime FestivalStartDate { get; set; }
        public DateTime FestivalEndDate { get; set; }
    }
} 