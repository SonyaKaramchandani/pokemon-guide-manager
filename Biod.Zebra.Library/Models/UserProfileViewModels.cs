using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.Models.DiseaseRelevance;
using Newtonsoft.Json;

namespace Biod.Zebra.Library.Models
{
    public class UserProfileViewModel
    {
        public PersonalDetailsViewModel PersonalDetails { get; set; }
        public ChangePasswordViewModel ChangePassword { get; set; }
        public UserNotificationViewModel UserNotification { get; set; }
    }

    public class PersonalDetailsViewModel
    {
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

        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }

        //[RegularExpression("[^\\D]+", ErrorMessage = "The Location field: please select a location from drop-down list after key input.")]
        [Range(1, int.MaxValue, ErrorMessage = "The Location field: please select a location from drop-down list after key input.")]
        public int GeonameId { get; set; }
        public string GridId { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string UserGroup { get; set; }

        public string RoleNotificationDescription { get; set; }
    }
    public class UserNotificationViewModel
    {
        [Required(ErrorMessage = "Please set at least one location as your default area of interest to continue.")]
        [Display(Name = "Area of interest")]
        public string AoiGeonameIds { get; set; }
        public List<GeonameModel> AoiGeonames { get; set; }
        public string DiseaseIds { get; set; }
        public bool SmsNotificationEnabled { get; set; }
        public bool NewCaseNotificationEnabled { get; set; }
        public bool NewOutbreakNotificationEnabled { get; set; }
        public bool WeeklyOutbreakNotificationEnabled { get; set; }
    }

    public class CustomSettingsViewModel
    {
        [Required(ErrorMessage = "Please set at least one location as your default area of interest to continue.")]
        [Display(Name = "Area of interest")]
        [JsonProperty("aoiGeonameIds")]
        public string AoiGeonameIds { get; set; }

        [Required]
        [Display(Name = "UserId")]
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("roleId")]
        public string RoleId { get; set; }

        [JsonProperty("roleDescription")]
        public string RoleDescription { get; set; }

        [JsonProperty("userRelevance")]
        public RelevanceViewModel UserRelevanceViewModel { get; set; }
        
        [JsonProperty("presetSelected")]
        public bool RolePresetSelected { get; set; }
        
        public DiseaseRelevance.DiseaseRelevanceViewModel DiseaseRelevanceViewModel { get; set; }
    }

    public class UserProfileDto
    {
        public static UserProfileDto GetUserProfileDto(BiodZebraEntities dbContext, ApplicationUser user)
        {
            return new UserProfileDto()
            {
                UserId = user.Id,
                UserName = user.UserName,
                OnboardingCompleted = user.OnboardingCompleted,
                PersonalDetails = new PersonalDetailsViewModel()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    GeonameId = user.GeonameId,
                    Location = user.Location
                },
                UserNotification = new UserNotificationViewModel()
                {
                    AoiGeonameIds = user.AoiGeonameIds,
                    AoiGeonames = dbContext.usp_SearchGeonamesByGeonameIds(user.AoiGeonameIds)
                        .Select(g => new GeonameModel
                        {
                            GeonameId = g.GeonameId,
                            LocationDisplayName = g.DisplayName
                        })
                        .ToList(),
                    DiseaseIds = string.Join(",", AccountHelper.GetRelevantDiseaseIds(dbContext, user)),
                    SmsNotificationEnabled = user.SmsNotificationEnabled,
                    NewCaseNotificationEnabled = user.NewCaseNotificationEnabled,
                    NewOutbreakNotificationEnabled = user.NewOutbreakNotificationEnabled,
                    WeeklyOutbreakNotificationEnabled = user.WeeklyOutbreakNotificationEnabled
                }
            };
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool OnboardingCompleted { get; set; }
        public PersonalDetailsViewModel PersonalDetails { get; set; }
        public UserNotificationViewModel UserNotification { get; set; }
    }
}
