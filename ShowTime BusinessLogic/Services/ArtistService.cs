using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.GenericRepository;
using ShowTime.DataAccess.Models.ArtistInfo;
using ShowTime_BusinessLogic.Abstractions;
using ShowTime_BusinessLogic.Dtos.Artist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime_BusinessLogic.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IRepository<Artist> _artistRepository;

        public ArtistService(IRepository<Artist> artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<ArtistGetDto> GetByIdAsync(int id)
        {
            try
            {
                var artist = await _artistRepository.GetByIdAsync(id);

                return new ArtistGetDto
                {
                    Id = artist.Id,
                    Name = artist.Name,
                    Genre = artist.Genre,
                    Image = artist.Image,
                    Rating = artist.Rating,
                    IsTrending = artist.IsTrending,
                    TrendingReason = artist.TrendingReason,
                    Origin = artist.Origin,
                    Description = artist.Description,
                    Instagram = artist.Instagram,
                    YouTube = artist.YouTube,
                    FansCount = artist.FansCount,
                    DebutYear = artist.DebutYear,
                    Category = artist.Category
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while retrieving the artist", ex);
            }
        }


        public async Task<IList<ArtistGetDto>> GetAllAsync()
        {
            try
            {
                var artists = await _artistRepository.GetAllAsync();
                return artists.Select(artist => new ArtistGetDto
                {
                    Id = artist.Id,
                    Name = artist.Name,
                    Image = artist.Image,
                    Genre = artist.Genre,
                    Rating = artist.Rating,
                    IsTrending = artist.IsTrending,
                    TrendingReason = artist.TrendingReason,
                    Origin = artist.Origin,
                    Description = artist.Description,
                    Instagram = artist.Instagram,
                    YouTube = artist.YouTube,
                    FansCount = artist.FansCount,
                    DebutYear = artist.DebutYear,
                    Category = artist.Category
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all artists", ex);
            }
        }


        public async Task AddAsync(ArtistCreateDto obj)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(obj.Name))
                    throw new ArgumentException("Artist name is required.");

                if (obj.Name.All(char.IsDigit))
                    throw new ArgumentException("Artist name cannot be only numbers.");

                if (obj.Name.Length < 2)
                    throw new ArgumentException("Artist name must be at least 2 characters.");

                if (string.IsNullOrWhiteSpace(obj.Genre))
                    throw new ArgumentException("Genre is required.");

                if (obj.Genre.Length < 2)
                    throw new ArgumentException("Genre must be at least 2 characters.");

                if (!obj.Genre.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || "-&".Contains(c)))
                    throw new ArgumentException("Genre contains invalid characters.");

                if (string.IsNullOrWhiteSpace(obj.Image) || !Uri.TryCreate(obj.Image, UriKind.Absolute, out var _))
                    throw new ArgumentException("Image must be a valid URL.");

                var allArtists = await _artistRepository.GetAllAsync();
                if (allArtists.Any(a => a.Name.ToLower() == obj.Name.ToLower()))
                    throw new InvalidOperationException("An artist with this name already exists.");

                var artist = new Artist
                {
                    Name = obj.Name,
                    Genre = obj.Genre,
                    Image = obj.Image,
                    Rating = obj.Rating,
                    IsTrending = obj.IsTrending,
                    TrendingReason = obj.TrendingReason,
                    Origin = obj.Origin,
                    Description = obj.Description,
                    Instagram = obj.Instagram,
                    YouTube = obj.YouTube,
                    FansCount = obj.FansCount,
                    DebutYear = obj.DebutYear,
                    Category = obj.Category
                };

                await _artistRepository.AddAsync(artist);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the artist", ex);
            }
        }



        public async Task UpdateAsync(int id, ArtistUpdateDto obj)
        {
            try
            {
                var artist = await _artistRepository.GetByIdAsync(id);

                if (artist == null)
                {
                    throw new Exception($"Artist with Id {id} not found!");
                }

                artist.Name = obj.Name;
                artist.Genre = obj.Genre;
                artist.Image = obj.Image;
                artist.Rating = obj.Rating;
                artist.IsTrending = obj.IsTrending;
                artist.TrendingReason = obj.TrendingReason;
                artist.Origin = obj.Origin;
                artist.Description = obj.Description;
                artist.Instagram = obj.Instagram;
                artist.YouTube = obj.YouTube;
                artist.FansCount = obj.FansCount;
                artist.DebutYear = obj.DebutYear;
                artist.Category = obj.Category;

                await _artistRepository.UpdateAsync(artist);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while updating the artist", ex);
            }
        }


        public async Task DeleteAsync(int id)
        {
            try
            {
                var artist = await _artistRepository.GetByIdAsync(id);

                if (artist == null)
                {
                    throw new Exception($"Artist with ID {id} not found.");
                }

                await _artistRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the artist", ex);
            }
        }

    }
}
