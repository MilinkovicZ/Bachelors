using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using WebShop.Interfaces;

namespace WebShop.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmail(string header, string body, string to)
        {
            var message = new MimeMessage
            {
                Subject = header,
                Body = new TextPart(MimeKit.Text.TextFormat.Plain) { Text = body },
            };

            message.From.Add(new MailboxAddress(_configuration["Email:DisplayName"], _configuration["Email:Address"]));
            message.To.Add(MailboxAddress.Parse(to));

            SmtpClient smtp = new SmtpClient();
            await smtp.ConnectAsync(_configuration["Email:Host"], int.Parse(_configuration["Email:Port"]!), SecureSocketOptions.Auto);
            await smtp.AuthenticateAsync(_configuration["Email:Address"], _configuration["Email:Password"]);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
    }
}
