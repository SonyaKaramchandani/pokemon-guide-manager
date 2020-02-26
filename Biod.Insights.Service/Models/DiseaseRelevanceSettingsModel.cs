using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Biod.Insights.Service.Models
{
    public class DiseaseRelevanceSettingsModel
    {
        [Required]
        public HashSet<int> AlwaysNotifyDiseaseIds { get; set; }
        
        [Required]
        public HashSet<int> RiskOnlyDiseaseIds { get; set; }
        
        [Required]
        public HashSet<int> NeverNotifyDiseaseIds { get; set; }

        public HashSet<int> GetRelevantDiseases()
        {
            return new HashSet<int>(AlwaysNotifyDiseaseIds.Union(RiskOnlyDiseaseIds));
        }
    }
}