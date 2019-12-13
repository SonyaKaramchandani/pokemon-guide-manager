using System.Collections;
using System.Collections.Generic;
using Biod.Insights.Api.Models.Disease;

namespace Biod.Insights.Api.Models.Event
{
    public class GetEventListModel
    {
        public DiseaseInformationModel DiseaseInformation { get; set; }
        
        public RiskModel ImportationRisk { get; set; }
        
        public RiskModel ExportationRisk { get; set; }
        
        public IEnumerable<GetEventModel> EventsList { get; set; }
    }
}