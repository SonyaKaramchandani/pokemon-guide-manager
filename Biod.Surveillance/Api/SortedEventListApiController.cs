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
    public class SortedEventListApiController : ApiController
    {
        public List<EventItemModel> Get(string eventType, string sortBy, string sortOrder)
        {
            BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
            List<EventItemModel> eventList = new List<EventItemModel>();
            var eventListUnsorted = (eventType == "active") ? db.Events.Where(d => d.EndDate == null) : db.Events.Where(d => d.EndDate != null);

            List<EventItemModel> unsortedEventListFormatted = new List<EventItemModel>();
            foreach (var evt in eventListUnsorted)
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
                unsortedEventListFormatted.Add(evtItem);
            }

            if (sortBy == "LastModified")
            {
                var sortedByLastModified = (sortOrder == "Asc") ? unsortedEventListFormatted.OrderBy(s => s.LastUpdatedDate).ToList() : unsortedEventListFormatted.OrderByDescending(s => s.LastUpdatedDate).ToList();
                eventList.AddRange(sortedByLastModified);
            }
            else if (sortBy == "StartDate")
            {
                //Start Date
                var sortedByStartDate = (sortOrder == "Asc") ? unsortedEventListFormatted.OrderBy(s => s.StartDate).ToList() : unsortedEventListFormatted.OrderByDescending(s => s.StartDate).ToList();
                eventList.AddRange(sortedByStartDate);
            }
            else if (sortBy == "EndDate")
            {
                var sortedByEndDate = (sortOrder == "Asc") ? unsortedEventListFormatted.OrderBy(s => s.EndDate).ToList() : unsortedEventListFormatted.OrderByDescending(s => s.EndDate).ToList();
                eventList.AddRange(sortedByEndDate);
            }
            else if (sortBy == "Priority")
            {
                var highPriorityEvents = unsortedEventListFormatted.Where(p => p.PriorityId == 3).OrderByDescending(s => s.LastUpdatedDate).ToList();
                var mediumPriorityEvents = unsortedEventListFormatted.Where(p => p.PriorityId == 2).OrderByDescending(s => s.LastUpdatedDate).ToList();
                var lowPriorityEvents = unsortedEventListFormatted.Where(p => p.PriorityId == 1).OrderByDescending(s => s.LastUpdatedDate).ToList();

                if (sortOrder == "Asc")
                {
                    eventList.AddRange(lowPriorityEvents);
                    eventList.AddRange(mediumPriorityEvents);
                    eventList.AddRange(highPriorityEvents);
                }
                else
                {
                    eventList.AddRange(highPriorityEvents);
                    eventList.AddRange(mediumPriorityEvents);
                    eventList.AddRange(lowPriorityEvents);
                }
            }
            else if (sortBy == "Alphabetical")
            {
                //Alphabetic
                var sortedByAlphabate = (sortOrder == "Asc") ? unsortedEventListFormatted.OrderBy(a => a.EventTitle).ToList() : unsortedEventListFormatted.OrderByDescending(a => a.EventTitle).ToList();
                eventList.AddRange(sortedByAlphabate);
            }
            else
            {
                //API Visibility
                var sortedByAPIvisibility = (sortOrder == "Asc") ? unsortedEventListFormatted.OrderBy(p => p.IsPublished).ToList() : unsortedEventListFormatted.OrderByDescending(p => p.IsPublished).ToList();
                eventList.AddRange(sortedByAPIvisibility);
            }
            return eventList;
        }
    }
}
