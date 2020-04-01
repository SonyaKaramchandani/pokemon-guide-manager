using System.Collections.Generic;
using Biod.Insights.Service.Data.CustomModels;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Models.Event;

namespace Biod.Insights.Service.Interface
{
    public interface ICaseCountService
    {
        Dictionary<int, EventCaseCountModel> GetCaseCountTree(List<XtblEventLocation> eventLocations);
        
        Dictionary<int, EventCaseCountModel> GetCaseCountTree(List<XtblEventLocationJoinResult> eventLocations);

        Dictionary<int, EventCaseCountModel> GetLocationIncreasedCaseCount(Dictionary<int, EventCaseCountModel> previous, Dictionary<int, EventCaseCountModel> current, bool isDataFlattened = true);
        
        Dictionary<int, EventCaseCountModel> GetAggregatedIncreasedCaseCount(Dictionary<int, EventCaseCountModel> previous, Dictionary<int, EventCaseCountModel> current, bool isDataFlattened = true);
    }
}