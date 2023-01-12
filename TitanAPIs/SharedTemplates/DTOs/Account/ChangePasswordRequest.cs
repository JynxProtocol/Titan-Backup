using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Titan.Models.Account
{
    // transport class used to send password+new password to API for password reset
    public class ChangePasswordRequest
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string NewPassword { get; set; } = "";
    }
}
