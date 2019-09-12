using Biod.Zebra.Library.Infrastructures;

namespace Biod.Zebra.Library.Models.Notification.Email
{
    public class ResetPasswordEmailViewModel : EmailViewModel
    {
        public override int NotificationType => Constants.EmailTypes.RESET_PASSWORD_EMAIL;

        public override string ViewFilePath => "~/Views/Home/ResetPasswordEmail.cshtml";

        public string FirstName { get; set; }

        public string CallbackUrl { get; set; }
    }
}