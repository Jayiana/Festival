using ShowTime.DataAccess;
using ShowTime_BusinessLogic.Dtos.Accommodation;

namespace ShowTime.BusinessLogic.Services
{
    public class AccommodationService
    {
        private readonly IRepository<Accommodation> _accommodationRepository;
        private readonly IRepository<Festival> _festivalRepository;
        private readonly ShowTimeDbContext _dbContext;

        public AccommodationService(IRepository<Accommodation> accommodationRepository, IRepository<Festival> festivalRepository, ShowTimeDbContext dbContext)
        {
            _accommodationRepository = accommodationRepository;
            _festivalRepository = festivalRepository;
            _dbContext = dbContext;
        }

        public async Task<List<AccommodationTypeInfoDto>> GetAccommodationTypesAsync()
        {
            var entities = await Task.FromResult(_dbContext.AccommodationTypeInfo.ToList());
            return entities.Select(e => new AccommodationTypeInfoDto
            {
                Id = e.Id,
                Type = e.Type,
                Name = e.Name,
                Description = e.Description,
                Icon = e.Icon,
                BasePrice = e.BasePrice,
                Features = e.FeaturesSerialized?.Split(',', System.StringSplitOptions.RemoveEmptyEntries | System.StringSplitOptions.TrimEntries) ?? new string[0]
            }).ToList();
        }
    }
} 