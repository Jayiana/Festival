using ShowTime.DataAccess.GenericRepository;
using ShowTime.DataAccess.Models.BookingInfo;
using ShowTime.DataAccess.Models.FestivalInfo;
using ShowTime_BusinessLogic.Abstractions;
using ShowTime_BusinessLogic.Dtos.Booking;
using System.Security.Cryptography;
using System.Text;

namespace ShowTime_BusinessLogic.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IRepository<Festival> _festivalRepository;

        public BookingService(IRepository<Booking> bookingRepository, IRepository<Festival> festivalRepository)
        {
            _bookingRepository = bookingRepository;
            _festivalRepository = festivalRepository;
        }

        public async Task<BookingResponseDto> CreateBookingAsync(BookingCreateDto bookingDto)
        {
            try
            {
                // Get festival details
                var festivals = await _festivalRepository.GetAllAsync();
                var festival = festivals.FirstOrDefault(f => f.Id == bookingDto.FestivalId);

                if (festival == null)
                {
                    return new BookingResponseDto
                    {
                        Success = false,
                        Message = "Festival not found."
                    };
                }

                // Calculate total price
                decimal basePrice = GetTicketPrice(bookingDto.TicketType);
                decimal totalPrice = basePrice * bookingDto.Quantity;

                // Add additional services
                if (bookingDto.IncludeCamping) totalPrice += 50 * bookingDto.Quantity;
                if (bookingDto.IncludeFoodPackage) totalPrice += 30 * bookingDto.Quantity;
                if (bookingDto.IncludeTransportation) totalPrice += 25 * bookingDto.Quantity;

                // Generate ticket number
                string ticketNumber = GenerateTicketNumber();

                // Create booking
                var booking = new Booking
                {
                    FestivalId = bookingDto.FestivalId,
                    UserId = 1, // Default user ID - should be replaced with actual user ID
                    TicketType = bookingDto.TicketType,
                    Quantity = bookingDto.Quantity,
                    TicketNumber = ticketNumber,
                    Price = totalPrice,
                    CustomerName = bookingDto.CustomerName,
                    CustomerEmail = bookingDto.CustomerEmail,
                    CustomerPhone = bookingDto.CustomerPhone,
                    SpecialRequests = bookingDto.SpecialRequests,
                    IncludeCamping = bookingDto.IncludeCamping,
                    IncludeFoodPackage = bookingDto.IncludeFoodPackage,
                    IncludeTransportation = bookingDto.IncludeTransportation,
                    Status = "Pending",
                    PaymentStatus = "Pending",
                    BookingDate = DateTime.Now
                };

                await _bookingRepository.AddAsync(booking);

                return new BookingResponseDto
                {
                    Success = true,
                    Message = "Booking created successfully!",
                    BookingId = booking.Id.ToString(),
                    TicketNumber = ticketNumber,
                    TotalPrice = totalPrice,
                    FestivalName = festival.Name,
                    FestivalDate = festival.StartDate,
                    Location = festival.Location
                };
            }
            catch (Exception ex)
            {
                return new BookingResponseDto
                {
                    Success = false,
                    Message = $"Booking failed: {ex.Message}"
                };
            }
        }

        public async Task<BookingResponseDto> UpdateBookingAsync(BookingUpdateDto bookingDto)
        {
            try
            {
                var bookings = await _bookingRepository.GetAllAsync();
                var booking = bookings.FirstOrDefault(b => b.Id == bookingDto.Id);

                if (booking == null)
                {
                    return new BookingResponseDto
                    {
                        Success = false,
                        Message = "Booking not found."
                    };
                }

                // Update booking properties
                booking.FestivalId = bookingDto.FestivalId;
                booking.TicketType = bookingDto.TicketType;
                booking.Quantity = bookingDto.Quantity;
                booking.CustomerName = bookingDto.CustomerName;
                booking.CustomerEmail = bookingDto.CustomerEmail;
                booking.CustomerPhone = bookingDto.CustomerPhone;
                booking.SpecialRequests = bookingDto.SpecialRequests;
                booking.IncludeCamping = bookingDto.IncludeCamping;
                booking.IncludeFoodPackage = bookingDto.IncludeFoodPackage;
                booking.IncludeTransportation = bookingDto.IncludeTransportation;
                booking.Status = bookingDto.Status;
                booking.PaymentMethod = bookingDto.PaymentMethod;
                booking.PaymentStatus = bookingDto.PaymentStatus;
                booking.TransactionId = bookingDto.TransactionId;
                booking.ConfirmationDate = bookingDto.ConfirmationDate;
                booking.CancellationDate = bookingDto.CancellationDate;

                // Recalculate price
                decimal basePrice = GetTicketPrice(bookingDto.TicketType);
                decimal totalPrice = basePrice * bookingDto.Quantity;

                if (bookingDto.IncludeCamping) totalPrice += 50 * bookingDto.Quantity;
                if (bookingDto.IncludeFoodPackage) totalPrice += 30 * bookingDto.Quantity;
                if (bookingDto.IncludeTransportation) totalPrice += 25 * bookingDto.Quantity;

                booking.Price = totalPrice;

                await _bookingRepository.UpdateAsync(booking);

                // Get festival details for response
                var festivals = await _festivalRepository.GetAllAsync();
                var festival = festivals.FirstOrDefault(f => f.Id == bookingDto.FestivalId);

                return new BookingResponseDto
                {
                    Success = true,
                    Message = "Booking updated successfully!",
                    BookingId = booking.Id.ToString(),
                    TicketNumber = booking.TicketNumber,
                    TotalPrice = totalPrice,
                    FestivalName = festival?.Name ?? "Unknown Festival",
                    FestivalDate = festival?.StartDate ?? DateTime.MinValue,
                    Location = festival?.Location ?? "Unknown Location"
                };
            }
            catch (Exception ex)
            {
                return new BookingResponseDto
                {
                    Success = false,
                    Message = $"Booking update failed: {ex.Message}"
                };
            }
        }

        public async Task<IEnumerable<BookingGetDto>> GetUserBookingsAsync(int userId)
        {
            try
            {
                var bookings = await _bookingRepository.GetAllAsync();
                var festivals = await _festivalRepository.GetAllAsync();

                var userBookings = bookings.Where(b => b.UserId == userId)
                    .Select(b =>
                    {
                        var festival = festivals.FirstOrDefault(f => f.Id == b.FestivalId);
                        return new BookingGetDto
                        {
                            Id = b.Id,
                            FestivalName = festival?.Name ?? "Unknown Festival",
                            FestivalDate = festival?.StartDate ?? DateTime.MinValue,
                            Location = festival?.Location ?? "Unknown Location",
                            TicketType = b.TicketType,
                            Quantity = b.Quantity,
                            Price = b.Price,
                            CustomerName = b.CustomerName,
                            CustomerEmail = b.CustomerEmail,
                            TicketNumber = b.TicketNumber,
                            BookingDate = b.BookingDate,
                            IncludeCamping = b.IncludeCamping,
                            IncludeFoodPackage = b.IncludeFoodPackage,
                            IncludeTransportation = b.IncludeTransportation,
                            Status = b.Status
                        };
                    });

                return userBookings;
            }
            catch
            {
                return new List<BookingGetDto>();
            }
        }

        public async Task<BookingGetDto?> GetBookingByIdAsync(int bookingId)
        {
            try
            {
                var bookings = await _bookingRepository.GetAllAsync();
                var festivals = await _festivalRepository.GetAllAsync();

                var booking = bookings.FirstOrDefault(b => b.Id == bookingId);
                if (booking == null) return null;

                var festival = festivals.FirstOrDefault(f => f.Id == booking.FestivalId);
                return new BookingGetDto
                {
                    Id = booking.Id,
                    FestivalName = festival?.Name ?? "Unknown Festival",
                    FestivalDate = festival?.StartDate ?? DateTime.MinValue,
                    Location = festival?.Location ?? "Unknown Location",
                    TicketType = booking.TicketType,
                    Quantity = booking.Quantity,
                    Price = booking.Price,
                    CustomerName = booking.CustomerName,
                    CustomerEmail = booking.CustomerEmail,
                    TicketNumber = booking.TicketNumber,
                    BookingDate = booking.BookingDate,
                    IncludeCamping = booking.IncludeCamping,
                    IncludeFoodPackage = booking.IncludeFoodPackage,
                    IncludeTransportation = booking.IncludeTransportation,
                    Status = booking.Status
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CancelBookingAsync(int bookingId)
        {
            try
            {
                var bookings = await _bookingRepository.GetAllAsync();
                var booking = bookings.FirstOrDefault(b => b.Id == bookingId);

                if (booking == null) return false;

                booking.Status = "Cancelled";
                booking.CancellationDate = DateTime.Now;

                await _bookingRepository.UpdateAsync(booking);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private decimal GetTicketPrice(string ticketType)
        {
            return ticketType.ToLower() switch
            {
                "vip" => 299.99m,
                "premium" => 199.99m,
                "standard" => 99.99m,
                "early bird" => 79.99m,
                _ => 99.99m
            };
        }

        private string GenerateTicketNumber()
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
            var hash = Convert.ToBase64String(bytes).Replace("/", "").Replace("+", "").Replace("=", "");
            return $"ST-{hash.Substring(0, 8).ToUpper()}";
        }
    }
}