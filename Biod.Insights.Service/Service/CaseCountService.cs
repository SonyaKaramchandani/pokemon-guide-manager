using System.Collections.Generic;
using System.Linq;
using Biod.Insights.Service.Data.CustomModels;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.Event;
using Biod.Products.Common.Constants;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Biod.Insights.Service.Data;
using Biod.Insights.Service.Models.Geoname;

namespace Biod.Insights.Service.Service
{
    /// <summary>
    /// Provides the logic to compute Nesting calculations as defined in https://bluedotglobal.atlassian.net/browse/PT-279
    /// in order to address duplication when dealing with multiple location types/levels.
    /// </summary>
    public class CaseCountService : ICaseCountService
    {
        private readonly ILogger<CaseCountService> _logger;
        private readonly BiodZebraContext _biodZebraContext;

        public CaseCountService(
            ILogger<CaseCountService> logger,
            BiodZebraContext biodZebraContext)
        {
            _logger = logger;
            _biodZebraContext = biodZebraContext;
        }

        public Dictionary<int, EventCaseCountModel> GetCaseCountTree(List<XtblEventLocation> eventLocations)
        {
            var caseCounts = eventLocations
                .ToDictionary(e => e.GeonameId, e => new EventCaseCountModel
                {
                    RawRepCaseCount = e.RepCases ?? 0,
                    RawConfCaseCount = e.ConfCases ?? 0,
                    RawSuspCaseCount = e.SuspCases ?? 0,
                    RawDeathCount = e.Deaths ?? 0,
                    EventDate = e.EventDate,
                    GeonameId = e.GeonameId,
                    Admin1GeonameId = e.Geoname.Admin1GeonameId,
                    CountryGeonameId = e.Geoname.CountryGeonameId ?? -1,
                    LocationType = e.Geoname.LocationType ?? (int) LocationType.City
                });
            EventCaseCountModel.BuildDependencyTree(caseCounts);
            EventCaseCountModel.ApplyNesting(caseCounts);
            return caseCounts;
        }

        public Dictionary<int, EventCaseCountModel> GetCaseCountTree(List<XtblEventLocationJoinResult> eventLocations)
        {
            var caseCounts = eventLocations
                .ToDictionary(e => e.GeonameId, e => new EventCaseCountModel
                {
                    RawRepCaseCount = e.RepCases,
                    RawConfCaseCount = e.ConfCases,
                    RawSuspCaseCount = e.SuspCases,
                    RawDeathCount = e.Deaths,
                    EventDate = e.EventDate,
                    GeonameId = e.GeonameId,
                    Admin1GeonameId = e.Admin1GeonameId,
                    CountryGeonameId = e.CountryGeonameId,
                    LocationType = e.LocationType ?? (int) LocationType.City
                });
            EventCaseCountModel.BuildDependencyTree(caseCounts);
            EventCaseCountModel.ApplyNesting(caseCounts);
            return caseCounts;
        }

        /// <summary>
        /// Calculates the difference between two case count dictionaries. 
        /// This assumes that input dictionaries have nesting applied on them.
        /// As such, if the case-count difference is caused by the sum of its children, 
        /// that case count model is not included in the result.
        /// For example, given the following nested case counts for previous and current:
        /// Toronto     4  -->  Toronto     5
        /// Ontario     4  -->  Ontario     5
        /// Canada      4  -->  Canada      11
        ///                -->  Montreal    6
        /// The resulting list will include Toronto and Montreal.
        /// </summary>
        public Dictionary<int, EventCaseCountModel> GetLocationIncreasedCaseCount(
            Dictionary<int, EventCaseCountModel> previous,
            Dictionary<int, EventCaseCountModel> current,
            bool isDataFlattened = true)
        {
            var flattenedPrevious = isDataFlattened ? previous : EventCaseCountModel.FlattenTree(previous);
            var flattenedCurrent = isDataFlattened ? current : EventCaseCountModel.FlattenTree(current);

            return flattenedCurrent.Values
                .Where(e => e.RawRepCaseCount > e.ChildrenRepCaseCount
                            || e.RawSuspCaseCount > e.ChildrenSuspCaseCount
                            || e.RawConfCaseCount > e.ChildrenConfCaseCount
                            || e.RawDeathCount > e.ChildrenDeathCount)
                .Select(e =>
                {
                    var previousReportedCaseCount = flattenedPrevious.ContainsKey(e.GeonameId) ? flattenedPrevious[e.GeonameId].GetNestedRepCaseCount() : 0;
                    var previousSuspectedCaseCount = flattenedPrevious.ContainsKey(e.GeonameId) ? flattenedPrevious[e.GeonameId].GetNestedSuspCaseCount() : 0;
                    var previousConfirmedCaseCount = flattenedPrevious.ContainsKey(e.GeonameId) ? flattenedPrevious[e.GeonameId].GetNestedConfCaseCount() : 0;
                    var previousDeathCount = flattenedPrevious.ContainsKey(e.GeonameId) ? flattenedPrevious[e.GeonameId].GetNestedDeathCount() : 0;

                    return new EventCaseCountModel
                    {
                        EventDate = e.EventDate,
                        GeonameId = e.GeonameId,
                        Admin1GeonameId = e.Admin1GeonameId,
                        CountryGeonameId = e.CountryGeonameId,
                        LocationType = e.LocationType,
                        RawRepCaseCount = e.RawRepCaseCount - Math.Min(e.RawRepCaseCount, Math.Max(e.ChildrenRepCaseCount, previousReportedCaseCount)),
                        RawSuspCaseCount = e.RawSuspCaseCount - Math.Min(e.RawSuspCaseCount, Math.Max(e.ChildrenSuspCaseCount, previousSuspectedCaseCount)),
                        RawConfCaseCount = e.RawConfCaseCount - Math.Min(e.RawConfCaseCount, Math.Max(e.ChildrenConfCaseCount, previousConfirmedCaseCount)),
                        RawDeathCount = e.RawDeathCount - Math.Min(e.RawDeathCount, Math.Max(e.ChildrenDeathCount, previousDeathCount)),
                        ChildrenRepCaseCount = 0,
                        ChildrenSuspCaseCount = 0,
                        ChildrenConfCaseCount = 0,
                        ChildrenDeathCount = 0
                    };
                })
                .Where(e => e.RawRepCaseCount > 0
                            || e.RawSuspCaseCount > 0
                            || e.RawConfCaseCount > 0
                            || e.RawDeathCount > 0)
                .ToDictionary(e => e.GeonameId, e => e);
        }

        public Dictionary<int, EventCaseCountModel> GetAggregatedIncreasedCaseCount(
            Dictionary<int, EventCaseCountModel> previous,
            Dictionary<int, EventCaseCountModel> current,
            bool isDataFlattened = true)
        {
            var flattenedPrevious = isDataFlattened ? previous : EventCaseCountModel.FlattenTree(previous);
            var flattenedCurrent = isDataFlattened ? current : EventCaseCountModel.FlattenTree(current);

            var deltaDictionary = flattenedCurrent.Values
                .Select(e =>
                {
                    var previousCaseCountModel = flattenedPrevious.ContainsKey(e.GeonameId) ? flattenedPrevious[e.GeonameId] : null;

                    return new EventCaseCountModel
                    {
                        EventDate = e.EventDate,
                        GeonameId = e.GeonameId,
                        Admin1GeonameId = e.Admin1GeonameId,
                        CountryGeonameId = e.CountryGeonameId,
                        LocationType = e.LocationType,
                        RawRepCaseCount = Math.Max(0, e.RawRepCaseCount - (previousCaseCountModel?.GetNestedRepCaseCount() ?? 0)),
                        RawSuspCaseCount = Math.Max(0, e.RawSuspCaseCount - (previousCaseCountModel?.GetNestedSuspCaseCount() ?? 0)),
                        RawConfCaseCount = Math.Max(0, e.RawConfCaseCount - (previousCaseCountModel?.GetNestedConfCaseCount() ?? 0)),
                        RawDeathCount = Math.Max(0, e.RawDeathCount - (previousCaseCountModel?.GetNestedDeathCount() ?? 0)),
                        ChildrenRepCaseCount = Math.Max(0, e.ChildrenRepCaseCount - (previousCaseCountModel?.GetNestedRepCaseCount() ?? 0)),
                        ChildrenSuspCaseCount = Math.Max(0, e.ChildrenSuspCaseCount - (previousCaseCountModel?.GetNestedSuspCaseCount() ?? 0)),
                        ChildrenConfCaseCount = Math.Max(0, e.ChildrenConfCaseCount - (previousCaseCountModel?.GetNestedConfCaseCount() ?? 0)),
                        ChildrenDeathCount = Math.Max(0, e.ChildrenDeathCount - (previousCaseCountModel?.GetNestedDeathCount() ?? 0)),
                    };
                })
                .Where(e => e.GetNestedRepCaseCount() > 0
                            || e.GetNestedSuspCaseCount() > 0
                            || e.GetNestedConfCaseCount() > 0
                            || e.GetNestedDeathCount() > 0)
                .ToDictionary(e => e.GeonameId, e => e);

            EventCaseCountModel.BuildDependencyTree(deltaDictionary);
            return deltaDictionary;
        }

        public async Task<IEnumerable<ProximalCaseCountModel>> GetProximalCaseCount(GetGeonameModel geoname, int diseaseId, int? eventId)
        {
            var eventIntersectionList = (await SqlQuery
                    .GetProximalEventLocations(_biodZebraContext, geoname.GeonameId, diseaseId, eventId))
                .ToList();

            var caseCountByEventId = eventIntersectionList
                .Where(x => x.LocationType == (int) LocationType.Country)
                .GroupBy(x => x.EventId)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.RepCases));

            var proximalCaseCounts = eventIntersectionList
                .GroupBy(x => x.GeonameId)
                .Select(g =>
                {
                    var locationDetails = g.First();
                    var reportedCases = g.Sum(x => x.RepCases);
                    var totalEventCases = g.Sum(x => caseCountByEventId[x.EventId]);

                    // Subtract sum of children case counts for each event location
                    var caseCountDelta = locationDetails.LocationType switch
                    {
                        (int) LocationType.Province => eventIntersectionList
                            .Where(y => y.LocationType == (int) LocationType.City && y.Admin1GeonameId == locationDetails.GeonameId)
                            .Sum(y => y.RepCases),
                        (int) LocationType.Country => eventIntersectionList
                            .Where(y => y.CountryGeonameId == locationDetails.CountryGeonameId)
                            .Where(y => y.LocationType == (int) LocationType.Province
                                        || y.LocationType == (int) LocationType.City && !y.Admin1GeonameId.HasValue) // Edge case: Cities that don't belong to a province (e.g. Vatican City)
                            .Sum(y => y.RepCases),
                        _ => 0
                    };

                    return new
                    {
                        ProximalCases = Math.Max(0, reportedCases - caseCountDelta),
                        TotalEventCases = totalEventCases,
                        locationDetails.IsWithinLocation,
                        EventLocationDetails = new
                        {
                            locationDetails.EventId,
                            locationDetails.GeonameId,
                            locationDetails.Admin1GeonameId,
                            locationDetails.CountryGeonameId,
                            locationDetails.LocationType,
                            locationDetails.DisplayName
                        }
                    };
                })
                // Include proximal event locations with reported cases OR if the event location matches the input geoname (even without reported cases)
                .Where(x => x.EventLocationDetails.GeonameId == geoname.GeonameId || x.ProximalCases > 0 && x.IsWithinLocation)
                .Select(x => new ProximalCaseCountModel
                {
                    EventId = x.EventLocationDetails.EventId,
                    ProximalCases = x.ProximalCases,
                    TotalEventCases = x.TotalEventCases,
                    LocationId = x.EventLocationDetails.GeonameId,
                    LocationName = x.EventLocationDetails.DisplayName,
                    LocationType = x.EventLocationDetails.LocationType,
                    IsWithinLocation = geoname.LocationType switch
                    {
                        (int) LocationType.Country => x.EventLocationDetails.CountryGeonameId == geoname.GeonameId,
                        (int) LocationType.Province => x.EventLocationDetails.Admin1GeonameId == geoname.GeonameId,
                        _ => x.EventLocationDetails.GeonameId == geoname.GeonameId
                    }
                })
                .ToList();

            return proximalCaseCounts.Any() && proximalCaseCounts.Any(x => x.LocationId == geoname.GeonameId)
                ? proximalCaseCounts
                : proximalCaseCounts.Union(new List<ProximalCaseCountModel>
                {
                    new ProximalCaseCountModel
                    {
                        ProximalCases = 0,
                        LocationId = geoname.GeonameId,
                        LocationName = geoname.FullDisplayName,
                        LocationType = geoname.LocationType,
                        IsWithinLocation = true
                    }
                });
        }
    }
}