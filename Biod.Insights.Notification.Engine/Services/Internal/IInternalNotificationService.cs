using System.Threading.Tasks;

namespace Biod.Insights.Notification.Engine.Services.Internal
{
    public interface IInternalNotificationService : INotificationService
    {
        Task ProcessRequest(string subject, string body);
    }
}