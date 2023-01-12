namespace Titan.Models.Account
{
    // transport class used to send token+password to API for password reset
    public class TokenChangePasswordRequest
    {
        public string Token { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
