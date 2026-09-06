namespace InvoiceCoreAPI.DTO
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public UsersDto User { get; set; }
    }
}
