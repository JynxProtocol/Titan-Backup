using System.Security.Cryptography;


namespace Titan.Functions
{
    // legacy hash handling code
    public class MyHmac
    {
        private const int SaltSize = 32;

        public static byte[] GenerateSalt()
        {
            using var rng = new RNGCryptoServiceProvider();
            var randomNumber = new byte[SaltSize];

            rng.GetBytes(randomNumber);

            return randomNumber;
        }

        public static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
                new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }
    }
}