using System.Collections.Generic;
using System.Linq;

namespace Biod.Insights.Api.Models
{
    public class DiseaseRelevanceSettingsModel
    {
        public HashSet<int> AlwaysNotifyDiseaseIds { get; set; }
        
        public HashSet<int> RiskOnlyDiseaseIds { get; set; }
        
        public HashSet<int> NeverNotifyDiseaseIds { get; set; }

        public HashSet<int> GetRelevantDiseases()
        {
            return new HashSet<int>(AlwaysNotifyDiseaseIds.Union(RiskOnlyDiseaseIds));
        }
    }
}