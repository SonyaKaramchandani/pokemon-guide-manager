using System.Threading.Tasks;
using Biod.Insights.Api.Models;
using Biod.Insights.Api.Models.Event;

namespace Biod.Insights.Api.Interface
{
    public interface IEventService
    {
        Task<EventAirportModel> GetAirports(int eventId, int? geonameId);
        
        Task<CaseCountModel> GetEventCaseCount(int eventId);
        
        Task<GetEventModel> GetEvent(int eventId, int? geonameId);
        
        Task<GetEventListModel> GetEvents(int? diseaseId, int? geonameId);
    }
}