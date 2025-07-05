using ShowTime.DataAccess.Models;
using ShowTime_BusinessLogic.Dtos.Artist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime_BusinessLogic.Abstractions
{
    public interface IArtistService
    {
        Task<ArtistGetDto> GetByIdAsync(int id);
        Task<IList<ArtistGetDto>> GetAllAsync();
        Task AddAsync(ArtistCreateDto artistCreateDto);
        Task UpdateAsync(int id, ArtistUpdateDto obj);
        Task DeleteAsync(int id);
    }
}
