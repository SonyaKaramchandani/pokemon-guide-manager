using System.Collections.Generic;
using Biod.Insights.Api.Models.Article;

namespace Biod.Insights.Api.Models.Event
{
    public class GetEventModel
    {
        public EventInformationModel EventInformation { get; set; }

        public RiskModel ImportationRisk { get; set; }

        public RiskModel ExportationRisk { get; set; }

        public CaseCountModel CaseCounts { get; set; }
        
        public IEnumerable<EventLocationModel> EventLocations { get; set; }
        
        public IEnumerable<ArticleModel> Articles { get; set; }

        public bool IsLocal { get; set; }
    }
}