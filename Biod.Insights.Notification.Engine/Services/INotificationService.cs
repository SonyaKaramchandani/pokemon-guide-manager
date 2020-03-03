using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Notification.Engine.Models;

namespace Biod.Insights.Notification.Engine.Services
{
    public interface INotificationService
    {
        Task SendEmail(NotificationSettings notificationSettings, EmailViewModel emailViewModel);
        
        Task SendEmails(NotificationSettings notificationSettings, IAsyncEnumerable<EmailViewModel> emailViewModels);
    }
}