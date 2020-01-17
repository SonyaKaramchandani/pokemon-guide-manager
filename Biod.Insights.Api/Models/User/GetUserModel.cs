using System.Collections.Generic;

namespace Biod.Insights.Api.Models.User
{
    public class GetUserModel
    {
        public string Id { get; set; }
        
        public IEnumerable<UserRoleModel> Roles { get; set; }
        
        public DiseaseRelevanceSettingsModel DiseaseRelevanceSetting { get; set; }
        
        public bool IsDoNotTrack { get; set; }
    }
}