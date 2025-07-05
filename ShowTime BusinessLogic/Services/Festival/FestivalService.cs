using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.GenericRepository;
using ShowTime.DataAccess.Models.FestivalInfo;
using ShowTime_BusinessLogic.Abstractions;
using ShowTime_BusinessLogic.Dtos.Festival;

namespace ShowTime_BusinessLogic.Services
{
    public class FestivalService : IFestivalService
    {
        private readonly IRepository<Festival> _festivalRepository;

        public FestivalService(IRepository<Festival> festivalRepository)
        {
            _festivalRepository = festivalRepository;
        }

        public async Task<FestivalGetDto> GetByIdAsync(int id)
        {
            var fest = await _festivalRepository.GetByIdAsync(id);
            return new FestivalGetDto
            {
                Id = fest.Id,
                Name = fest.Name,
                Location = fest.Location,
                StartDate = fest.StartDate,
                EndDate = fest.EndDate,
                SplashArt = fest.SplashArt,
                Capacity = fest.Capacity,
                Theme = fest.Theme,
                IsIndoor = fest.IsIndoor,
                HasCamping = fest.HasCamping,
                HasFoodCourt = fest.HasFoodCourt,
                HasAfterParty = fest.HasAfterParty,
                Rating = fest.Rating,
                Description = fest.Description
            };
        }

        public async Task<IList<FestivalGetDto>> GetAllAsync()
        {
            var fests = await _festivalRepository.GetAllAsync();
            return fests.Select(fest => new FestivalGetDto
            {
                Id = fest.Id,
                Name = fest.Name,
                Location = fest.Location,
                StartDate = fest.StartDate,
                EndDate = fest.EndDate,
                SplashArt = fest.SplashArt,
                Capacity = fest.Capacity,
                Theme = fest.Theme,
                IsIndoor = fest.IsIndoor,
                HasCamping = fest.HasCamping,
                HasFoodCourt = fest.HasFoodCourt,
                HasAfterParty = fest.HasAfterParty,
                Rating = fest.Rating,
                Description = fest.Description
            }).ToList();
        }

        public async Task AddAsync(FestivalCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Festival name cannot be empty.");

            var existingFest = await _festivalRepository.GetAllAsync();
            if (existingFest.Any(f => f.Name.ToLower() == dto.Name.ToLower()))
                throw new ArgumentException($"A festival named '{dto.Name}' already exists.");

            if (dto.StartDate.Date < DateTime.Today)
                throw new ArgumentException("Start date cannot be in the past.");

            if (dto.EndDate <= dto.StartDate)
                throw new ArgumentException("End date must be after start date.");

            if (string.IsNullOrWhiteSpace(dto.Location))
                throw new ArgumentException("Location cannot be empty.");

            if (dto.Capacity <= 0)
                throw new ArgumentException("Capacity must be a positive number.");

            var fest = new Festival
            {
                Name = dto.Name,
                Location = dto.Location,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                SplashArt = dto.SplashArt,
                Capacity = dto.Capacity,
                Theme = dto.Theme,
                IsIndoor = dto.IsIndoor,
                HasCamping = dto.HasCamping,
                HasFoodCourt = dto.HasFoodCourt,
                HasAfterParty = dto.HasAfterParty,
                Rating = dto.Rating,
                Description = dto.Description
            };

            await _festivalRepository.AddAsync(fest);
        }

        public async Task UpdateAsync(int id, FestivalUpdateDto dto)
        {
            var fest = await _festivalRepository.GetByIdAsync(id);
            if (fest == null)
                throw new Exception("Festival not found");

            fest.Name = dto.Name;
            fest.Location = dto.Location;
            fest.StartDate = dto.StartDate;
            fest.EndDate = dto.EndDate;
            fest.SplashArt = dto.SplashArt;
            fest.Capacity = dto.Capacity;
            fest.Theme = dto.Theme;
            fest.IsIndoor = dto.IsIndoor;
            fest.HasCamping = dto.HasCamping;
            fest.HasFoodCourt = dto.HasFoodCourt;
            fest.HasAfterParty = dto.HasAfterParty;
            fest.Rating = dto.Rating;
            fest.Description = dto.Description;

            await _festivalRepository.UpdateAsync(fest);
        }

        public async Task DeleteAsync(int id)
        {
            var fest = await _festivalRepository.GetByIdAsync(id);
            if (fest == null)
                throw new Exception("Festival not found");

            await _festivalRepository.DeleteAsync(id);
        }
    }
}
