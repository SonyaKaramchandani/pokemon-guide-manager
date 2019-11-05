using Biod.Zebra.Library.EntityModels;

namespace Biod.Zebra.Library.Infrastructures.Notification
{
    public class NotificationDependencyFactory : INotificationDependencyFactory
    {
        public BiodZebraEntities GetDbContext()
        {
            return new BiodZebraEntities();
        }
    }
}