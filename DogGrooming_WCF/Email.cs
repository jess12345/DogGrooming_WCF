using System;
using System.Net;
using System.Net.Mail;

namespace DogGrooming_WCF
{
    public class Email
    {
        public static void Send(string toEmail, string subject, string message) // This throws exception if it desn't work
        {
            string smtp_user = "4testing.testemail@gmail.com";
            string smtp_pass = "Testing12";
            SmtpClient smtpClient = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(smtp_user, smtp_pass)

            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(smtp_user),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mailMessage.To.Add(toEmail);
            //mailMessage.CC.Add("jessicahu@windowslive.com");

            smtpClient.Send(mailMessage);
        }
    }
}