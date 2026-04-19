using Toolkit_API.Application.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
namespace Toolkit_API.Infrastructure.Services
{
    public class EmailServices : IEmailServices
    {
        public async Task<string> SendEmail(string to, string subject, string body)
        {
            var msg = new MimeMessage();

            msg.From.Add(new MailboxAddress("Avtoolkit News Letter", "avtoolkitnews@gmail.com"));

            msg.To.Add(new MailboxAddress("", to));

            msg.Subject = subject;
            msg.Body = new TextPart("plain")
            {
                Text = body
            };

            using(var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("avtoolkitnews@gmail.com", Environment.GetEnvironmentVariable("NEWS_PS"));
                await client.SendAsync(msg);
                await client.DisconnectAsync(true);
            }

            return "Email sent successfully";
        }
        public async Task<string> SubscribeToNewsLetter(string email)
        {
            // Logic to add the email to the subscription list
            return "Subscribed successfully";
        }
    }
}
