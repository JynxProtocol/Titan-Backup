using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Titan.Data;

namespace Titan.Functions
{
    public class Email
    {
        public static void Sendmail(String To, String Subject, String Body)
        {
            Titan.TitanFunctions.DataLogger.LogData("Email", "Attempt to Send Mail To: " + To, 1);

            try
            {
                var message = new MailMessage();

                message.To.Add(new MailAddress(To));
                message.Subject = Subject;
                message.Body = Body;
                message.Priority = MailPriority.High;

                message.IsBodyHtml = true;

                using var smtp = new SmtpClient();
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                Titan.TitanFunctions.DataLogger.LogData("Email", "Error: " + ex, 1);
            }
        }

        public static void SubNotification(int ticketID)
        {
            //get list of subscritions with ticketId

            //for each ticket id find the email address for the usrid and email

            var mydata = new TitanEntities();
            var myauth = new AuthEntities();

            List<Titan.Data.TicketSubscription> mylist = (from d in mydata.TicketSubscriptions where d.SubTicketID == ticketID select d).ToList();

            foreach (var itm in mylist)
            {
                if (itm == null)
                {
                    //No Data to Process
                }
                else
                {
                    //Get email address and send mail

                    var myaddress = myauth.Users.Where(usr => usr.UsrID == itm.SubUsrID).FirstOrDefault();
                    string myemail = myaddress.UsrEmailAddress.ToString();

                    string TicketSubject = "Enquiry " + itm.SubTicketID + " has been updated";
                    string TicketBody = "Enquiry Has been Updated - Please login to review updates or to add comments";

                    Sendmail(myemail, TicketSubject, TicketBody);
                }
            }
        }

        public static void Sendmail2(String To, String Subject, String Body)
        {
            var message = new MailMessage();

            message.To.Add(new MailAddress(To));
            message.Bcc.Add("customerservices@sabrerail.com"); //Disabled at request
            message.Subject = Subject;
            message.Body = Body;
            message.Priority = MailPriority.High;

            System.Net.Mail.Attachment attachment;

            attachment = new System.Net.Mail.Attachment(@"C:\Titan\OrderAck\" + "logo.png");
            message.Attachments.Add(attachment);
            message.Attachments[0].ContentId = "logo.jpg";

            attachment = new System.Net.Mail.Attachment(@"C:\Titan\OrderAck\" + "Sabre Terms and Conditions.pdf");
            message.Attachments.Add(attachment);

            message.IsBodyHtml = true;

            try
            {
                using var smtp = new SmtpClient();
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                DataLogger.LogData("Email", "Could Not send mail:" + ex.Message, 1);
            }
        }

        public static void Sendmail3(String To, String Subject, String Body)
        {
            var message = new MailMessage();

            message.To.Add(new MailAddress(To));
            message.Subject = Subject;
            message.Body = Body;
            message.Priority = MailPriority.High;

            System.Net.Mail.Attachment attachment;

            attachment = new System.Net.Mail.Attachment(@"C:\Titan\OrderAck\" + "logo.png");
            message.Attachments.Add(attachment);
            message.Attachments[0].ContentId = "logo.jpg";

            attachment = new System.Net.Mail.Attachment(@"C:\Titan\OrderAck\" + "Sabre Terms and Conditions.pdf");
            message.Attachments.Add(attachment);

            message.IsBodyHtml = true;

            using var smtp = new SmtpClient();
            smtp.Send(message);
        }

        public static void SendBulkMail(String To, String Subject, String Body)
        {
            var message = new MailMessage();

            message.To.Add(new MailAddress(To));
            message.Subject = Subject;
            message.Body = Body;
            message.Priority = MailPriority.High;

            message.IsBodyHtml = true;

            using var smtp = new SmtpClient();
            smtp.Send(message);
        }
    }
}