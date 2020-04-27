using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Products.Common.Constants;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Models;
using Biod.Insights.Service.Models.Event;

namespace Biod.Insights.Service.Interface
{
    public interface IEventService
    {
        /// <summary>
        /// Gets all the source and destination airports associated with the event
        /// </summary>
        /// <param name="eventId">the event id</param>
        /// <param name="geonameId">the geoname id</param>
        Task<EventAirportModel> GetAirports(int eventId, int? geonameId);

        /// <summary>
        /// Gets all users that are considered proximal to the locations of an event
        /// </summary>
        /// <param name="eventId">the event id</param>
        /// <returns>Dictionary with (user id) -> (dictionary with (user's location) -> (event location))</returns>
        Task<Dictionary<string, Dictionary<int, HashSet<int>>>> GetUsersWithinEventLocations(int eventId);

        /// <summary>
        /// Gets the event details for a single event
        /// </summary>
        /// <param name="eventConfig">the configuration on which filters to apply and properties to load</param>
        Task<GetEventModel> GetEvent(EventConfig eventConfig);

        /// <summary>
        /// Gets the event details for all events
        /// </summary>
        /// <param name="eventConfig">the configuration on which filters to apply and properties to load</param>
        Task<GetEventListModel> GetEvents(EventConfig eventConfig);

        /// <summary>
        /// Gets the event details for all events
        /// </summary>
        /// <param name="eventConfig">the configuration on which filters to apply and properties to load</param>
        /// <param name="relevanceSettings">the user's disease relevance settings</param>
        Task<GetEventListModel> GetEvents(EventConfig eventConfig, DiseaseRelevanceSettingsModel relevanceSettings);

        /// <summary>
        /// Updates the Xtbl_Event_Location_History of Proximal <see cref="EventLocationHistoryDateType"/> for a given event 
        /// </summary>
        /// <param name="eventId">the event id to update</param>
        Task UpdateEventActivityHistory(int eventId);

        /// <summary>
        /// Updates the Xtbl_Event_Location_History of Weekly <see cref="EventLocationHistoryDateType"/>
        /// </summary>
        Task UpdateWeeklyEventActivityHistory();
    }
}