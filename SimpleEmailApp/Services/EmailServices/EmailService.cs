using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace SimpleEmailApp.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        public readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(EmailDto request)
        {
            var email = new MimeMessage();

            //Go to Ethereal Email an create an account to get a test email address
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            // You can use HTML/Simple Text/Rich Text etc
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            // Sender Credentials
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.Auto);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);

        }
    }
}
