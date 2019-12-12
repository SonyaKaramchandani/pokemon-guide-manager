using System;

namespace Biod.Insights.Api.Models
{
    public class GetDiseaseRiskModel
    {
        public DiseaseInformationModel DiseaseInformation { get; set; } 
        
        public RiskModel ImportationRisk { get; set; }
        
        public DateTime LastUpdatedEventDate { get; set; }
        
        public int LocalCaseCount { get; set; }
        
        public string OutbreakPotentialCategory { get; set; }
    }
}