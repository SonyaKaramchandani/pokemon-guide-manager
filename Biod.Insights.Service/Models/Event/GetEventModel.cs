using System.Collections.Generic;
using Biod.Insights.Service.Models.Airport;
using Biod.Insights.Service.Models.Article;
using Biod.Insights.Service.Models.Disease;
using Newtonsoft.Json;

namespace Biod.Insights.Service.Models.Event
{
    public class GetEventModel
    {
        public bool IsLocal { get; set; }

        public EventInformationModel EventInformation { get; set; }

        public RiskModel ImportationRisk { get; set; }

        public RiskModel ExportationRisk { get; set; }

        public CaseCountModel CaseCounts { get; set; }

        public IEnumerable<EventLocationModel> EventLocations { get; set; }

        public IEnumerable<ArticleModel> Articles { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<GetAirportModel> SourceAirports { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<GetAirportModel> DestinationAirports { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DiseaseInformationModel DiseaseInformation { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public OutbreakPotentialCategoryModel OutbreakPotentialCategory { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<EventLocationModel> UpdatedLocations { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public CaseCountModel LocalCaseCounts { get; set; }
    }
}