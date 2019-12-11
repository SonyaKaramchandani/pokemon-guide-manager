using Biod.Zebra.Library.EntityModels.Zebra;

namespace Biod.Zebra.Library.Infrastructures.Notification
{
    public interface INotificationDependencyFactory
    {
        BiodZebraEntities GetDbContext();
    }
}