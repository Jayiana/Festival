using ShowTime_BusinessLogic.Dtos;

namespace ShowTime_BusinessLogic.Abstractions
{
    public interface ILineupService
    {
        Task<IList<LineupGetDto>> GetAllAsync();
        Task<LineupGetDto> GetAsync(int festivalId, int artistId);
        Task AddAsync(LineupCreateDto dto);
        Task UpdateAsync(int festivalId, int artistId, LineupUpdateDto dto);
        Task DeleteAsync(int festivalId, int artistId);
    }
}
