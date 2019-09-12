namespace Biod.Zebra.Library.Models.Notification.Email
{
    public abstract class EmailViewModel : NotificationViewModel, IEmailViewModel
    {
        public abstract override int NotificationType { get; }

        public abstract string ViewFilePath { get; }

        public string AoiGeonameIds { get; set; }

        public string Email { get; set; }
    }
}
