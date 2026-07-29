using Foreman_Backend_Notif.Models;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using System.Net.Sockets;

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

            Console.WriteLine("Connecting to smtp...");

            try
            {
                using var tcp = new TcpClient();
                await tcp.ConnectAsync("smtp.gmail.com", 587);
                Console.WriteLine("TCP connection succeeded.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Authenticating...");
            
            await smtp.AuthenticateAsync(
                "foreman.cdac.app@gmail.com",
                "bsyclgongmftefmx");
            
            Console.WriteLine("Sending email...");
            
            await smtp.SendAsync(email);
            
            Console.WriteLine("Email sent successfully!");
            
            await smtp.DisconnectAsync(true);
        }

    }
}
