using Foreman_Backend_Notif.Models;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;

namespace Foreman_Backend_Notif.Services
{
    public class NotificationService
    {
        public async Task SendNotificationEmail(Notification n)
        {

            MailboxAddress SenderAddress = new MailboxAddress("Foreman", "foreman.cdac.app@gmail.com");
            MailboxAddress ReceiverAddress = new MailboxAddress("", n.ReceiverEmail);

            var email = new MimeMessage();

            email.From.Add(SenderAddress);
            email.To.Add(ReceiverAddress);
            email.Subject = n.Title;
            email.Body = new TextPart("plain")
            {

                Text = n.Message
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(
                "foreman.cdac.app@gmail.com",
                "bsyclgongmftefmx");

            await smtp.SendAsync(email);

            await smtp.DisconnectAsync(true);
        }

    }
}
