using System.Collections.Generic;
using System.Linq;
using Biod.Insights.Service.Data.CustomModels;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.Event;
using Biod.Insights.Common.Constants;
using Microsoft.Extensions.Logging;
using System;

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
        public Dictionary<int, EventCaseCountModel> GetIncreasedCaseCount(Dictionary<int, EventCaseCountModel> previous, Dictionary<int, EventCaseCountModel> current, bool isDataFlattened = true)
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
                    
                    e.RawRepCaseCount -= Math.Min(e.RawRepCaseCount, Math.Max(e.ChildrenRepCaseCount, previousReportedCaseCount));
                    e.RawSuspCaseCount -= Math.Min(e.RawSuspCaseCount, Math.Max(e.ChildrenSuspCaseCount, previousSuspectedCaseCount));
                    e.RawConfCaseCount -= Math.Min(e.RawConfCaseCount, Math.Max(e.ChildrenConfCaseCount, previousConfirmedCaseCount));
                    e.RawDeathCount -= Math.Min(e.RawDeathCount, Math.Max(e.ChildrenDeathCount, previousDeathCount));
                    
                    e.ChildrenRepCaseCount = 0;
                    e.ChildrenSuspCaseCount = 0;
                    e.ChildrenConfCaseCount = 0;
                    e.ChildrenDeathCount = 0;

                    return e;
                })
                .Where(e => e.RawRepCaseCount > 0
                            || e.RawSuspCaseCount > 0
                            || e.RawConfCaseCount > 0
                            || e.RawDeathCount > 0)
                .ToDictionary(e => e.GeonameId, e => e);
        }
    }
}