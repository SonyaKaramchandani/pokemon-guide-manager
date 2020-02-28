using System.Threading.Tasks;

namespace Biod.Insights.Notification.Engine.Services.Weekly
{
    public interface IWeeklyNotificationService : INotificationService
    {
        Task ProcessRequest();
    }
}