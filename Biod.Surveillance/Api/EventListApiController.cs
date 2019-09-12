using Biod.Surveillance.Models.Surveillance;
using Biod.Surveillance.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Biod.Surveillance.Api
{
    public class EventListApiController : ApiController
    {
        public List<EventItemModel> Get()
        {
            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
            List<EventItemModel> eventList = new List<EventItemModel>();

            List<EventItemModel> unsortedEventListFormated = new List<EventItemModel>();
            var unsortedEventList = db.Events.ToList();

            foreach (var evt in unsortedEventList)
            {
                var eventID = evt.EventId;
                var articleCount = evt.ProcessedArticles.Count();
                bool elapsedTime = EventViewModelHelper.HasEventElapsedSinceLastReportedCase(db, eventID);

                EventItemModel evtItem = new EventItemModel();
                evtItem.EventId = evt.EventId;
                evtItem.EventTitle = evt.EventTitle;
                evtItem.StartDate = evt.StartDate;
                evtItem.EndDate = evt.EndDate;
                evtItem.IsPublished = evt.IsPublished;
                evtItem.PriorityId = evt.PriorityId;
                evtItem.ArticleCount = evt.ProcessedArticles.Where(s => s.HamTypeId != 1).Count();
                evtItem.Has30DaysElapsed = elapsedTime;
                evtItem.LastUpdatedDate = evt.LastUpdatedDate;

                unsortedEventListFormated.Add(evtItem);
            }


            var highPriorityEvents = unsortedEventListFormated.Where(p => p.PriorityId == 3).OrderByDescending(s => s.LastUpdatedDate).ToList();
            var mediumPriorityEvents = unsortedEventListFormated.Where(p => p.PriorityId == 2).OrderByDescending(s => s.LastUpdatedDate).ToList();
            var lowPriorityEvents = unsortedEventListFormated.Where(p => p.PriorityId == 1).OrderByDescending(s => s.LastUpdatedDate).ToList();

            eventList.AddRange(highPriorityEvents);
            eventList.AddRange(mediumPriorityEvents);
            eventList.AddRange(lowPriorityEvents);

            var LatestDate = eventList.Max(x => x.LastUpdatedDate);
            var LatestModifiedEvent = eventList.First(x => x.LastUpdatedDate == LatestDate);
            var idx = eventList.FindIndex(s => s.EventId == LatestModifiedEvent.EventId);

            eventList.RemoveAt(idx);
            eventList.Insert(0, LatestModifiedEvent);

            return eventList;
        }
    }
}
