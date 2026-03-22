using System;
using System.Threading.Tasks;

namespace LoginFrontend.Services
{
    public interface IEmailService
    {
        Task SendAccountBlockedEmailAsync(string toEmail, string userName, DateTime blockExpirationTime);
    }
}
