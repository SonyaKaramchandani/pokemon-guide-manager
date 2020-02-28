using System.Threading.Tasks;

namespace Biod.Insights.Notification.Engine.Services.NewEvent
{
    public interface INewEventNotificationService : INotificationService
    {
        Task ProcessRequest(int eventId);
    }
}