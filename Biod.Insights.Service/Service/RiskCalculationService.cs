using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Data;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.Risk;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Service.Service
{
    public class RiskCalculationService : IRiskCalculationService
    {
        private readonly ILogger<RiskCalculationService> _logger;
        private readonly BiodZebraContext _biodZebraContext;
        private readonly IEventService _eventService;

        /// <summary>
        /// Risk Calculation service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        /// <param name="eventService">The event service</param>
        public RiskCalculationService(
            BiodZebraContext biodZebraContext,
            ILogger<RiskCalculationService> logger,
            IEventService eventService)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
            _eventService = eventService;
        }

        public async Task<CalculationBreakdownModel> GetCalculationBreakdown(int eventId, int? geonameId)
        {
            var eventConfigBuilder = new EventConfig.Builder()
                .SetEventId(eventId)
                .ShouldIncludeLocations()
                .ShouldIncludeCalculationMetadata()
                .ShouldIncludeDiseaseInformation()
                .ShouldIncludeSourceAirports(new AirportConfig.Builder(eventId).ShouldIncludeExportationRisk().Build())
                .ShouldIncludeDestinationAirports(new AirportConfig.Builder(eventId).ShouldIncludeImportationRisk(geonameId).Build());

            var eventConfig = eventConfigBuilder.Build();
            var eventModel = await _eventService.GetEvent(eventConfig);

            return new CalculationBreakdownModel
            {
                CalculationCases = eventModel.CalculationMetadata,
                DiseaseInformation = eventModel.DiseaseInformation,
                TopSourceAirports = eventModel.SourceAirports
                    .OrderBy(a => a.ExportationRisk?.IsModelNotRun)
                    .ThenByDescending(a => a.ExportationRisk?.MaxProbability)
                    .ThenByDescending(a => a.ExportationRisk?.MaxMagnitude)
                    .Take(CalculationBreakdownModel.TopAirportCount),
                TotalSourceAirports = eventModel.SourceAirports.Count(),
                TotalSourceVolume = eventModel.SourceAirports.Sum(a => a.Volume),
                TopDestinationAirports = eventConfig.IncludeDestinationAirports
                    ? eventModel.DestinationAirports
                        .OrderBy(a => a.ExportationRisk?.IsModelNotRun)
                        .ThenByDescending(a => a.ImportationRisk?.MaxProbability)
                        .ThenByDescending(a => a.ImportationRisk?.MaxMagnitude)
                        .Take(CalculationBreakdownModel.TopAirportCount)
                    : null,
                TotalDestinationAirports = eventConfig.IncludeDestinationAirports ? eventModel.DestinationAirports.Count() : 0,
                TotalDestinationVolume = eventConfig.IncludeDestinationAirports ? eventModel.DestinationAirports.Sum(a => a.Volume) : 0
            };
        }

        public async Task PreCalculateImportationRisk(int geonameId)
        {
            var hasPreCalculation = await HasPreCalculatedImportationRisk(geonameId);
            if (!hasPreCalculation)
            {
                await ExecuteImportationRiskCalculation(geonameId);
            }
        }

        public async Task PreCalculateImportationRisk(ICollection<int> geonameIds)
        {
            var uncalculatedGeonameIds = await GetUncalculatedImportationRisk(geonameIds);
            if (uncalculatedGeonameIds.Any())
            {
                foreach (var geonameId in uncalculatedGeonameIds)
                {
                    await ExecuteImportationRiskCalculation(geonameId);
                }
            }
        }

        private async Task<bool> HasPreCalculatedImportationRisk(int geonameId)
        {
            return await _biodZebraContext.EventImportationRisksByGeonameSpreadMd.AnyAsync(g => g.GeonameId == geonameId);
        }

        private async Task<ICollection<int>> GetUncalculatedImportationRisk(ICollection<int> geonameIds)
        {
            return new HashSet<int>(geonameIds.Except(
                await _biodZebraContext.EventImportationRisksByGeonameSpreadMd
                    .Select(g => g.GeonameId)
                    .Where(gid => geonameIds.Contains(gid))
                    .ToListAsync()));
        }

        private async Task ExecuteImportationRiskCalculation(int geonameId)
        {
            var resultCode = await SqlQuery.ExecuteImportationRiskCalculation(_biodZebraContext, geonameId);
            _logger.LogInformation($"Ran pre-calculation of importation risk on geonameId {geonameId}: Received result status code: {resultCode}");
        }
    }
}