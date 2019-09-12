namespace Biod.Zebra.Library.Models.Notification.Email
{
    public interface IEmailViewModel : INotificationViewModel
    {
        string AoiGeonameIds { get; set; }

        string Email { get; set; }

        string ViewFilePath { get; }
    }
}