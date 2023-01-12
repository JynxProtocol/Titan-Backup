using System;
using System.Net.Mail;

namespace Titan.API.Functions
{
    public static class EmailFunctions
    {
        // legacy email function
        // IMPROVEMENT: update email sending methods
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
                message.From = new MailAddress("Titan@sabrerail.com");

                SmtpClient.Send(message);
            }
            catch (Exception)
            {

            }
        }
    }
}
