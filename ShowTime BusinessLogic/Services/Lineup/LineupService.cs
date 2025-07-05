using ShowTime.DataAccess.GenericRepository;
using ShowTime.DataAccess.Models.ArtistInfo;
using ShowTime.DataAccess.Models.FestivalInfo;
using ShowTime.DataAccess.Models.LineupInfo;
using ShowTime_BusinessLogic.Abstractions;
using ShowTime_BusinessLogic.Dtos;

namespace ShowTime_BusinessLogic.Services
{
    public class LineupService : ILineupService
    {
        private readonly IRepository<Lineup> _lineupRepository;
        private readonly IRepository<Festival> _festivalRepository;
        private readonly IRepository<Artist> _artistRepository;

        public LineupService(IRepository<Lineup> lineupRepository,
                     IRepository<Festival> festivalRepository,
                     IRepository<Artist> artistRepository)
        {
            _lineupRepository = lineupRepository;
            _festivalRepository = festivalRepository;
            _artistRepository = artistRepository;
        }

        public async Task<IList<LineupGetDto>> GetAllAsync()
        {
            var lineups = await _lineupRepository.GetAllAsync(x => x.Festival, y => y.Artist);
            return lineups.Select(l => new LineupGetDto
            {
                FestivalId = l.FestivalId,
                ArtistId = l.ArtistId,
                Stage = l.Stage,
                StartTime = l.StartTime,
                FestivalName = l.Festival.Name,
                ArtistName = l.Artist.Name,
                IsMainStage = l.IsMainStage,
                IsLivePerformance = l.IsLivePerformance,
                Description = l.Description,
                StageTheme = l.StageTheme
            }).ToList();
        }

        public async Task<LineupGetDto?> GetAsync(int festivalId, int artistId)
        {
            var lineup = await _lineupRepository.GetByIdsAsync(festivalId, artistId, x => x.Festival, y => y.Artist);
            if (lineup == null) return null;

            return new LineupGetDto
            {
                FestivalId = lineup.FestivalId,
                ArtistId = lineup.ArtistId,
                Stage = lineup.Stage,
                StartTime = lineup.StartTime,
                FestivalName = lineup.Festival.Name,
                ArtistName = lineup.Artist.Name,
                IsMainStage = lineup.IsMainStage,
                IsLivePerformance = lineup.IsLivePerformance,
                Description = lineup.Description,
                StageTheme = lineup.StageTheme
            };
        }

        public async Task AddAsync(LineupCreateDto dto)
        {
            if (dto.FestivalId <= 0)
                throw new ArgumentException("Festival ID must be a positive integer.");

            if (dto.ArtistId <= 0)
                throw new ArgumentException("Artist ID must be a positive integer.");

            var festival = await _festivalRepository.GetByIdAsync(dto.FestivalId);
            if (festival == null)
                throw new ArgumentException("Festival ID does not exist.");

            var artist = await _artistRepository.GetByIdAsync(dto.ArtistId);
            if (artist == null)
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
                StartTime = dto.StartTime,
                IsMainStage = dto.IsMainStage,
                IsLivePerformance = dto.IsLivePerformance,
                Description = dto.Description,
                StageTheme = dto.StageTheme
            };

            await _lineupRepository.AddAsync(entity);
        }


        public async Task UpdateAsync(int festivalId, int artistId, LineupUpdateDto dto)
        {
            var entity = await _lineupRepository.GetByIdsAsync(artistId, festivalId);

            if (entity == null)
                throw new InvalidOperationException("The lineup to update was not found.");

            entity.Stage = dto.Stage;
            entity.StartTime = dto.StartTime;
            entity.IsMainStage = dto.IsMainStage;
            entity.IsLivePerformance = dto.IsLivePerformance;
            entity.Description = dto.Description;
            entity.StageTheme = dto.StageTheme;

            await _lineupRepository.UpdateAsync(entity);
        }


        public async Task DeleteAsync(int festivalId, int artistId)
        {
            await _lineupRepository.DeleteByIdsAsync(festivalId, artistId);
        }
    }
}
