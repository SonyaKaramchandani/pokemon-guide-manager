using System.Collections.Generic;
using Biod.Insights.Service.Models.Geoname;

namespace Biod.Insights.Service.Models.User
{
    public class UserModel
    {
        public string Id { get; set; }

        public int? GroupId { get; set; }

        public string AoiGeonameIds { get; set; }

        public UserPersonalDetailsModel PersonalDetails { get; set; }

        public GetGeonameModel Location { get; set; }

        public UserTypeModel UserType { get; set; }

        public IEnumerable<UserRoleModel> Roles { get; set; }

        public DiseaseRelevanceSettingsModel DiseaseRelevanceSetting { get; set; }

        public UserNotificationsModel NotificationsSetting { get; set; }

        public bool IsDoNotTrack { get; set; }

        public bool IsEmailConfirmed { get; set; }
    }
}