namespace ShowTime_BusinessLogic.Dtos.Authentication.Login
{
    public class LoginResponseDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int? Role { get; set; }
    }
}
