using ShowTime.DataAccess.GenericRepository;
using ShowTime.DataAccess.Models.UserInfo;
using ShowTime_BusinessLogic.Dtos.Authentication.Login;
using ShowTime_BusinessLogic.Dtos.Authentication.Register;
using ShowTime_BusinessLogic.Services;
using System.Security.Cryptography;
using System.Text;

namespace ShowTime_BusinessLogic.Dtos
{
    public class UserService(IRepository<User> userRepository) : IUserService
    {
        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            var users = await userRepository.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Email == loginDto.Email);

            if (user == null)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Email doesn’t exist."
                };
            }

            string hashedPassword = HashPassword(loginDto.Password);
            if (user.Password != hashedPassword)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Incorrect password."
                };
            }

            return new LoginResponseDto
            {
                Success = true,
                Role = user.Role
            };
        }


        public async Task<RegisterResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var users = await userRepository.GetAllAsync();
            if (users.Any(u => u.Email == registerDto.Email))
            {
                return new RegisterResponseDto
                {
                    Success = false,
                    Message = "Email already registered."
                };
            }

            var newUser = new User
            {
                Email = registerDto.Email,
                Password = HashPassword(registerDto.Password),
                Role = 0 
            };

            await userRepository.AddAsync(newUser);

            return new RegisterResponseDto
            {
                Success = true,
                Message = "Registration successful!"
            };
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
