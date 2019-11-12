using Biod.Surveillance.Infrastructures;
using Biod.Zebra.Library.EntityModels.Surveillance;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biod.Surveillance.ViewModels
{
    public class SuggestedEventDialogViewModel : EventDialogViewModel
    {
        public SuggestedEventViewModel EventInfo { get; set; }
        public IList<LocationRoot> Locations { get; set; }
        public MultiSelectList ReasonMultiSelect { get; set; }

        public SuggestedEventDialogViewModel(BiodSurveillanceDataEntities DbContext) : this(DbContext, Constants.Event.INVALID_ID.ToString()) { }

        public SuggestedEventDialogViewModel(BiodSurveillanceDataEntities DbContext, string eventId) : base(DbContext)
        {
            var suggestedEvent = DbContext.SuggestedEvents
                .Include(e => e.Geonames)
                .Single(e => e.SuggestedEventId == eventId);

            EventInfo = new SuggestedEventViewModel
            {
                EventId = suggestedEvent.SuggestedEventId,
                EventTitle = suggestedEvent.EventTitle,
                StartDate = null,
                EndDate = null,
                DiseaseId = suggestedEvent.DiseaseId,
                SpeciesId = Constants.Species.HUMAN,
                PriorityId = Constants.Priority.MEDIUM,
                IsPublished = false,
                IsLocalOnly = true,
                Summary = "",
                Notes = ""
            };
            Locations = suggestedEvent.Geonames
                    .GroupBy(el => el.GeonameId)  // group by geoname ID
                    .Select(el => el.FirstOrDefault())  // take first location for each geoname ID
                    .Select(el => new LocationRoot
                    {
                        GeonameId = el.GeonameId,
                        GeoName = el.DisplayName,
                        LocationItems = new List<LocationItem>()
                    })
                    .ToList();
            ReasonMultiSelect = new MultiSelectList(DbContext.SurveillanceEventCreationReasons.ToList(), "ReasonId", "ReasonName", new List<string>().ToArray());
        }
    }
}