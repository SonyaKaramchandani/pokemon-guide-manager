using System.Collections.Generic;
using Biod.Insights.Api.Models.Disease;
using Biod.Insights.Api.Models.Map;

namespace Biod.Insights.Api.Models.Event
{
    public class GetEventListModel
    {
        public DiseaseInformationModel DiseaseInformation { get; set; }

        public RiskModel ImportationRisk { get; set; }

        public RiskModel ExportationRisk { get; set; }

        public OutbreakPotentialCategoryModel OutbreakPotentialCategory { get; set; }

        public IEnumerable<GetEventModel> EventsList { get; set; }
        
        public IEnumerable<EventsPinModel> CountryPins { get; set; }
    }
}