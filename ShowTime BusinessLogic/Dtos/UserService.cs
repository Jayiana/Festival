using ShowTime.DataAccess.GenericRepository;
using ShowTime.DataAccess.Models;
using ShowTime_BusinessLogic.Dtos.Login;
using ShowTime_BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime_BusinessLogic.Dtos
{
    public class UserService(IRepository<User> userRepository) : IUserService
    {
        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            return new LoginResponseDto
            {
                Role = 0
            };
                
        }

    }
}
