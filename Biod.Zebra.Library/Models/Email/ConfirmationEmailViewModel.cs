using Biod.Zebra.Library.Infrastructures;

namespace Biod.Zebra.Library.Models.Email
{
    public class ConfirmationEmailViewModel : EmailViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CallbackUrl { get; set; }

        public override int GetEmailType()
        {
            return Constants.EmailTypes.EMAIL_CONFIRMATION;
        }

        public override string GetViewFilePath()
        {
            return "~/Views/Home/ConfirmationEmail.cshtml";
        }
    }
}