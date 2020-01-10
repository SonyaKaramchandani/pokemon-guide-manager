using System.Collections.Generic;

namespace Biod.Insights.Api.Models
{
    public class DiseaseRelevanceSettingsModel
    {
        public HashSet<int> AlwaysNotifyDiseaseIds { get; set; }
        
        public HashSet<int> RiskOnlyDiseaseIds { get; set; }
        
        public HashSet<int> NeverNotifyDiseaseIds { get; set; }
    }
}