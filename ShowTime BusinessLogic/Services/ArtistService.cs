using Microsoft.EntityFrameworkCore;
using ShowTime.BusinessLogic;
using ShowTime.DataAccess.GenericRepository;
using ShowTime.DataAccess.Models;
using ShowTime_BusinessLogic.Abstractions;
using ShowTime_BusinessLogic.Dtos;
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
                    Image = artist.Image,
                    Genre = artist.Genre
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
                    Genre = artist.Genre
                }
                ).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while retrieving all artists", ex);
            }
        }

        public async Task AddAsync(ArtistCreateDto obj)
        {
            try
            {
                var artist = new Artist
                {
                    Name = obj.Name,
                    Image = obj.Image,
                    Genre = obj.Genre
                };

                await _artistRepository.AddAsync(artist);
            }
            catch(Exception ex) 
            {
                    throw new Exception("An error occured while adding the artist", ex);
            }
        }

        public async Task UpdateAsync(int id, ArtistUpdateDto obj)
        {
            try
            {
                var artist = await _artistRepository.GetByIdAsync(id);

                if(artist == null)
                {
                    throw new Exception($"Artist with Id {id} not found!");
                }

                artist.Name = obj.Name;
                artist.Image = obj.Image;
                artist.Genre = obj.Genre;

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
