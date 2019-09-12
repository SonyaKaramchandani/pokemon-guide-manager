using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Biod.Zebra.Library.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "RoleName")]
        public string Name { get; set; }
        
        [Display(Name = "Notification Description")]
        public string NotificationDescription { get; set; }
        
        [Display(Name = "Public")]
        public bool IsPublic { get; set; }
        
        public IEnumerable<SelectListItem> UsersList { get; set; }
    }

    public class UserGroupViewModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "User Group Name")]
        public string Name { get; set; }
        [Display(Name = "Users")]
        public IEnumerable<SelectListItem> UsersList { get; set; }
    }


    public class EditUserViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Organization")]
        public string Organization { get; set; }
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }
        //[RegularExpression("[^\\D]+", ErrorMessage = "The Location field: please select a location from drop-down list after key input.")]
        [Range(1, int.MaxValue, ErrorMessage = "The Location field: please select a location from drop-down list after key input.")]
        public int GeonameId { get; set; }
        public string AoiGeonameIds { get; set; }
        public string GridId { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The User Group field: please select a user group from drop-down list")]
        public int? UserGroupId { get; set; }
        [Display(Name = "User Group")]
        public string UserGroup { get; set; }
        [Display(Name = "Do Not Track Enabled")]
        public bool DoNotTrackEnabled { get; set; }
        public bool EmailConfirmed { get; set; }
        public IEnumerable<SelectListItem> RolesList { get; set; }
        public string RoleNames { get; set; }
    }

    public class UserViewModel : EditUserViewModel
    {
        public string UserName { get; set; }

        public string CreationDate { get; set; }

        public bool NewCaseNotificationEnabled { get; set; }

        public bool NewOutbreakNotificationEnabled { get; set; }

        public bool PeriodicNotificationEnabled { get; set; }

        public bool WeeklyOutbreakNotificationEnabled { get; set; }
    }
}