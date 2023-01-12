using System;
using System.Collections.Generic;

namespace Titan.Models.Account
{
    // transport class used to recieve to the JWT+expiration, user claims
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }
}
