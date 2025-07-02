using ShowTime.BusinessLogic;
using ShowTime_BusinessLogic.Dtos;
using ShowTime_BusinessLogic.Dtos.Festival;

namespace ShowTime_BusinessLogic.Abstractions
{
    public interface IFestivalService
    {
        Task<FestivalGetDto> GetByIdAsync(int id);
        Task<IList<FestivalGetDto>> GetAllAsync();
        Task AddAsync(FestivalCreateDto dto);
        Task UpdateAsync(int id, FestivalUpdateDto dto);
        Task DeleteAsync(int id);
    }
}