using System.Collections.Generic;
using Biod.Insights.Api.Data.EntityModels;

namespace Biod.Insights.Api.Data.CustomModels
{
    public class EventJoinResult
    {
        public Event Event { get; set; }
        
        public EventImportationRisksByGeoname ImportationRisk { get; set; }
        
        public IEnumerable<XtblEventLocationJoinResult> XtblEventLocations { get; set; }
        
        public IEnumerable<usp_ZebraEventGetArticlesByEventId_Result> ArticleSources { get; set; }
    }
}