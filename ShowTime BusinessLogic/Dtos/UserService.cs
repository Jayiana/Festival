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
            try
            {
                Console.WriteLine($"Attempting login for email: {loginDto.Email}");
                var users = await userRepository.GetAllAsync();
                Console.WriteLine($"Retrieved {users.Count()} users from database");
                
                var user = users.FirstOrDefault(u => u.Email == loginDto.Email);

                if (user == null)
                {
                    Console.WriteLine($"User not found for email: {loginDto.Email}");
                    return new LoginResponseDto
                    {
                        Success = false,
                        Message = "Email doesn't exist."
                    };
                }

                Console.WriteLine($"User found with role: {user.Role}");
                string hashedPassword = HashPassword(loginDto.Password);
                if (user.Password != hashedPassword)
                {
                    Console.WriteLine("Password mismatch");
                    return new LoginResponseDto
                    {
                        Success = false,
                        Message = "Incorrect password."
                    };
                }

                Console.WriteLine("Login successful");
                return new LoginResponseDto
                {
                    Success = true,
                    Role = user.Role
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
                return new LoginResponseDto
                {
                    Success = false,
                    Message = $"Authentication error: {ex.Message}"
                };
            }
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
                Role = 1  
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
