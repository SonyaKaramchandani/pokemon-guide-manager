using System.Threading.Tasks;

namespace Biod.Insights.Notification.Engine.Services.EmailDelivery
{
    public interface IEmailClientService
    {
        Task SendEmailAsync(EmailMessage message);
    }
}