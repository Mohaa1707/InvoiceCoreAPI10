namespace InvoiceCoreAPI.DTO
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public UsersDto User { get; set; } = new();
    }
}
