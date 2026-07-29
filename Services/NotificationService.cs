using Foreman_Backend_Notif.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Foreman_Backend_Notif.Services
{
    public class NotificationService
    {
        public async Task SendNotificationEmail(Notification n)
        {
            MailboxAddress senderAddress =
                new MailboxAddress("Foreman", "foreman.cdac.app@gmail.com");

            MailboxAddress receiverAddress =
                new MailboxAddress("", n.ReceiverEmail);

            var email = new MimeMessage();

            email.From.Add(senderAddress);
            email.To.Add(receiverAddress);
            email.Subject = n.Title;
            email.Body = new TextPart("plain")
            {
                Text = n.Message
            };

            using var smtp = new SmtpClient();

            // ---------- Connect ----------
            try
            {
                Console.WriteLine("Connecting SMTP...");

                smtp.ServerCertificateValidationCallback = (s, c, h, e) =>
                {
                    Console.WriteLine("Certificate callback reached.");
                    return true;
                };
                
                await smtp.ConnectAsync(
                    "smtp.gmail.com",
                    465,
                    SecureSocketOptions.SslOnConnect);

                Console.WriteLine("SMTP Connected!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ConnectAsync failed!");
                Console.WriteLine(ex);
                throw;
            }

            // ---------- Authenticate ----------
            try
            {
                Console.WriteLine("Authenticating...");

                await smtp.AuthenticateAsync(
                    "foreman.cdac.app@gmail.com",
                    "bsyclgongmftefmx");

                Console.WriteLine("Authenticated!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("AuthenticateAsync failed!");
                Console.WriteLine(ex);
                throw;
            }

            // ---------- Send ----------
            try
            {
                Console.WriteLine("Sending email...");

                await smtp.SendAsync(email);

                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("SendAsync failed!");
                Console.WriteLine(ex);
                throw;
            }

            // ---------- Disconnect ----------
            try
            {
                Console.WriteLine("Disconnecting...");

                await smtp.DisconnectAsync(true);

                Console.WriteLine("Disconnected.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("DisconnectAsync failed!");
                Console.WriteLine(ex);
            }
        }
    }
}
