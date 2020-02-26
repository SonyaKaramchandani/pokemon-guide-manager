using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biod.Insights.Service.Models.User
{
    public class UserCustomSettingsModel
    {
        [Required]
        public IEnumerable<int> GeonameIds { get; set; }
        
        [Required]
        public string RoleId { get; set; }
        
        [Required]
        public DiseaseRelevanceSettingsModel DiseaseRelevanceSettings { get; set; }
    }
}