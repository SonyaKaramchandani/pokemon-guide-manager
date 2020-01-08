using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Api.Models.Map;

namespace Biod.Insights.Api.Interface
{
    public interface IMapService
    {
        Task<IEnumerable<EventsPinModel>> GetCountryEventPins();
        
        Task<IEnumerable<EventsPinModel>> GetCountryEventPins(HashSet<int> eventIds);
    }
}