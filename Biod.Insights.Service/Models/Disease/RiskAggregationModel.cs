using System.Collections.Generic;
using Biod.Insights.Service.Models.Map;

namespace Biod.Insights.Service.Models.Disease
{
    public class RiskAggregationModel
    {
        public IEnumerable<DiseaseRiskModel> DiseaseRisks { get; set; }
        
        public IEnumerable<EventsPinModel> CountryPins { get; set; }
    }
}