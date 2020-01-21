using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Api.Data.CustomModels;
using Biod.Insights.Api.Data.EntityModels;
using Biod.Insights.Api.Models.Event;

namespace Biod.Insights.Api.Interface
{
    public interface ICaseCountService
    {
        Dictionary<int, EventCaseCountModel> GetCaseCountTree(List<XtblEventLocation> eventLocations);
        
        Dictionary<int, EventCaseCountModel> GetCaseCountTree(List<XtblEventLocationJoinResult> eventLocations);
    }
}