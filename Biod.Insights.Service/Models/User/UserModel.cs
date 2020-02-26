using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Biod.Insights.Service.Models.Geoname;

namespace Biod.Insights.Service.Models.User
{
    public class UserModel
    {
        public string Id { get; set; }
        
        public int? GroupId { get; set; }
        
        public UserPersonalDetailsModel PersonalDetails { get; set; }
        
        public GetGeonameModel Location { get; set; }
        
        public IEnumerable<UserRoleModel> Roles { get; set; }
        
        public DiseaseRelevanceSettingsModel DiseaseRelevanceSetting { get; set; }
        
        public UserNotificationsModel NotificationsSetting { get; set; }
        
        public bool IsDoNotTrack { get; set; }
    }
}