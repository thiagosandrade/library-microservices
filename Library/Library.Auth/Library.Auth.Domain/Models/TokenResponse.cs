using System;

namespace Library.Auth.Domain.Models
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime? Expiration { get; set; }
    }
}
