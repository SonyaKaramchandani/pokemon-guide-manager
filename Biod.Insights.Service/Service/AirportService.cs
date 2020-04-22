using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Data.QueryBuilders;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models;
using Biod.Insights.Service.Models.Airport;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Models.Event;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Service.Service
{
    public class AirportService : IAirportService
    {
        private readonly ILogger<AirportService> _logger;
        private readonly BiodZebraContext _biodZebraContext;

        /// <summary>
        /// Airport service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        public AirportService(BiodZebraContext biodZebraContext, ILogger<AirportService> logger)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
        }

        public async Task<IEnumerable<GetAirportModel>> GetSourceAirports(SourceAirportConfig config)
        {
            var result = (await new SourceAirportQueryBuilder(_biodZebraContext, config).BuildAndExecute()).ToList();

            return result
                .Select(a => new GetAirportModel
                {
                    Id = a.StationId,
                    Name = a.StationName,
                    Code = a.StationCode,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude,
                    Volume = a.Volume,
                    City = a.CityName,
                    MinPrevalence = a.MinPrevalence,
                    MaxPrevalence = a.MaxPrevalence,
                    ExportationRisk = config.IncludeExportationRisk
                        ? new RiskModel
                        {
                            IsModelNotRun = a.IsModelNotRun,
                            MinProbability = a.MinProb,
                            MaxProbability = a.MaxProb,
                            MinMagnitude = a.MinExpVolume,
                            MaxMagnitude = a.MaxExpVolume
                        }
                        : null,
                    Population = config.IncludePopulation ? a.Population : 0,
                    Cases = config.IncludeCaseCounts
                        ? new EventCalculationCasesModel
                        {
                            CasesIncluded = a.GridStationCases.Sum(c => (int) Math.Round(c.Cases * c.Probability)),
                            MinCasesIncluded = a.GridStationCases.Sum(c => (int) Math.Round(c.MinCases * c.Probability)),
                            MaxCasesIncluded = a.GridStationCases.Sum(c => (int) Math.Round(c.MaxCases * c.Probability))
                        }
                        : null
                })
                .OrderByDescending(a => (a.ExportationRisk?.MinProbability + a.ExportationRisk?.MaxProbability) / 2f)
                .ThenByDescending(a => (a.ExportationRisk?.MinMagnitude + a.ExportationRisk?.MaxMagnitude) / 2f)
                .ThenBy(a => a.Name);
        }

        public async Task<IEnumerable<GetAirportModel>> GetDestinationAirports(AirportConfig config)
        {
            var result = (await new DestinationAirportQueryBuilder(_biodZebraContext, config).BuildAndExecute()).ToList();

            return result
                .Select(a => new GetAirportModel
                {
                    Id = a.StationId,
                    Name = a.StationName,
                    Code = a.StationCode,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude,
                    Volume = a.Volume,
                    City = a.CityName,
                    ImportationRisk = config.IncludeImportationRisk
                        ? new RiskModel
                        {
                            IsModelNotRun = a.IsModelNotRun,
                            MinProbability = a.MinProb,
                            MaxProbability = a.MaxProb,
                            MinMagnitude = a.MinExpVolume,
                            MaxMagnitude = a.MaxExpVolume
                        }
                        : null
                })
                .OrderByDescending(a => (a.ImportationRisk?.MinProbability + a.ImportationRisk?.MaxProbability) / 2f)
                .ThenByDescending(a => (a.ImportationRisk?.MinMagnitude + a.ImportationRisk?.MaxMagnitude) / 2f)
                .ThenBy(a => a.Name);
        }
    }
}