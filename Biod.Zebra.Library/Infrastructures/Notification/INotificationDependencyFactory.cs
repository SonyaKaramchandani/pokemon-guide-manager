using Biod.Zebra.Library.EntityModels;

namespace Biod.Zebra.Library.Infrastructures.Notification
{
    public interface INotificationDependencyFactory
    {
        BiodZebraEntities GetDbContext();
    }
}