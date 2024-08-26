using System.Net;
using System.Net.Mail;

namespace Background.Services
{
    public class SendEmailService
    {
        private readonly string _smtpServer;
        private readonly int _port;
        private readonly string _senderEmail;
        private readonly string _senderPassword;

        public SendEmailService(IConfiguration configuration)
        {
            var emailSettings = configuration.GetSection("EmailSettings")!;
            _smtpServer = emailSettings["SmtpServer"]!;
            _port = int.Parse(emailSettings["Port"]!);
            _senderEmail = emailSettings["SenderEmail"]!;
            _senderPassword = emailSettings["SenderPassword"]!;
        }

        public async Task SendEmailsWithBccAsync(IEnumerable<string> recipients, string subject, string body)
        {
            using var smtpClient = new SmtpClient(_smtpServer, _port)
            {
                Credentials = new NetworkCredential(_senderEmail, _senderPassword),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_senderEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            foreach (var recipient in recipients)
            {
                mailMessage.Bcc.Add(recipient);
            }

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
