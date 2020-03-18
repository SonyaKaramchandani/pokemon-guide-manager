using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Service.Models;
using Biod.Insights.Service.Models.Event;

namespace Biod.Insights.Service.Interface
{
    public interface IEventService
    {
        Task<EventAirportModel> GetAirports(int eventId);

        Task<Dictionary<string, Dictionary<int, HashSet<int>>>> GetUsersWithinEventLocations(int eventId);

        Task<Dictionary<string, Dictionary<int, HashSet<int>>>> GetUsersWithinEventLocations(GetEventModel eventModel);

        Task<GetEventModel> GetEvent(int eventId, int? geonameId, bool includeLocationHistory);

        Task<GetEventListModel> GetEvents(int? diseaseId, int? geonameId);

        Task<GetEventListModel> GetEvents(HashSet<int> diseaseId, int? geonameId);

        Task<GetEventListModel> GetEvents(int? diseaseId, int? geonameId, DiseaseRelevanceSettingsModel relevanceSettings);
    }
}