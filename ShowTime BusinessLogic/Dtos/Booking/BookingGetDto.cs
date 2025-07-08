namespace ShowTime_BusinessLogic.Dtos.Booking
{
    public class BookingGetDto
    {
        public int Id { get; set; }
        public string FestivalName { get; set; } = string.Empty;
        public DateTime FestivalDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public string TicketType { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string TicketNumber { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public bool IncludeCamping { get; set; }
        public bool IncludeFoodPackage { get; set; }
        public bool IncludeTransportation { get; set; }
        public string Status { get; set; } = string.Empty;
        public int FestivalId { get; set; }
        public DateTime FestivalStartDate { get; set; }
        public DateTime FestivalEndDate { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
