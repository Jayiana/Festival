using ShowTime_BusinessLogic.Dtos.Accommodation;
using ShowTime.DataAccess.Models.AccommodationInfo;

namespace ShowTime_BusinessLogic.Abstractions.Accommodation
{
    public interface IAccommodationService
    {
        Task<AccommodationResponseDto> CreateAccommodationAsync(AccommodationCreateDto accommodationDto);
        Task<AccommodationResponseDto> UpdateAccommodationAsync(AccommodationUpdateDto accommodationDto);
        Task<IEnumerable<AccommodationGetDto>> GetUserAccommodationsAsync(int userId);
        Task<AccommodationGetDto?> GetAccommodationByIdAsync(int accommodationId);
        Task<bool> CancelAccommodationAsync(int accommodationId);
        Task<IEnumerable<AccommodationGetDto>> GetAvailableAccommodationsAsync(int festivalId);
        Task<List<AccommodationTypeInfoDto>> GetAccommodationTypesAsync();
    }
} 