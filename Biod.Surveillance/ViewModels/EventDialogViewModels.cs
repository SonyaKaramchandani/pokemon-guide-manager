using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http.Headers;
using Biod.Surveillance.Models.Surveillance;
using System.Web.Mvc;
using Biod.Surveillance.Infrastructures;

namespace Biod.Surveillance.ViewModels
{
    public class LocationItem
    {
        public DateTime EventDate { get; set; }
        public int SuspCases { get; set; }
        public int ConfCases { get; set; }
        public int RepCases { get; set; }
        public int Deaths { get; set; }

    }
    public class LocationRoot
    {
        public int GeonameId { get; set; }
        public string GeoName { get; set; }
        public List<LocationItem> LocationItems { get; set; }
    }

    public class EventIncubationInfo
    {
        public bool IsIncubationExceeded { get; set; }
        public int? MultipleOfIncubation { get; set; }
    }

    public abstract class EventDialogViewModel
    {
        public IList<Species> Species { get; }
        public IList<Disease> Diseases { get; }
        public IList<EventPriority> EventPriorities { get; }

        public EventDialogViewModel(BiodSurveillanceDataEntities DbContext)
        {
            Species = DbContext.Species.OrderBy(s => s.SpeciesName).ToList();
            Diseases = DbContext.Diseases.OrderBy(s => s.DiseaseName).ToList();
            EventPriorities = DbContext.EventPriorities.ToList();
        }
    }

    public class CreatedEventDialogViewModel : EventDialogViewModel
    {
        public Event EventInfo { get; }
        public IList<LocationRoot> Locations { get; }
        public MultiSelectList ReasonMultiSelect { get; }
        public bool Has30DaysElapsed { get; }

        public CreatedEventDialogViewModel(BiodSurveillanceDataEntities DbContext) : this(DbContext, Constants.Event.INVALID_ID) { }

        public CreatedEventDialogViewModel(BiodSurveillanceDataEntities DbContext, int eventId) : base(DbContext)
        {
            EventInfo = eventId == Constants.Event.INVALID_ID ?
                 new Event
                 {
                     EventTitle = "Untitled Event",
                     EventId = Constants.Event.INVALID_ID,
                     DiseaseId = DbContext.Diseases.OrderBy(d => d.DiseaseName).First().DiseaseId,
                     SpeciesId = Constants.Species.HUMAN,
                     IsPublished = false,
                     IsPublishedChangesToApi = false,
                     IsLocalOnly = true,
                     PriorityId = Constants.Priority.MEDIUM,
                     Summary = "",
                     Notes = "",
                     LastUpdatedDate = DateTime.Now
                 } :
                 DbContext.Events
                    .Include(e => e.Xtbl_Event_Location.Select(el => el.Geoname))
                    .Include(e => e.EventCreationReasons)
                    .Single(e => e.EventId == eventId);
            Locations = EventInfo.Xtbl_Event_Location
                    .GroupBy(el => el.GeonameId)  // group by geoname ID
                    .Select(el => el.FirstOrDefault())  // take first location for each geoname ID
                    .Select(el => new LocationRoot
                    {
                        GeonameId = el.GeonameId,
                        GeoName = el.Geoname.DisplayName,
                        LocationItems = EventInfo.Xtbl_Event_Location
                            .Where(loc => loc.GeonameId == el.GeonameId)
                            .Select(item => new LocationItem
                            {
                                EventDate = item.EventDate,
                                SuspCases = item.SuspCases ?? 0,
                                ConfCases = item.ConfCases ?? 0,
                                RepCases = item.RepCases ?? 0,
                                Deaths = item.Deaths ?? 0
                            })
                            .ToList()
                    })
                    .ToList();

            string[] selectedReasons = eventId == Constants.Event.INVALID_ID ?
                null :
                EventInfo.EventCreationReasons
                    .Select(item => item.ReasonId.ToString())
                    .ToArray();
            ReasonMultiSelect = new MultiSelectList(DbContext.EventCreationReasons.ToList(), "ReasonId", "ReasonName", selectedReasons);
            Has30DaysElapsed = EventViewModelHelper.HasEventElapsedSinceLastReportedCase(EventInfo.Xtbl_Event_Location, eventId);
        }
    }

    public class EventViewModelHelper
    {
        public static List<EventItemModel> GetAllEvents(BiodSurveillanceDataEntities DbContext)
        {
            var events = DbContext.Events
                .Include(e => e.ProcessedArticles)
                .OrderByDescending(p => p.PriorityId)
                .ThenByDescending(d => d.LastUpdatedDate)
                .ToList();
            var processedArticleCount = events.ToDictionary(e => e.EventId, e => e.ProcessedArticles.Where(s => s.HamTypeId != 1).Count());
            var locations = DbContext.Xtbl_Event_Location.ToList();

            List<EventItemModel> eventList = events.Select(e => new EventItemModel
            {
                EventId = e.EventId,
                EventTitle = e.EventTitle,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                IsPublished = e.IsPublished,
                PriorityId = e.PriorityId,
                ArticleCount = processedArticleCount[e.EventId],
                Has30DaysElapsed = HasEventElapsedSinceLastReportedCase(locations, e.EventId),
                LastUpdatedDate = e.LastUpdatedDate,
                IsSuggested = false
            }).ToList();

            var LatestModifiedEvent = eventList.OrderByDescending(e => e.LastUpdatedDate).First();
            var LatestModifiedEventIndex = eventList.FindIndex(s => s.EventId == LatestModifiedEvent.EventId);

            // This allows the user to easily find the event just updated since it will be on top of the list
            eventList.RemoveAt(LatestModifiedEventIndex);
            eventList.Insert(0, LatestModifiedEvent);

            return eventList;
        }

        public static List<SuggestedEventItemModel> GetSuggestedEvents(BiodSurveillanceDataEntities DbContext)
        {
            return DbContext.SuggestedEvents.Select(
                item => new SuggestedEventItemModel
                {
                    EventId = item.SuggestedEventId.ToString(),
                    EventTitle = item.EventTitle,
                    StartDate = null,
                    EndDate = null,
                    ArticleCount = item.ProcessedArticles.Where(s => s.HamTypeId != 1).Count()
                }).ToList();
        }

        public static MultiSelectList GetAllEventsForArticle(BiodSurveillanceDataEntities DbContext, string articleID)
        {
            var article = DbContext.ProcessedArticles.Find(articleID);
            string[] selectedValues = article.Events
                .Select(item => item.EventId.ToString())
                .ToArray();

            return new MultiSelectList(DbContext.Events.ToList(), "EventId", "EventTitle", selectedValues);
        }

        public static bool HasEventElapsedSinceLastReportedCase(ICollection<Xtbl_Event_Location> Locations, int eventId)
        {
            if (Locations.Count == 0)
            {
                return false;
            }

            var latestModifiedDate = Locations
                .Where(e => e.EventId == eventId)
                .OrderByDescending(e => e.EventDate)
                .FirstOrDefault()?.EventDate;
            var eventDuration = latestModifiedDate + TimeSpan.FromDays(30);

            return DateTime.Now > eventDuration;
        }
    }
}