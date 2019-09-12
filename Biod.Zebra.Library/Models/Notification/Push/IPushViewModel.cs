using System.Collections.Generic;

namespace Biod.Zebra.Library.Models.Notification.Push
{
    public interface IPushViewModel : INotificationViewModel
    { 
        int NotificationId { get; set; }

        List<string> DeviceTokenList { get; set; }

        string Summary { get; set; }

    }
}
