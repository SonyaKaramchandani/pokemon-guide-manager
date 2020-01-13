using System.Collections.Generic;
using Biod.Insights.Api.Models.Map;

namespace Biod.Insights.Api.Models.Disease
{
    public class RiskAggregationModel
    {
        public IEnumerable<DiseaseRiskModel> DiseaseRisks { get; set; }
        
        public IEnumerable<EventsPinModel> CountryPins { get; set; }
    }
}