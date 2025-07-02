using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.GenericRepository;
using ShowTime_BusinessLogic.Abstractions;
using ShowTime_BusinessLogic.Dtos;

namespace ShowTime_BusinessLogic.Services
{
    public class LineupService : ILineupService
    {
        private readonly IRepository<Lineup> _lineupRepository;

        public LineupService(IRepository<Lineup> lineupRepository)
        {
            _lineupRepository = lineupRepository;
        }

        public async Task<IList<LineupGetDto>> GetAllAsync()
        {
            var lineups = await _lineupRepository.GetAllAsync(x => x.Festival, x => x.Artist);
            return lineups.Select(l => new LineupGetDto
            {
                FestivalId = l.FestivalId,
                ArtistId = l.ArtistId,
                Stage = l.Stage,
                StartTime = l.StartTime,
                FestivalName = l.Festival.Name,
                ArtistName = l.Artist.Name
            }).ToList();
        }

        public async Task<LineupGetDto> GetAsync(int festivalId, int artistId)
        {
            var lineup = await _lineupRepository.GetByIdsAsync(festivalId, artistId, x => x.Festival, x => x.Artist);
            return new LineupGetDto
            {
                FestivalId = lineup.FestivalId,
                ArtistId = lineup.ArtistId,
                Stage = lineup.Stage,
                StartTime = lineup.StartTime,
                FestivalName = lineup.Festival.Name,
                ArtistName = lineup.Artist.Name
            };
        }

        public async Task AddAsync(LineupCreateDto dto)
        {
            var entity = new Lineup
            {
                FestivalId = dto.FestivalId,
                ArtistId = dto.ArtistId,
                Stage = dto.Stage,
                StartTime = dto.StartTime
            };
            await _lineupRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(int festivalId, int artistId, LineupUpdateDto dto)
        {
            var entity = await _lineupRepository.GetByIdsAsync(festivalId, artistId);
            entity.Stage = dto.Stage;
            entity.StartTime = dto.StartTime;
            await _lineupRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int festivalId, int artistId)
        {
            await _lineupRepository.DeleteByIdsAsync(festivalId, artistId);
        }
    }
}