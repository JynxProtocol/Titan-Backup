using System;

namespace Titan.Models.Authentication
{
    public class JWTResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string Name { get; set; }
    }
}
