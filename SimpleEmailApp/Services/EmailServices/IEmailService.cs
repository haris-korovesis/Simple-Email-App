using SimpleEmailApp.Models;

namespace SimpleEmailApp.Services.EmailServices
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}