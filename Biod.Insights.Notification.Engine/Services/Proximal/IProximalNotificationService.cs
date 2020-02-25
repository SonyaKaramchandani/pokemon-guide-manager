using System.Threading.Tasks;

namespace Biod.Insights.Notification.Engine.Services.Proximal
{
    public interface IProximalNotificationService : INotificationService
    {
        Task ProcessRequest(int eventId);
    }
}