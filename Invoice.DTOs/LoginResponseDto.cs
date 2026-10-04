using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.DTOs;

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public UsersDto User { get; set; } = new();

}
