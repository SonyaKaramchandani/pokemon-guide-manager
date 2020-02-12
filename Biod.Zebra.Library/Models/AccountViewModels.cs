using System;
using Biod.Zebra.Library.Infrastructures;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Biod.Zebra.Library.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Stay logged in")]
        public bool RememberMe { get; set; }
    }

    [Obsolete("This model was used in MVC based registration and is now deprecated. Use CreateUserViewModel.cs")]
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            NewCaseNotificationEnabled = true;
            NewOutbreakNotificationEnabled = true;
            PeriodicNotificationEnabled = true;
            WeeklyOutbreakNotificationEnabled = true;
        }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }
        [Display(Name = "Role List")]
        public IEnumerable<SelectListItem> RolesList { get; set; }
        [Display(Name = "Organization")]
        public string Organization { get; set; }
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        //[AutoComplete("Account", "Autocomplete")]
        //[AutoComplete("Home", "LocationAutocomplete")]
        [Display(Name = "Location")]
        public string Location { get; set; }
        //[RegularExpression("[^\\D]+", ErrorMessage = "The Location field: please select a location from drop-down list after key input.")]
        [Range(1, int.MaxValue, ErrorMessage = "The Location field: please select a location from drop-down list after key input.")]
        public int GeonameId { get; set; }
        public string GridId { get; set; }
        public string AoiGeonameIds { get; set; }
        [DefaultValue(false)]
        public bool SmsNotificationEnabled { get; set; }
        [DefaultValue(true)]
        public bool NewCaseNotificationEnabled { get; set; }
        [DefaultValue(true)]
        public bool NewOutbreakNotificationEnabled { get; set; }
        [DefaultValue(true)]
        public bool PeriodicNotificationEnabled { get; set; }
        [DefaultValue(true)]
        public bool WeeklyOutbreakNotificationEnabled { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
       
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
