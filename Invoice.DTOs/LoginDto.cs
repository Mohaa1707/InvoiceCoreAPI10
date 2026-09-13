using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.DTOs
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
