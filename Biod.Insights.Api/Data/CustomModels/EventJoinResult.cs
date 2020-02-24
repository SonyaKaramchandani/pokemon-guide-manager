using System.Collections.Generic;
using System.Linq;
using Biod.Insights.Common.Constants;
using Biod.Insights.Data.CustomModels;
using Biod.Insights.Data.EntityModels;

namespace Biod.Insights.Api.Data.CustomModels
{
    public class EventJoinResult
    {
        public Event Event { get; set; }
        
        public EventImportationRisksByGeoname ImportationRisk { get; set; }
        
        public IEnumerable<XtblEventLocationJoinResult> XtblEventLocations { get; set; }
        
        public IEnumerable<usp_ZebraEventGetArticlesByEventId_Result> ArticleSources { get; set; }

        private bool? _IsModelNotRun { get; set; }
        public bool IsModelNotRun
        {
            get
            {
                if (!_IsModelNotRun.HasValue)
                {
                    _IsModelNotRun = Event.IsLocalOnly || XtblEventLocations.All(x => x.LocationType == (int) LocationType.Country);
                }

                return _IsModelNotRun.Value;
            }
        }
    }
}