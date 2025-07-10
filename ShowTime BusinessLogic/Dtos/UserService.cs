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
                Role = user.Role,
                UserId = user.Id 
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

        public class UserProfileDto
        {
            public int Id { get; set; }
            public string? FullName { get; set; }
            public string Email { get; set; }
            public string? ProfilePictureUrl { get; set; }
            public byte[]? ProfilePictureData { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Address { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public string? Bio { get; set; }
            public string? Instagram { get; set; }
            public string? Facebook { get; set; }
        }

        public async Task<UserProfileDto?> GetUserProfileAsync(int userId)
        {
            var user = (await userRepository.GetAllAsync()).FirstOrDefault(u => u.Id == userId);
            if (user == null) return null;
            return new UserProfileDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                ProfilePictureData = user.ProfilePictureData,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                DateOfBirth = user.DateOfBirth,
                Bio = user.Bio,
                Instagram = user.Instagram,
                Facebook = user.Facebook
            };
        }

        public async Task<bool> UpdateUserProfileAsync(int userId, UserProfileDto profile)
        {
            var user = (await userRepository.GetAllAsync()).FirstOrDefault(u => u.Id == userId);
            if (user == null) return false;
            user.FullName = profile.FullName;
            user.ProfilePictureData = profile.ProfilePictureData;
            user.PhoneNumber = profile.PhoneNumber;
            user.Address = profile.Address;
            user.DateOfBirth = profile.DateOfBirth;
            user.Bio = profile.Bio;
            user.Instagram = profile.Instagram;
            user.Facebook = profile.Facebook;
            await userRepository.UpdateAsync(user);
            return true;
        }
    }
}
