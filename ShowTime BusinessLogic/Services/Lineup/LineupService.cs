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
            if (dto.FestivalId <= 0)
                throw new ArgumentException("Festival ID must be a positive integer.");

            if (dto.ArtistId <= 0)
                throw new ArgumentException("Artist ID must be a positive integer.");

            var allFestivals = await _lineupRepository.GetAllAsync(x => x.Festival);
            var allArtists = await _lineupRepository.GetAllAsync(x => x.Artist);

            bool festivalExists = allFestivals.Any(l => l.FestivalId == dto.FestivalId);
            bool artistExists = allArtists.Any(l => l.ArtistId == dto.ArtistId);

            if (!festivalExists)
                throw new ArgumentException("Festival ID does not exist.");

            if (!artistExists)
                throw new ArgumentException("Artist ID does not exist.");

            if (dto.StartTime < DateTime.Now)
                throw new ArgumentException("Start time cannot be in the past.");

            if (string.IsNullOrWhiteSpace(dto.Stage) || dto.Stage.All(char.IsDigit))
                throw new ArgumentException("Stage name must include at least one non-numeric character.");

            var existingLineup = await _lineupRepository.GetByIdsAsync(dto.FestivalId, dto.ArtistId);
            if (existingLineup != null)
                throw new InvalidOperationException("This lineup already exists for the given artist and festival.");

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