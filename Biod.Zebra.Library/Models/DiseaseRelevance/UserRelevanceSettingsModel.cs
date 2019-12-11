using System.Collections.Generic;

namespace Biod.Zebra.Library.Models.DiseaseRelevance
{
    public class UserRelevanceSettingsModel
    {
        public HashSet<int> AlwaysNotifyDiseaseIds { get; set; }
        
        public HashSet<int> RiskOnlyDiseaseIds { get; set; }
        
        public HashSet<int> NeverNotifyDiseaseIds { get; set; }
    }
}