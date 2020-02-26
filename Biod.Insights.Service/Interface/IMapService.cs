using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Service.Models.Map;

namespace Biod.Insights.Service.Interface
{
    public interface IMapService
    {
        Task<IEnumerable<EventsPinModel>> GetCountryEventPins();
        
        Task<IEnumerable<EventsPinModel>> GetCountryEventPins(HashSet<int> eventIds);
    }
}