using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Biod.Zebra.Library.Models.User
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [JsonProperty(PropertyName = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}