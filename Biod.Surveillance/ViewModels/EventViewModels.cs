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

    public class EventViewModel
    {
        public Event EventInfo { get; }
        public IList<Species> Species { get; }
        public IList<Disease> Diseases { get; }
        public IList<EventPriority> EventPriorities { get; }
        public IList<LocationRoot> Locations { get; }
        public MultiSelectList ReasonMultiSelect { get; }
        public bool Has30DaysElapsed { get; }

        public EventViewModel(BiodSurveillanceDataEntities DbContext) : this(DbContext, -1) { }

        public EventViewModel(BiodSurveillanceDataEntities DbContext, int eventId)
        {
            EventInfo = eventId == -1 ?
                 new Event
                 {
                     EventTitle = "Untitled Event",
                     EventId = 012,
                     DiseaseId = 0,
                     SpeciesId = 1,  // 1 = Human
                     IsPublished = false,
                     StartDate = null,
                     EndDate = null,
                     IsLocalOnly = true,
                     PriorityId = 2,
                     Summary = "",
                     Notes = "",
                     LastUpdatedDate = DateTime.Now
                 } :
                 DbContext.Events
                    .Include(e => e.Xtbl_Event_Location.Select(el => el.Geoname))
                    .Include(e => e.EventCreationReasons)
                    .Single(e => e.EventId == eventId);
            Species = DbContext.Species.OrderBy(s => s.SpeciesName).ToList();
            Diseases = DbContext.Diseases.OrderBy(s => s.DiseaseName).ToList();
            EventPriorities = DbContext.EventPriorities.ToList();
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

            string[] selectedReasons = eventId == -1 ?
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
            var events = DbContext.Events.OrderByDescending(p => p.PriorityId).ThenByDescending(d => d.LastUpdatedDate).ToList();
            var processedArticleCount = events.ToDictionary(e => e.EventId, e => e.ProcessedArticles.Where(s => s.HamTypeId != 1).Count());

            List<EventItemModel> eventList = new List<EventItemModel>();
            foreach (var eventItem in events)
            {
                eventList.Add(new EventItemModel
                {
                    EventId = eventItem.EventId,
                    EventTitle = eventItem.EventTitle,
                    StartDate = eventItem.StartDate,
                    EndDate = eventItem.EndDate,
                    IsPublished = eventItem.IsPublished,
                    PriorityId = eventItem.PriorityId,
                    ArticleCount = processedArticleCount[eventItem.EventId],
                    Has30DaysElapsed = HasEventElapsedSinceLastReportedCase(DbContext, eventItem.EventId),
                    LastUpdatedDate = eventItem.LastUpdatedDate,
                    IsSuggested = false
                });
            }

            var LatestDate = eventList.Max(x => x.LastUpdatedDate);
            var LatestModifiedEvent = eventList.First(x => x.LastUpdatedDate == LatestDate);
            var idx = eventList.FindIndex(s => s.EventId == LatestModifiedEvent.EventId);

            eventList.RemoveAt(idx);
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

        public static bool HasEventElapsedSinceLastReportedCase(BiodSurveillanceDataEntities DbContext, int eventId)
        {
            return HasEventElapsedSinceLastReportedCase(DbContext.Xtbl_Event_Location.ToList(), eventId);
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