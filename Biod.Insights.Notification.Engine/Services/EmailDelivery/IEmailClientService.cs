using System.Threading.Tasks;

namespace Biod.Insights.Notification.Engine.Services.EmailDelivery
{
    public interface IEmailClientService
    {
        /// <summary>
        /// Sends the Email Message to third party service to ensure delivery to user email
        /// </summary>
        /// <param name="message">the email to send</param>
        /// <returns>true if successfully sent, false otherwise</returns>
        Task<bool> SendEmailAsync(EmailMessage message);
    }
}