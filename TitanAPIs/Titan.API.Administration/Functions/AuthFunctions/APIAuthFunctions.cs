using System;
using System.Linq;
using System.Net.Mail;
using Titan.Data;
using Titan.Models.DBUser;

namespace Titan.Functions
{
    internal static class APIAuthFunctions
    {
        // legacy security log function
        public static void SecurityLog(TitanAuthContext TitanAuth, string User)
        {
            var mylog = new DBUser();

            mylog = TitanAuth.Users.Where(usr => usr.UsrName == User).FirstOrDefault();

            int mycount = mylog.UsrLoginCount;

            mylog.UsrLoginCount = mycount + 1;

            mylog.UsrLastLogin = System.DateTime.UtcNow;

            TitanAuth.SaveChanges();
        }

        // legacy email function
        public static void Email(this SmtpClient SmtpClient, string To, string Subject, string Body)
        {
            try
            {
                var message = new MailMessage
                {
                    IsBodyHtml = true
                };
                message.To.Add(new MailAddress(To));
                message.Subject = Subject;
                message.Body = Body;

                SmtpClient.Send(message);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>Function <c>GeneratePassword</c> generates a secure random password, formatted as base64</summary>
        public static string GeneratePassword()
        {
            return Convert.ToBase64String(GenerateRandomBytes(8)).Replace("=", "");
        }

        /// <summary>Function <c>GeneratePin</c> generates a secure random pin that is unique against an IQueryable</summary>
        public static string GeneratePin(IQueryable<string> InvalidPins)
        {
            string attempt;
            int i = 0;
            do
            {
                attempt = RandomLongBetweenInc(000000, 999999).ToString("D6");
                if (i > 1000)
                {
                    throw new TimeoutException("Could not generate a valid pin due to conflicts");
                }
                i++;
            } while (InvalidPins.Any(x => x == attempt));

            return attempt;
        }

        /// <summary>Function <c>RandomLongBetweenInc</c> generates a secure random integer between <c>min</c> and <c>max</c> inclusive</summary>
        public static long RandomLongBetweenInc(long min, long max)
        {
            return RandomLongBetweenExc(min - 1, max + 1);
        }

        /// <summary>Function <c>RandomLongBetweenExc</c> generates a secure random integer between <c>min</c> and <c>max</c> exclusive</summary>
        public static long RandomLongBetweenExc(long min, long max)
        {
            if ((max - min) <= 1 || min > max)
            {
                throw new ArgumentOutOfRangeException($"No valid integers to pick between {min} and {max}");
            }

            var lengthOfGap = (max - min) - 1;
            // find the number of random bits needed to generate a secure result
            var noBitsNeeded = (int)Math.Ceiling(Math.Log2(lengthOfGap));
            // shifts a 1 noBitsNeeded+1 places then subtracts one to create a mask of noBitsNeeded ones
            var mask = (long)((ulong)1 << (noBitsNeeded + 1)) - 1;

            long result;
            do
            {
                result = GenerateRandomLong();
                // remove unneeded bits from result, then add min
                // creates a number beween min and [the next power of two greater than max]
                result = (result & mask) + min;
                // this could be higher than max, in which case try again
                // WE DO NOT SCALE OR USE DIVISION BECAUSE FLOATS CREATE PATTERNS THAT DESTROY RANDOMNESS
            } while (!((result > min) && (result < max)));

            return result;
        }

        public static long GenerateRandomLong()
        {
            return BitConverter.ToInt64(GenerateRandomBytes(8));
        }

        public static byte[] GenerateRandomBytes(int n)
        {
            using System.Security.Cryptography.RNGCryptoServiceProvider cryptRNG = new();
            byte[] tokenBuffer = new byte[n];
            cryptRNG.GetBytes(tokenBuffer);
            return tokenBuffer;
        }
    }
}
