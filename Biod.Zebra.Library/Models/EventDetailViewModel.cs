﻿using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.EntityModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Biod.Zebra.Library.Models
{
    public class EventDetailViewModel
    {
        public EventDetailViewModel()
        {
        }
        public EventDetailViewModel(string userId, int eventId, List<GeonameModel> aoiLocations, FilterParamsModel filterParams)
        {
            var zebraDbContext = new BiodZebraEntities();
            zebraDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));
            var outbreakPotentialCategories = zebraDbContext.usp_ZebraDashboardGetOutbreakPotentialCategories().ToList();
            
            var zebraEventsInfo = zebraDbContext.usp_ZebraEventGetEventSummary(
                userId, 
                filterParams.geonameIds, 
                filterParams.diseasesIds, 
                filterParams.transmissionModesIds, 
                filterParams.prevensionMethods, 
                filterParams.severityRisks, 
                filterParams.biosecurityRisks, 
                filterParams.locationOnly).ToList();
            filterParams.customEvents = true;
            filterParams.geonames = aoiLocations;
            filterParams.totalEvents = zebraEventsInfo.Count();
            filterParams.hasEventId = true;
            FilterParams = filterParams;
            
            var zebraEvent = zebraEventsInfo.FirstOrDefault(e => e.EventId == eventId);
            if (zebraEvent == null)
            {
                zebraEventsInfo = zebraDbContext.usp_ZebraEventGetEventSummary(userId, "", "", "", "", "", "", false).ToList();
                zebraEvent = zebraEventsInfo.First(e => e.EventId == eventId);
                FilterParams = new FilterParamsModel("", "", "", "", false, "", "")
                {
                    customEvents = false,
                    geonames = aoiLocations,
                    totalEvents = zebraEventsInfo.Count(),
                    hasEventId = true
                };
            }

            EventInfo = new EventsInfoModel
            {
                EventId = zebraEvent.EventId.Value,
                EventTitle = zebraEvent.EventTitle,
                Notes = zebraEvent.Notes,
                ExportationPriorityTitle = zebraEvent.ExportationPriorityTitle,
                ExportationProbabilityName = zebraEvent.ExportationProbabilityName,
                DiseaseName = zebraEvent.DiseaseName,
                StartDate = zebraEvent.StartDate.CompareTo(new DateTime(1900, 12, 31)) > 0 ? StringFormattingHelper.FormatShortDate(zebraEvent.StartDate) : "Unknown",
                EndDate = zebraEvent.EndDate.CompareTo(new DateTime(2900, 1, 1)) < 0 ? StringFormattingHelper.FormatShortDate(zebraEvent.EndDate) : "Present",
                LastUpdatedDate = zebraEvent.LastUpdatedDate,
                Summary = zebraEvent.Summary ?? "-",
                HasOutlookReport = zebraEvent.HasOutlookReport ?? false,
                IsLocalOnly = zebraEvent.IsLocalOnly,
                ExportationProbabilityMin = zebraEvent.ExportationProbabilityMin ?? -1, //-1 means Unlikely
                ExportationProbabilityMax = zebraEvent.ExportationProbabilityMax ?? -1, //-1 means Unlikely
                ExportationInfectedTravellersMin = zebraEvent.ExportationInfectedTravellersMin ?? -1, //-1 means Negligible
                ExportationInfectedTravellersMax = zebraEvent.ExportationInfectedTravellersMax ?? -1, //-1 means Negligible
                OutbreakPotentialCategory = OutbreakPotentialCategoryModel.GetOutbreakPotentialCategory(
                    zebraDbContext,
                    zebraEvent.EventId.Value,
                    zebraEvent.DiseaseId.Value,
                    //Temp fix. Null should not be retrieved from the DS diseases internal APIs. Assigned 5 for it as "Unknown"
                    zebraEvent.OutbreakPotentialAttributeId ?? 5,
                    outbreakPotentialCategories,
                    filterParams.geonameIds
                ),
                EventCountry = new EventCountryModel()
                {
                    CountryName = zebraEvent.CountryName,
                    CountryCentroidAsText = zebraEvent.CountryCentroidAsText
                },
                ImportationInfectedTravellersMax = zebraEvent.ImportationInfectedTravellersMax ?? -1,
                ImportationInfectedTravellersMin = zebraEvent.ImportationInfectedTravellersMin ?? -1,
                SourceNameList = SourceNameHelper.GetSourceName(zebraDbContext.usp_ZebraEventGetArticlesByEventId(zebraEvent.EventId)),
            };

            EventDiseaseInfo = zebraDbContext.usp_ZebraEventGetDiseaseByEventId(eventId).FirstOrDefault();
            var totalCaseCounts = zebraDbContext.usp_ZebraEventGetCaseCountByEventId(eventId).ToList();
            EventCaseCountSummary = totalCaseCounts.FirstOrDefault(x => x.GeonameId == -1);
            EventCaseCounts = totalCaseCounts.Where(x => x.GeonameId != -1);
            EventSourceAirports = zebraDbContext.usp_ZebraEventGetSourceAirportsByEventId(eventId).ToList();
            //Passed GeonameIds to usp_ZebraEventGetDestinationAirportsByEventId to associate the destination airports to the AOI
            EventDestinationAirports = zebraDbContext.usp_ZebraEventGetDestinationAirportsByEventId(eventId, filterParams.geonameIds).ToList();
            EventArticles = zebraDbContext.usp_ZebraEventGetArticlesByEventId(eventId);
            SelectedAreaOutbreakInfo = IntersectOutbreakLocation(
                outbreakPotentialCategories,
                EventInfo.OutbreakPotentialCategory,
                aoiLocations);

            ImportationRisk = zebraDbContext.usp_ZebraEventGetImportationRisk(eventId, filterParams.geonameIds).SingleOrDefault();
        }

        private List<EventLocationsOutbreakPotentialModel> IntersectOutbreakLocation(
            List<usp_ZebraDashboardGetOutbreakPotentialCategories_Result> outbreakPotentialCategories,
            List<EventLocationsOutbreakPotentialModel> riskValueLocations,
            List<GeonameModel> aoiLocations)
        {
            var retVal = new List<EventLocationsOutbreakPotentialModel>();

            if (aoiLocations == null || aoiLocations.Count <= 0) return retVal;
            foreach (var aoiLocation in aoiLocations)
            {
                var elop = riskValueLocations.FirstOrDefault(x => x.GeonameId == aoiLocation.GeonameId);
                if (elop != null)
                {
                    retVal.Add(elop);
                }
                else if (riskValueLocations.Count > 0 && riskValueLocations[0].AttributeId <= 2)
                {
                    var newElop = new EventLocationsOutbreakPotentialModel
                    {
                        GeonameId = aoiLocation.GeonameId,
                        LocationDisplayName = aoiLocation.LocationDisplayName,
                        EffectiveMessage = riskValueLocations[0].EffectiveMessage,
                        AttributeId = riskValueLocations[0].AttributeId
                    };
                    retVal.Add(newElop);
                }
                else
                {
                    var newElop = new EventLocationsOutbreakPotentialModel
                    {
                        GeonameId = aoiLocation.GeonameId,
                        LocationDisplayName = aoiLocation.LocationDisplayName,
                        EffectiveMessage = outbreakPotentialCategories.FirstOrDefault(x => x.AttributeId == 4)?.EffectiveMessage
                    };
                    retVal.Add(newElop);
                }
            }

            return retVal;
        }

        public FilterParamsModel FilterParams { get; set; }
        public EventsInfoModel EventInfo { get; set; }
        public usp_ZebraEventGetDiseaseByEventId_Result EventDiseaseInfo { get; set; }
        public usp_ZebraEventGetCaseCountByEventId_Result EventCaseCountSummary { get; set; }
        public IEnumerable<usp_ZebraEventGetCaseCountByEventId_Result> EventCaseCounts { get; set; }
        public List<usp_ZebraEventGetSourceAirportsByEventId_Result> EventSourceAirports { get; set; }
        public List<usp_ZebraEventGetDestinationAirportsByEventId_Result> EventDestinationAirports { get; set; }
        public IEnumerable<usp_ZebraEventGetArticlesByEventId_Result> EventArticles { get; set; }
        public List<EventLocationsOutbreakPotentialModel> SelectedAreaOutbreakInfo { get; set; }
        public usp_ZebraEventGetImportationRisk_Result ImportationRisk { get; set; }
    }
}