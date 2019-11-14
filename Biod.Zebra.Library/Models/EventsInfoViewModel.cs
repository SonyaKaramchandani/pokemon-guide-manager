using Biod.Zebra.Library.Infrastructures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;
using Biod.Zebra.Library.Infrastructures.FilterEventResult;
using Biod.Zebra.Library.Models.Map;
using Biod.Zebra.Library.EntityModels.Zebra;

namespace Biod.Zebra.Library.Models
{
    public class EventsInfoViewModel
    {
        public EventsInfoViewModel() { }
        public EventsInfoViewModel(string userId, int EventId, string geonameIds, string diseasesIds, string transmissionModesIds, string interventionMethods, bool locationOnly, string severityRisks, string biosecurityRisks)
        {
            // NOTE: This path is only called during first time Dashboard loading initialization. Do not use this for custom Filter Parameters. Use 'FilterGroupSort' instead.
            
            // Retrieve the user information
            var result = JsonStringResultClass.GetJsonStringResultAsync(
                ConfigurationManager.AppSettings.Get("ZebraApiBaseUrl"),
                "/api/ZebraUserProfile?userId=" + userId,
                ConfigurationManager.AppSettings.Get(@"ZebraApiUserName"),
                ConfigurationManager.AppSettings.Get("ZebraApiPassword")).Result;
            UserProfileDto = JsonConvert.DeserializeObject<UserProfileDto>(result);
            
            // Apply custom settings if no params were passed in
            geonameIds = string.IsNullOrEmpty(geonameIds) ? UserProfileDto.UserNotification.AoiGeonameIds : geonameIds;
            diseasesIds = string.IsNullOrEmpty(diseasesIds) ? UserProfileDto.UserNotification.DiseaseIds : diseasesIds;
            
            // Retrieve events for the provided filter parameters
            var zebraDbContext = new BiodZebraEntities();
            zebraDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));

            var zebraEventsInfo = zebraDbContext.usp_ZebraEventGetCustomEventSummary(userId).ToList();
            FilterParams = new FilterParamsModel(geonameIds, diseasesIds, transmissionModesIds, interventionMethods, locationOnly, severityRisks, biosecurityRisks)
            {
                hasEventId = EventId > 0,
                customEvents = true,
                sortBy = EventResultSortHelper.GetDefaultSortingOption(zebraDbContext),
                groupBy = EventResultGroupHelper.GetDefaultGroupingOption(zebraDbContext)
            };
            var @event = zebraEventsInfo.FirstOrDefault(e => e.EventId == EventId);
            if (EventId > 0 && @event == null)
            {
                // Event not found in the provided filter, go to global filters
                zebraEventsInfo = zebraDbContext.usp_ZebraEventGetEventSummary(userId, "", "", "", "", "", "", false).ToList();
                FilterParams = new FilterParamsModel("", "", "", "", false, "", "")
                {
                    hasEventId = EventId > 0,
                    customEvents = false,
                    sortBy = Constants.OrderByFieldTypes.LAST_UPDATED,
                    groupBy = Constants.GroupByFieldTypes.NONE
                };
            }
            FilterParams.totalEvents = zebraDbContext.Events.Count(e => e.EndDate == null && zebraDbContext.Xtbl_Event_Location.Select(el => el.EventId).Distinct().Contains(e.EventId));
            FilterParams.geonames = string.IsNullOrEmpty(FilterParams.geonameIds)
                ? new List<GeonameModel>()
                : zebraDbContext.usp_SearchGeonamesByGeonameIds(FilterParams.geonameIds).Select(g => new GeonameModel
                {
                    GeonameId = g.GeonameId,
                    LocationDisplayName = g.DisplayName
                }).ToList();

            EventsInfo = GroupSort(zebraDbContext, zebraEventsInfo, FilterParams.groupBy, FilterParams.sortBy);

            EventsMap = zebraDbContext.usp_ZebraDashboardGetEventsMap().ToList();
            //retrieve data for filtering the events
            BiosecurityRisks = zebraDbContext.usp_ZebraDashboardGetBiosecurityRisks().ToList();
            Diseases = zebraDbContext.usp_ZebraDashboardGetDiseases().ToList();
            DiseaseSeveritys = zebraDbContext.usp_ZebraDashboardGetDiseaseSeveritys().ToList();
            InterventionMethods = zebraDbContext.usp_ZebraDashboardGetInterventionMethods().ToList();
            TransmissionModes = zebraDbContext.usp_ZebraDashboardGetTransmissionModes().ToList();
            OrderByFields = zebraDbContext.usp_ZebraDashboardGetEventsOrderByFields().ToList();
            GroupByFields = zebraDbContext.usp_ZebraDashboardGetEventsGroupByFields().ToList();
            MapPinModel = MapPinModel.FromEventsInfoViewModel(this);
        }

        private List<EventsInfoModel> FormatAndGetZebraEventsInfoOutbreakPotentials(BiodZebraEntities zebraDbContext, List<usp_ZebraEventGetEventSummary_Result> zebraEventsInfo)
        {
            //get the outbreak potentials effective message
            List<EventsInfoModel> eventsInfoModel = new List<EventsInfoModel>();
            foreach (var zebraEventInfo in zebraEventsInfo)
            {
                eventsInfoModel.Add(new EventsInfoModel()
                {
                    EventId = zebraEventInfo.EventId.Value,
                    EventTitle = zebraEventInfo.EventTitle,
                    Notes = zebraEventInfo.Notes,
                    ExportationPriorityTitle = zebraEventInfo.ExportationPriorityTitle,
                    ExportationProbabilityName = zebraEventInfo.ExportationProbabilityName,
                    StartDate = zebraEventInfo.StartDate.CompareTo(new DateTime(1900, 12, 31)) > 0 ? StringFormattingHelper.FormatShortDate(zebraEventInfo.StartDate) : "Unknown",
                    EndDate = zebraEventInfo.EndDate.CompareTo(new DateTime(2900, 1, 1)) < 0 ? StringFormattingHelper.FormatShortDate(zebraEventInfo.EndDate) : "Present",
                    LastUpdatedDate = zebraEventInfo.LastUpdatedDate,
                    Summary = zebraEventInfo.Summary ?? "-",
                    IsLocalOnly = zebraEventInfo.IsLocalOnly,
                    DiseaseName = zebraEventInfo.DiseaseName,
                    DiseaseId = zebraEventInfo.DiseaseId ?? -1,
                    BiosecurityRisk = zebraEventInfo.BiosecurityRisk,
                    Transmissions = zebraEventInfo.Transmissions,
                    Interventions = zebraEventInfo.Interventions,
                    RepCases = zebraEventInfo.RepCases ?? -1,
                    Deaths = zebraEventInfo.Deaths ?? -1,
                    Group = String.Empty,
                    ExportationProbabilityMin = zebraEventInfo.ExportationProbabilityMin != null ? zebraEventInfo.ExportationProbabilityMin.Value : -1, //-1 means Unlikely
                    ExportationProbabilityMax = zebraEventInfo.ExportationProbabilityMax != null ? zebraEventInfo.ExportationProbabilityMax.Value : -1, //-1 means Unlikely
                    ExportationInfectedTravellersMin = zebraEventInfo.ExportationInfectedTravellersMin != null ? zebraEventInfo.ExportationInfectedTravellersMin.Value : -1, //-1 means Negligible
                    ExportationInfectedTravellersMax = zebraEventInfo.ExportationInfectedTravellersMax != null ? zebraEventInfo.ExportationInfectedTravellersMax.Value : -1, //-1 means Negligible
                    EventCountry = new EventCountryModel()
                    {
                        CountryName = zebraEventInfo.CountryName,
                        CountryCentroidAsText = zebraEventInfo.CountryCentroidAsText
                    },
                    ImportationProbabilityMin = zebraEventInfo.ImportationMinProbability != null ? zebraEventInfo.ImportationMinProbability.Value : -1, //-1 means Not Available,
                    ImportationProbabilityMax = zebraEventInfo.ImportationMaxProbability != null ? zebraEventInfo.ImportationMaxProbability.Value : -1, //-1 means Not Available,
                    ImportationInfectedTravellersMin = zebraEventInfo.ImportationInfectedTravellersMin != null ? zebraEventInfo.ImportationInfectedTravellersMin.Value : -1, //-1 means Not Available,
                    ImportationInfectedTravellersMax = zebraEventInfo.ImportationInfectedTravellersMax != null ? zebraEventInfo.ImportationInfectedTravellersMax.Value : -1, //-1 means Not Available,
                    ImportationProbabilityName = RiskProbabilityHelper.GetProbabilityName(zebraEventInfo.ImportationMaxProbability),
                    LocalSpread = zebraEventInfo.LocalSpread != null && zebraEventInfo.LocalSpread != 0 ? true : false,
                    //TODO: affect performance
                    SourceNameList = SourceNameHelper.GetSourceName(zebraDbContext.usp_ZebraEventGetArticlesByEventId(zebraEventInfo.EventId)),
                    //Getting this EventLocationsOutbreakPotentialModel for each event is performance issue. 
                    //We prefer to remove the IsLocalTransmissionPossible grouping functionality from the global dashborad 
                    //This property will be removed from getting it for each event and will be added to the EventDetailViewModel to get it only when clicking on specific event
                    OutbreakPotentialCategory = new List<EventLocationsOutbreakPotentialModel>()
                });

            }

            return eventsInfoModel;
        }

        public EventsInfoViewModel FilterGroupSort(string userId, string geonameIds = "", string diseasesIds = "", string transmissionModesIds = "",
            string interventionMethods = "", string severityRisks = "", string biosecurityRisks = "", bool locationOnly = false, int groupType = 1, string sortType = "LastUpdatedDate")
        {
            var model = new EventsInfoViewModel();
            BiodZebraEntities zebraDbContext = new BiodZebraEntities();
            zebraDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));

            var zebraEventsInfo = zebraDbContext.usp_ZebraEventGetEventSummary(userId, geonameIds, diseasesIds, transmissionModesIds, interventionMethods, severityRisks, biosecurityRisks, locationOnly).ToList();
            model.FilterParams = new FilterParamsModel(geonameIds, diseasesIds, transmissionModesIds, interventionMethods, locationOnly, severityRisks, biosecurityRisks)
            {
                hasEventId = false,
                customEvents = false,
                totalEvents = zebraDbContext.Events.Count(e => e.EndDate == null && zebraDbContext.Xtbl_Event_Location.Select(el => el.EventId).Distinct().Contains(e.EventId)),
                geonames = string.IsNullOrEmpty(geonameIds)
                    ? new List<GeonameModel>()
                    : zebraDbContext.usp_SearchGeonamesByGeonameIds(geonameIds).Select(g => new GeonameModel
                    {
                        GeonameId = g.GeonameId,
                        LocationDisplayName = g.DisplayName
                    }).ToList()
            };

            var sortTypeId = zebraDbContext.usp_ZebraDashboardGetEventsOrderByFields().FirstOrDefault(o => o.ColumnName == sortType)?.Id ?? EventResultSortHelper.GetDefaultSortingOption(zebraDbContext);
            model.EventsInfo = model.GroupSort(zebraDbContext, zebraEventsInfo, groupType, sortTypeId);
            model.EventsMap = zebraDbContext.usp_ZebraDashboardGetEventsMap().ToList();
            model.MapPinModel = MapPinModel.FromEventsInfoViewModel(model);

            return model;
        }

        public EventsInfoViewModel CustomGroupSort(string userId, int groupType = 1, string sortType = "LastUpdatedDate")
        {
            // Retrieve the user information
            var result = JsonStringResultClass.GetJsonStringResultAsync(
                ConfigurationManager.AppSettings.Get("ZebraApiBaseUrl"),
                "/api/ZebraUserProfile?userId=" + userId,
                ConfigurationManager.AppSettings.Get(@"ZebraApiUserName"),
                ConfigurationManager.AppSettings.Get("ZebraApiPassword")).Result;
            UserProfileDto = JsonConvert.DeserializeObject<UserProfileDto>(result);
            var geonameIds = UserProfileDto.UserNotification.AoiGeonameIds;
            
            var model = new EventsInfoViewModel();
            BiodZebraEntities zebraDbContext = new BiodZebraEntities();
            zebraDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));

            var zebraEventsInfo = zebraDbContext.usp_ZebraEventGetCustomEventSummary(userId).ToList();
            model.FilterParams = new FilterParamsModel(geonameIds, "", "", "", false, "", "")
            {
                customEvents = true,
                totalEvents = zebraDbContext.Events.Count(e => e.EndDate == null && zebraDbContext.Xtbl_Event_Location.Select(el => el.EventId).Distinct().Contains(e.EventId)),
                geonames = string.IsNullOrEmpty(geonameIds)
                    ? new List<GeonameModel>()
                    : zebraDbContext.usp_SearchGeonamesByGeonameIds(geonameIds).Select(g => new GeonameModel
                    {
                        GeonameId = g.GeonameId,
                        LocationDisplayName = g.DisplayName
                    }).ToList()
            };

            var sortTypeId = zebraDbContext.usp_ZebraDashboardGetEventsOrderByFields().FirstOrDefault(o => o.ColumnName == sortType)?.Id ?? EventResultSortHelper.GetDefaultSortingOption(zebraDbContext);
            model.EventsInfo = model.GroupSort(zebraDbContext, zebraEventsInfo, groupType, sortTypeId);
            model.EventsMap = zebraDbContext.usp_ZebraDashboardGetEventsMap().ToList();
            model.MapPinModel = MapPinModel.FromEventsInfoViewModel(model);

            return model;
        }

        private IEnumerable<EventsInfoModel> GroupSort(BiodZebraEntities dbContext, List<usp_ZebraEventGetEventSummary_Result> events, int groupType, int sortType)
        {
            var eventsInfo = FormatAndGetZebraEventsInfoOutbreakPotentials(dbContext, events);

            var groupTypeId = groupType;
            if (!FilterParams.geonames.Any() && groupTypeId == Constants.GroupByFieldTypes.DISEASE_RISK)
            {
                groupTypeId = Constants.GroupByFieldTypes.NONE;
            }
            eventsInfo = EventResultGroupHelper.GroupEvents(
                groupTypeId, 
                eventsInfo, 
                InterventionMethods ?? dbContext.usp_ZebraDashboardGetInterventionMethods().ToList());
            FilterParams.groupBy = groupTypeId;

            var sortTypeId = sortType;
            if (!FilterParams.geonames.Any() && sortTypeId == Constants.OrderByFieldTypes.RISK_OF_IMPORTATION)
            {
                // No risk of importation sorting when no AOI, default to last updated
                sortTypeId = Constants.OrderByFieldTypes.LAST_UPDATED;
            }
            eventsInfo = EventResultSortHelper.SortEvents(sortTypeId, eventsInfo);
            FilterParams.sortBy = sortTypeId;

            return eventsInfo.OrderBy(x => x.Group);
        }

        public FilterParamsModel FilterParams { get; set; }
        public UserProfileDto UserProfileDto { get; set; }
        public IEnumerable<EventsInfoModel> EventsInfo { get; set; }
        public IEnumerable<usp_ZebraDashboardGetEventsMap_Result> EventsMap { get; set; }
        public List<usp_ZebraDashboardGetBiosecurityRisks_Result> BiosecurityRisks { get; set; }
        public List<usp_ZebraDashboardGetDiseases_Result> Diseases { get; set; }
        public List<usp_ZebraDashboardGetDiseaseSeveritys_Result> DiseaseSeveritys { get; set; }
        public List<usp_ZebraDashboardGetInterventionMethods_Result> InterventionMethods { get; set; }
        public List<usp_ZebraDashboardGetTransmissionModes_Result> TransmissionModes { get; set; }
        public List<usp_ZebraDashboardGetEventsOrderByFields_Result> OrderByFields { get; set; }
        public List<usp_ZebraDashboardGetEventsGroupByFields_Result> GroupByFields { get; set; }
        public MapPinModel MapPinModel { get; set; } 
    }
}