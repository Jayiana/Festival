using ShowTime.DataAccess.Models.BookingInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Models.UserInfo
{
    public class User
    {
        public int Id { get; set; }
        public string? FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Role { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public byte[]? ProfilePictureData { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Bio { get; set; }
        public string? Instagram { get; set; }
        public string? Facebook { get; set; }
    }

}
