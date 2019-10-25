using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.EntityModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Dynamic;

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
                customEvents = true
            };
            var @event = zebraEventsInfo.FirstOrDefault(e => e.EventId == EventId);
            if (EventId > 0 && @event == null)
            {
                // Event not found in the provided filter, go to global filters
                zebraEventsInfo = zebraDbContext.usp_ZebraEventGetEventSummary(userId, "", "", "", "", "", "", false).ToList();
                FilterParams = new FilterParamsModel("", "", "", "", false, "", "")
                {
                    hasEventId = EventId > 0,
                    customEvents = false
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
            
            var eventsInfo = FormatAndGetZebraEventsInfoOutbreakPotentials(zebraDbContext, zebraEventsInfo);
            EventsInfo = SortEvents(eventsInfo);

            EventsMap = zebraDbContext.usp_ZebraDashboardGetEventsMap();
            //retrieve data for filtering the events
            BiosecurityRisks = zebraDbContext.usp_ZebraDashboardGetBiosecurityRisks().ToList();
            Diseases = zebraDbContext.usp_ZebraDashboardGetDiseases().ToList();
            DiseaseSeveritys = zebraDbContext.usp_ZebraDashboardGetDiseaseSeveritys().ToList();
            InterventionMethods = zebraDbContext.usp_ZebraDashboardGetInterventionMethods().ToList();
            TransmissionModes = zebraDbContext.usp_ZebraDashboardGetTransmissionModes().ToList();
            OrderByFields = zebraDbContext.usp_ZebraDashboardGetEventsOrderByFields().ToList();
            GroupByFields = zebraDbContext.usp_ZebraDashboardGetEventsGroupByFields().ToList();
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
                    HasOutlookReport = zebraEventInfo.HasOutlookReport == null ? false : zebraEventInfo.HasOutlookReport.Value,
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

        public List<EventsInfoModel> SortEvents(List<EventsInfoModel> events)
        {
            List<EventsInfoModel> sortedEvents = new List<EventsInfoModel>();

            sortedEvents = events.OrderByDescending(s => s.LastUpdatedDate).ToList();
            return sortedEvents;
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

            model.EventsInfo = GroupSort(zebraDbContext, zebraEventsInfo, groupType, sortType);
            model.EventsMap = zebraDbContext.usp_ZebraDashboardGetEventsMap();

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

            model.EventsInfo = GroupSort(zebraDbContext, zebraEventsInfo, groupType, sortType);
            model.EventsMap = zebraDbContext.usp_ZebraDashboardGetEventsMap();

            return model;
        }

        private IEnumerable<EventsInfoModel> GroupSort(BiodZebraEntities dbContext, List<usp_ZebraEventGetEventSummary_Result> events, int groupType, string sortType)
        {
            var eventsInfo = FormatAndGetZebraEventsInfoOutbreakPotentials(dbContext, events);
            eventsInfo = SortEvents(eventsInfo);

            switch (groupType)
            {
                case 1:
                    //do nothing
                    break;
                case 2://local and global
                    foreach (var e in eventsInfo)
                    {
                        if (e.IsLocalOnly)
                        {
                            e.Group = "0-Local to your areas of interest";
                        }
                        else
                        {
                            e.Group = "1-Connected to your areas of interest";
                        }
                    }
                    break;
                case 3://disease
                    foreach (var e in eventsInfo)
                    {
                        e.Group = e.DiseaseName;
                    }
                    break;
                case 4://transmission
                    var toAdd = new List<EventsInfoModel>();
                    foreach (var e in eventsInfo)
                    {
                        if (e.Transmissions == null)
                        {
                            e.Group = "1-No Information";
                        }
                        else
                        {
                            var prefix = "0-";
                            var modes = e.Transmissions.Split(',');
                            if (modes.Length > 0)
                            {
                                for (var i = 0; i < modes.Length; i++)
                                {
                                    if (i == 0)
                                    {
                                        e.Group = prefix + modes[i].Trim();
                                    }
                                    else
                                    {
                                        var clone = e.DeepCopy();
                                        clone.Group = prefix + modes[i].Trim();
                                        toAdd.Add(clone);
                                    }
                                }

                            }
                            else
                            {
                                e.Group = prefix + e.Transmissions;
                            }
                        }
                    }

                    if (toAdd.Count() > 0)
                    {
                        eventsInfo.AddRange(toAdd);
                    }

                    //foreach (var e in eventsInfo)
                    //{
                    //    e.Group = e.Transmissions;
                    //}
                    break;
                case 5:
                    //this option cannot be done as it will be performance bottle neck for loading the global dashboard
                    //it is been set to hidden from the Group List menu
                    foreach (var e in eventsInfo)
                    {
                        e.Group = e.OutbreakPotentialCategory.FirstOrDefault().IsLocalTransmissionPossible ? "Local spread in at least one selected region" : "Local spread not possible in any selected region";
                    }
                    break;
                case 6:
                    foreach (var e in eventsInfo)
                    {
                        //Temp. fix. The BioSecurity for Syndroms are null. 
                        //We considered it as No for now and show it as "No/unknown risk" on UI
                        if (e.BiosecurityRisk == null || e.BiosecurityRisk.ToLower() == "no")
                        {
                            e.Group = "No/unknown risk";
                        }
                        else
                        {
                            e.Group = "Category " + e.BiosecurityRisk;
                        }
                    }
                    break;
                case 7:
                    if (InterventionMethods == null)
                    {
                        InterventionMethods = dbContext.usp_ZebraDashboardGetInterventionMethods().ToList();
                    }
                    var preventionTypes = new HashSet<string>(InterventionMethods.Select(i => i.InterventionDisplayName));
                    foreach (var e in eventsInfo)
                    {
                        e.Group = preventionTypes.Contains(e.Interventions) ? e.Interventions : Constants.PreventionTypes.BEHAVIOURAL;
                    }
                    break;
                default:
                    //do nothing
                    break;
            }

            //Sorting
            var orderByFields = dbContext.usp_ZebraDashboardGetEventsOrderByFields().ToList();
            var orderBy = orderByFields.FirstOrDefault(f => f.ColumnName.Equals(sortType)) ?? orderByFields.First();

            if (orderBy.Id == Constants.OrderByFieldTypes.EVENT_START_DATE)
            {
                // Temporary solution to fix this. Future fix involves refactoring the model to not be a string
                eventsInfo = eventsInfo.OrderByDescending(e => e.StartDate, new CustomComparer.StartDate()).ToList();
            }
            else if (orderBy.Id == Constants.OrderByFieldTypes.RISK_OF_IMPORTATION)
            {
                var localRange = eventsInfo
                    .Where(x => x.LocalSpread)
                    .OrderByDescending(e => e.RepCases)
                    .ThenBy(e => e.EventTitle)
                    .ToList();
                var globalRange = eventsInfo
                    .Where(x => !x.LocalSpread)
                    .OrderByDescending(e => e.ImportationProbabilityMax)
                    .ToList();

                var eventsInfoSortedByLocal = new List<EventsInfoModel>();

                //Proximals(local) are displayed at the top of the event list.
                eventsInfoSortedByLocal.AddRange(localRange);
                eventsInfoSortedByLocal.AddRange(globalRange);
                eventsInfo = eventsInfoSortedByLocal;
            }
            else
            {
                eventsInfo = eventsInfo.OrderBy(orderBy.ColumnName + " DESC").ToList();
            }

            return eventsInfo.OrderBy(x => x.Group);
        }

        public class CustomComparer
        {
            public class StartDate : IComparer<string>
            {
                public int Compare(string x, string y)
                {
                    var retVal = 0;

                    if (x != y)
                    {
                        if (x.ToLower() == "unknown")
                        {
                            retVal = -1;
                        }
                        if (y.ToLower() == "unknown")
                        {
                            retVal = 1;
                        }

                        if (retVal == 0)
                        {
                            if (Convert.ToDateTime(x) > Convert.ToDateTime(y))
                            {
                                retVal = 1;
                            }
                            else
                            {
                                retVal = -1;
                            }
                        }
                    }
                    else
                    {
                        retVal = 0;//0 is for both values equal
                    }

                    return retVal;
                }
            }
            public class RiskLikelihood : IComparer<decimal>
            {
                public int Compare(decimal x, decimal y)
                {
                    var retVal = 0;
                    if (x > y)
                    {
                        retVal = 1;
                    }
                    else if (x < y)
                    {
                        retVal = -1;
                    }
                    return retVal;
                }
            }
            public class RiskExportation : IComparer<decimal>
            {
                public int Compare(decimal x, decimal y)
                {
                    var retVal = 0;
                    if (x > y)
                    {
                        retVal = 1;
                    }
                    else if (x < y)
                    {
                        retVal = -1;
                    }
                    return retVal;
                }
            }
            public class CaseCount : IComparer<int>
            {
                public int Compare(int x, int y)
                {
                    var retVal = 0;
                    if (x > y)
                    {
                        retVal = 1;
                    }
                    else if (x < y)
                    {
                        retVal = -1;
                    }
                    return retVal;
                }
            }
            public class DeathCount : IComparer<int>
            {
                public int Compare(int x, int y)
                {
                    var retVal = 0;
                    if (x > y)
                    {
                        retVal = 1;
                    }
                    else if (x < y)
                    {
                        retVal = -1;
                    }
                    return retVal;
                }
            }
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
    }
}