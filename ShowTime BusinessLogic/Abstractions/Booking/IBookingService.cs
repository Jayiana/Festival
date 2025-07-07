using ShowTime_BusinessLogic.Dtos.Booking;

namespace ShowTime_BusinessLogic.Abstractions
{
    public interface IBookingService
    {
        Task<BookingResponseDto> CreateBookingAsync(BookingCreateDto bookingDto);
        Task<BookingResponseDto> UpdateBookingAsync(BookingUpdateDto bookingDto);
        Task<IEnumerable<BookingGetDto>> GetUserBookingsAsync(int userId);
        Task<BookingGetDto?> GetBookingByIdAsync(int bookingId);
        Task<bool> CancelBookingAsync(int bookingId);
    }
}