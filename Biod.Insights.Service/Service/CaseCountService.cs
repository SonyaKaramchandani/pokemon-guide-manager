using System.Collections.Generic;
using System.Linq;
using Biod.Insights.Service.Data.CustomModels;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.Event;
using Biod.Insights.Common.Constants;
using Microsoft.Extensions.Logging;

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
    }
}