using Biod.Zebra.Library.Infrastructures;

namespace Biod.Zebra.Library.Models.Notification.Email
{
    public class ConfirmationEmailViewModel : EmailViewModel
    {
        public override int NotificationType => Constants.EmailTypes.EMAIL_CONFIRMATION;

        public override string ViewFilePath => "~/Views/Home/ConfirmationEmail.cshtml";

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CallbackUrl { get; set; }
    }
}