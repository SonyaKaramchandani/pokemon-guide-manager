using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Notification.Engine.Models;

namespace Biod.Insights.Notification.Engine.Services
{
    public interface INotificationService
    {
        Task<ProcessEmailResult> SendEmail(EmailViewModel emailViewModel);
        
        Task SendEmails(IAsyncEnumerable<EmailViewModel> emailViewModels);
    }
}