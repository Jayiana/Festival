using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.GenericRepository;
using ShowTime.DataAccess.Models;
using ShowTime_BusinessLogic.Abstractions;
using ShowTime_BusinessLogic.Dtos;
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
                Capacity = fest.Capacity
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
                Capacity = fest.Capacity
            }).ToList();
        }

        public async Task AddAsync(FestivalCreateDto dto)
        {
            var fest = new Festival
            {
                Name = dto.Name,
                Location = dto.Location,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                SplashArt = dto.SplashArt,
                Capacity = dto.Capacity
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