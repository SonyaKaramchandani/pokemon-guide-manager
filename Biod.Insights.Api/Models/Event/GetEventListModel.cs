using System.Collections.Generic;
using Biod.Insights.Api.Models.Disease;
using Biod.Insights.Api.Models.Map;
using Newtonsoft.Json;

namespace Biod.Insights.Api.Models.Event
{
    public class GetEventListModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DiseaseInformationModel DiseaseInformation { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public OutbreakPotentialCategoryModel OutbreakPotentialCategory { get; set; }
        
        public CaseCountModel LocalCaseCounts { get; set; }

        public RiskModel ImportationRisk { get; set; }

        public RiskModel ExportationRisk { get; set; }

        public IEnumerable<GetEventModel> EventsList { get; set; }
        
        public IEnumerable<EventsPinModel> CountryPins { get; set; }
    }
}