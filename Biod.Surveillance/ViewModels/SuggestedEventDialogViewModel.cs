using Biod.Surveillance.Models.Surveillance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biod.Surveillance.ViewModels
{
    public class SuggestedEventDialogViewModel
    {
        public int PriorityId { get; set; }
        public SuggestedEventViewModel EventInfo { get; set; }       
        public IList<LocationRoot> locations { get; set; }
        public MultiSelectList EventReasonMultiSelect { get; set; }
        public IList<Disease> Diseases { get; set; }
        public IList<EventPriority> EventPriorities { get; set; }

        public static List<LocationRoot> GetSuggestedEventLocationById(string EventId)
        {

            try
            {
                BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
                var eventItem = db.SuggestedEvents.Find(EventId);

                var suggestedEventLocs = eventItem.Geonames.ToList();
                List<LocationRoot> response = new List<LocationRoot>();

                foreach (var geoItem in suggestedEventLocs)
                {
                    LocationRoot locRoot = new LocationRoot();
                    locRoot.GeonameId = geoItem.GeonameId;
                    locRoot.GeoName = geoItem.DisplayName;

                    List<LocationItem> locItems = new List<LocationItem>();
                    locRoot.LocationItems = locItems;
                    response.Add(locRoot);

                }               
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static SuggestedEventViewModel GetSuggestedEventById(string Id)
        {
            try
            {
                BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
                var eventItem = db.SuggestedEvents.Find(Id);

                SuggestedEventViewModel suggestedEvent = new SuggestedEventViewModel();
                suggestedEvent.EventId = eventItem.SuggestedEventId;
                suggestedEvent.EventTitle = eventItem.EventTitle;
                suggestedEvent.StartDate = null;
                suggestedEvent.EndDate = null;
                suggestedEvent.DiseaseId = eventItem.DiseaseId;
                suggestedEvent.PriorityId = 2; //Medium
                suggestedEvent.IsPublished = false;
                suggestedEvent.IsLocalOnly = true;
                suggestedEvent.Summary = "";
                suggestedEvent.Notes = "";

                return suggestedEvent;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MultiSelectList GetEventReasonsById(string Id)
        {
            try
            {
                BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();

                //var eventItem = db.Events.Find(Id);
                //string[] selectedReasons = new string[eventItem.EventCreationReasons.Count];
                //var i = 0;
                //foreach (var reason in eventItem.EventCreationReasons)
                //{
                //    selectedReasons[i] = (reason.ReasonId).ToString();
                //    i++;
                //}
                var eventReasons = (from r in db.EventCreationReasons select r);

                List<string> selectedReasonList = new List<string>();
                var selectedReasons = selectedReasonList.ToArray();

                return new MultiSelectList(eventReasons, "ReasonId", "ReasonName", selectedReasons);


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static IList<Disease> GetDiseases()
        {
            try
            {
                BiodSurveillanceDataEntities dbContext = new BiodSurveillanceDataEntities();

                var diseases = dbContext.Diseases.OrderBy(s => s.DiseaseName).ToList();

                return diseases;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IList<EventPriority> GetEventPriorities()
        {
            try
            {
                BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();
                var eventPriorities = db.EventPriorities.ToList();
                return eventPriorities;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}