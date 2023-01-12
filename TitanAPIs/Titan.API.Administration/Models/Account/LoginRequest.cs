namespace Titan.Models.Account
{
    // transport class used to send user+password to API for login
    public class LoginRequest
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
