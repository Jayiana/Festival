using ShowTime_BusinessLogic.Abstractions;
using ShowTime_BusinessLogic.Dtos.Accommodation;
using ShowTime.DataAccess.GenericRepository;
using ShowTime.DataAccess.Models.AccommodationInfo;
using ShowTime.DataAccess.Models.FestivalInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowTime_BusinessLogic.Services
{
    public class AccommodationService : IAccommodationService
    {
        private readonly IRepository<Accommodation> _accommodationRepository;
        private readonly IRepository<Festival> _festivalRepository;

        public AccommodationService(IRepository<Accommodation> accommodationRepository, IRepository<Festival> festivalRepository)
        {
            _accommodationRepository = accommodationRepository;
            _festivalRepository = festivalRepository;
        }

        public async Task<AccommodationResponseDto> CreateAccommodationAsync(AccommodationCreateDto accommodationDto)
        {
            try
            {
                // Validate festival exists
                var festival = await _festivalRepository.GetByIdAsync(accommodationDto.FestivalId);
                if (festival == null)
                {
                    return new AccommodationResponseDto
                    {
                        Success = false,
                        Message = "Festival not found"
                    };
                }

                // Calculate pricing based on accommodation type
                var (pricePerNight, amenities) = CalculateAccommodationPricing(accommodationDto.AccommodationType, accommodationDto.RoomType);
                
                // Calculate total price
                var totalPrice = pricePerNight * accommodationDto.NumberOfNights;
                
                // Add additional services
                if (accommodationDto.IncludeBreakfast) totalPrice += 25 * accommodationDto.NumberOfNights;
                if (accommodationDto.IncludeDinner) totalPrice += 35 * accommodationDto.NumberOfNights;
                if (accommodationDto.IncludeShuttleService) totalPrice += 15 * accommodationDto.NumberOfNights;
                if (accommodationDto.IncludeCleaningService) totalPrice += 20 * accommodationDto.NumberOfNights;
                if (accommodationDto.IncludeEquipmentRental) totalPrice += 50;

                // Generate confirmation number
                var confirmationNumber = GenerateConfirmationNumber();

                var accommodation = new Accommodation
                {
                    FestivalId = accommodationDto.FestivalId,
                    UserId = null, // Will be set when user authentication is implemented
                    AccommodationType = accommodationDto.AccommodationType,
                    RoomType = accommodationDto.RoomType,
                    Capacity = accommodationDto.Capacity,
                    PricePerNight = pricePerNight,
                    NumberOfNights = accommodationDto.NumberOfNights,
                    TotalPrice = totalPrice,
                    Location = GetLocationForType(accommodationDto.AccommodationType),
                    HasShower = amenities.HasShower,
                    HasElectricity = amenities.HasElectricity,
                    HasWiFi = amenities.HasWiFi,
                    HasParking = amenities.HasParking,
                    HasSecurity = amenities.HasSecurity,
                    HasFoodService = amenities.HasFoodService,
                    HasTransportation = amenities.HasTransportation,
                    CustomerName = accommodationDto.CustomerName,
                    CustomerEmail = accommodationDto.CustomerEmail,
                    CustomerPhone = accommodationDto.CustomerPhone,
                    SpecialRequests = accommodationDto.SpecialRequests,
                    CheckInDate = accommodationDto.CheckInDate,
                    CheckOutDate = accommodationDto.CheckOutDate,
                    IncludeBreakfast = accommodationDto.IncludeBreakfast,
                    IncludeDinner = accommodationDto.IncludeDinner,
                    IncludeShuttleService = accommodationDto.IncludeShuttleService,
                    IncludeCleaningService = accommodationDto.IncludeCleaningService,
                    IncludeEquipmentRental = accommodationDto.IncludeEquipmentRental,
                    Status = "Confirmed",
                    PaymentStatus = "Paid",
                    TransactionId = Guid.NewGuid().ToString(),
                    ConfirmationDate = DateTime.Now
                };

                await _accommodationRepository.AddAsync(accommodation);

                return new AccommodationResponseDto
                {
                    Success = true,
                    Message = "Accommodation booked successfully!",
                    BookingId = accommodation.Id.ToString(),
                    ConfirmationNumber = confirmationNumber,
                    TotalPrice = totalPrice,
                    FestivalName = festival.Name,
                    FestivalDate = festival.StartDate,
                    Location = accommodation.Location,
                    CheckInDate = accommodation.CheckInDate,
                    CheckOutDate = accommodation.CheckOutDate,
                    AccommodationType = accommodation.AccommodationType,
                    RoomType = accommodation.RoomType
                };
            }
            catch (Exception ex)
            {
                return new AccommodationResponseDto
                {
                    Success = false,
                    Message = $"Error creating accommodation: {ex.Message}"
                };
            }
        }

        public async Task<AccommodationResponseDto> UpdateAccommodationAsync(AccommodationUpdateDto accommodationDto)
        {
            try
            {
                var existingAccommodation = await _accommodationRepository.GetByIdAsync(accommodationDto.Id);
                if (existingAccommodation == null)
                {
                    return new AccommodationResponseDto
                    {
                        Success = false,
                        Message = "Accommodation booking not found"
                    };
                }

                // Recalculate pricing
                var (pricePerNight, amenities) = CalculateAccommodationPricing(accommodationDto.AccommodationType, accommodationDto.RoomType);
                var totalPrice = pricePerNight * accommodationDto.NumberOfNights;
                
                if (accommodationDto.IncludeBreakfast) totalPrice += 25 * accommodationDto.NumberOfNights;
                if (accommodationDto.IncludeDinner) totalPrice += 35 * accommodationDto.NumberOfNights;
                if (accommodationDto.IncludeShuttleService) totalPrice += 15 * accommodationDto.NumberOfNights;
                if (accommodationDto.IncludeCleaningService) totalPrice += 20 * accommodationDto.NumberOfNights;
                if (accommodationDto.IncludeEquipmentRental) totalPrice += 50;

                // Update accommodation
                existingAccommodation.AccommodationType = accommodationDto.AccommodationType;
                existingAccommodation.RoomType = accommodationDto.RoomType;
                existingAccommodation.Capacity = accommodationDto.Capacity;
                existingAccommodation.PricePerNight = pricePerNight;
                existingAccommodation.NumberOfNights = accommodationDto.NumberOfNights;
                existingAccommodation.TotalPrice = totalPrice;
                existingAccommodation.CustomerName = accommodationDto.CustomerName;
                existingAccommodation.CustomerEmail = accommodationDto.CustomerEmail;
                existingAccommodation.CustomerPhone = accommodationDto.CustomerPhone;
                existingAccommodation.SpecialRequests = accommodationDto.SpecialRequests;
                existingAccommodation.CheckInDate = accommodationDto.CheckInDate;
                existingAccommodation.CheckOutDate = accommodationDto.CheckOutDate;
                existingAccommodation.IncludeBreakfast = accommodationDto.IncludeBreakfast;
                existingAccommodation.IncludeDinner = accommodationDto.IncludeDinner;
                existingAccommodation.IncludeShuttleService = accommodationDto.IncludeShuttleService;
                existingAccommodation.IncludeCleaningService = accommodationDto.IncludeCleaningService;
                existingAccommodation.IncludeEquipmentRental = accommodationDto.IncludeEquipmentRental;
                existingAccommodation.Status = accommodationDto.Status;
                existingAccommodation.PaymentStatus = accommodationDto.PaymentStatus;
                existingAccommodation.TransactionId = accommodationDto.TransactionId;

                await _accommodationRepository.UpdateAsync(existingAccommodation);

                return new AccommodationResponseDto
                {
                    Success = true,
                    Message = "Accommodation updated successfully!",
                    BookingId = existingAccommodation.Id.ToString(),
                    TotalPrice = totalPrice
                };
            }
            catch (Exception ex)
            {
                return new AccommodationResponseDto
                {
                    Success = false,
                    Message = $"Error updating accommodation: {ex.Message}"
                };
            }
        }

        public async Task<IEnumerable<AccommodationGetDto>> GetUserAccommodationsAsync(int userId)
        {
            var accommodations = await _accommodationRepository.GetAllAsync();
            var userAccommodations = accommodations.Where(a => a.UserId.HasValue && a.UserId.Value == userId);

            return userAccommodations.Select(a => new AccommodationGetDto
            {
                Id = a.Id,
                FestivalName = a.Festival?.Name ?? "",
                FestivalDate = a.Festival?.StartDate ?? DateTime.MinValue,
                FestivalLocation = a.Festival?.Location ?? "",
                AccommodationType = a.AccommodationType,
                RoomType = a.RoomType,
                Capacity = a.Capacity,
                PricePerNight = a.PricePerNight,
                NumberOfNights = a.NumberOfNights,
                TotalPrice = a.TotalPrice,
                Location = a.Location,
                HasShower = a.HasShower,
                HasElectricity = a.HasElectricity,
                HasWiFi = a.HasWiFi,
                HasParking = a.HasParking,
                HasSecurity = a.HasSecurity,
                HasFoodService = a.HasFoodService,
                HasTransportation = a.HasTransportation,
                CustomerName = a.CustomerName,
                CustomerEmail = a.CustomerEmail,
                CustomerPhone = a.CustomerPhone,
                SpecialRequests = a.SpecialRequests,
                CheckInDate = a.CheckInDate,
                CheckOutDate = a.CheckOutDate,
                Status = a.Status,
                BookingDate = a.BookingDate,
                ConfirmationDate = a.ConfirmationDate,
                CancellationDate = a.CancellationDate,
                IncludeBreakfast = a.IncludeBreakfast,
                IncludeDinner = a.IncludeDinner,
                IncludeShuttleService = a.IncludeShuttleService,
                IncludeCleaningService = a.IncludeCleaningService,
                IncludeEquipmentRental = a.IncludeEquipmentRental,
                PaymentStatus = a.PaymentStatus,
                TransactionId = a.TransactionId
            });
        }

        public async Task<AccommodationGetDto?> GetAccommodationByIdAsync(int accommodationId)
        {
            var accommodation = await _accommodationRepository.GetByIdAsync(accommodationId);
            if (accommodation == null) return null;

            return new AccommodationGetDto
            {
                Id = accommodation.Id,
                FestivalName = accommodation.Festival?.Name ?? "",
                FestivalDate = accommodation.Festival?.StartDate ?? DateTime.MinValue,
                FestivalLocation = accommodation.Festival?.Location ?? "",
                AccommodationType = accommodation.AccommodationType,
                RoomType = accommodation.RoomType,
                Capacity = accommodation.Capacity,
                PricePerNight = accommodation.PricePerNight,
                NumberOfNights = accommodation.NumberOfNights,
                TotalPrice = accommodation.TotalPrice,
                Location = accommodation.Location,
                HasShower = accommodation.HasShower,
                HasElectricity = accommodation.HasElectricity,
                HasWiFi = accommodation.HasWiFi,
                HasParking = accommodation.HasParking,
                HasSecurity = accommodation.HasSecurity,
                HasFoodService = accommodation.HasFoodService,
                HasTransportation = accommodation.HasTransportation,
                CustomerName = accommodation.CustomerName,
                CustomerEmail = accommodation.CustomerEmail,
                CustomerPhone = accommodation.CustomerPhone,
                SpecialRequests = accommodation.SpecialRequests,
                CheckInDate = accommodation.CheckInDate,
                CheckOutDate = accommodation.CheckOutDate,
                Status = accommodation.Status,
                BookingDate = accommodation.BookingDate,
                ConfirmationDate = accommodation.ConfirmationDate,
                CancellationDate = accommodation.CancellationDate,
                IncludeBreakfast = accommodation.IncludeBreakfast,
                IncludeDinner = accommodation.IncludeDinner,
                IncludeShuttleService = accommodation.IncludeShuttleService,
                IncludeCleaningService = accommodation.IncludeCleaningService,
                IncludeEquipmentRental = accommodation.IncludeEquipmentRental,
                PaymentStatus = accommodation.PaymentStatus,
                TransactionId = accommodation.TransactionId
            };
        }

        public async Task<bool> CancelAccommodationAsync(int accommodationId)
        {
            var accommodation = await _accommodationRepository.GetByIdAsync(accommodationId);
            if (accommodation == null) return false;

            accommodation.Status = "Cancelled";
            accommodation.CancellationDate = DateTime.Now;
            accommodation.PaymentStatus = "Refunded";

            await _accommodationRepository.UpdateAsync(accommodation);
            return true;
        }

        public async Task<IEnumerable<AccommodationGetDto>> GetAvailableAccommodationsAsync(int festivalId)
        {
            var accommodations = await _accommodationRepository.GetAllAsync();
            var availableAccommodations = accommodations.Where(a => a.FestivalId == festivalId && a.Status == "Confirmed");

            return availableAccommodations.Select(a => new AccommodationGetDto
            {
                Id = a.Id,
                FestivalName = a.Festival?.Name ?? "",
                FestivalDate = a.Festival?.StartDate ?? DateTime.MinValue,
                FestivalLocation = a.Festival?.Location ?? "",
                AccommodationType = a.AccommodationType,
                RoomType = a.RoomType,
                Capacity = a.Capacity,
                PricePerNight = a.PricePerNight,
                NumberOfNights = a.NumberOfNights,
                TotalPrice = a.TotalPrice,
                Location = a.Location,
                HasShower = a.HasShower,
                HasElectricity = a.HasElectricity,
                HasWiFi = a.HasWiFi,
                HasParking = a.HasParking,
                HasSecurity = a.HasSecurity,
                HasFoodService = a.HasFoodService,
                HasTransportation = a.HasTransportation,
                CustomerName = a.CustomerName,
                CustomerEmail = a.CustomerEmail,
                CustomerPhone = a.CustomerPhone,
                SpecialRequests = a.SpecialRequests,
                CheckInDate = a.CheckInDate,
                CheckOutDate = a.CheckOutDate,
                Status = a.Status,
                BookingDate = a.BookingDate,
                ConfirmationDate = a.ConfirmationDate,
                CancellationDate = a.CancellationDate,
                IncludeBreakfast = a.IncludeBreakfast,
                IncludeDinner = a.IncludeDinner,
                IncludeShuttleService = a.IncludeShuttleService,
                IncludeCleaningService = a.IncludeCleaningService,
                IncludeEquipmentRental = a.IncludeEquipmentRental,
                PaymentStatus = a.PaymentStatus,
                TransactionId = a.TransactionId
            });
        }

        private (decimal pricePerNight, Amenities amenities) CalculateAccommodationPricing(string accommodationType, string roomType)
        {
            var basePrice = accommodationType.ToLower() switch
            {
                "hotel" => 150m,
                "camping" => 30m,
                "rv" => 80m,
                "glamping" => 200m,
                "hostel" => 60m,
                "apartment" => 120m,
                _ => 100m
            };

            var roomMultiplier = roomType.ToLower() switch
            {
                "single" => 1.0m,
                "double" => 1.5m,
                "suite" => 2.5m,
                "family" => 2.0m,
                "luxury" => 3.0m,
                _ => 1.0m
            };

            var pricePerNight = basePrice * roomMultiplier;

            var amenities = new Amenities
            {
                HasShower = accommodationType.ToLower() != "camping",
                HasElectricity = accommodationType.ToLower() != "camping",
                HasWiFi = accommodationType.ToLower() == "hotel" || accommodationType.ToLower() == "apartment",
                HasParking = true,
                HasSecurity = accommodationType.ToLower() == "hotel" || accommodationType.ToLower() == "apartment",
                HasFoodService = accommodationType.ToLower() == "hotel",
                HasTransportation = accommodationType.ToLower() == "hotel" || accommodationType.ToLower() == "apartment"
            };

            return (pricePerNight, amenities);
        }

        private string GetLocationForType(string accommodationType)
        {
            return accommodationType.ToLower() switch
            {
                "hotel" => "On-site Premium Hotel",
                "camping" => "Festival Grounds Camping Area",
                "rv" => "RV Parking Zone",
                "glamping" => "Luxury Glamping Village",
                "hostel" => "Nearby Hostel",
                "apartment" => "Off-site Apartments",
                _ => "Festival Accommodation"
            };
        }

        private string GenerateConfirmationNumber()
        {
            return $"ACC{DateTime.Now:yyyyMMdd}{new Random().Next(1000, 9999)}";
        }

        private class Amenities
        {
            public bool HasShower { get; set; }
            public bool HasElectricity { get; set; }
            public bool HasWiFi { get; set; }
            public bool HasParking { get; set; }
            public bool HasSecurity { get; set; }
            public bool HasFoodService { get; set; }
            public bool HasTransportation { get; set; }
        }
    }
} 