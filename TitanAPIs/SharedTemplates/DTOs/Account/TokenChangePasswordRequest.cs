using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Titan.Models.Account
{
    // transport class used to send token+password to API for password reset
    public class TokenChangePasswordRequest
    {
        public string Token { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
