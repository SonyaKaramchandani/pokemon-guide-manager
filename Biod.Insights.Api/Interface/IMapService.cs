using System.Collections.Generic;
using Biod.Insights.Api.Models.Map;

namespace Biod.Insights.Api.Interface
{
    public interface IMapService
    {
        IEnumerable<EventsPinModel> GetCountryEventPins();
        
        IEnumerable<EventsPinModel> GetCountryEventPins(HashSet<int> eventIds);
    }
}