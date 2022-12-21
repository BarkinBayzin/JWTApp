namespace JWTAppBackOffice.Core.Application.DTOs
{
    public class CheckUserResponseDto
    {
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int Id { get; set; }
        public bool IsExist { get; set; } //result pattern uygulamaya çalışıyorum
    }
}
