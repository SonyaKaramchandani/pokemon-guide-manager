using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Api.Data.EntityModels;
using Biod.Insights.Api.Data.QueryBuilders;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models;
using Biod.Insights.Api.Models.Airport;
using Biod.Insights.Api.Models.Geoname;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Service
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

        public async Task<IEnumerable<GetAirportModel>> GetSourceAirports(int eventId)
        {
            var result = (await new SourceAirportQueryBuilder(_biodZebraContext)
                .SetEventId(eventId)
                .IncludeAll()
                .BuildAndExecute())
                .ToList();

            return result
                .Select(a => new GetAirportModel
                {
                    Id = a.SourceAirport.SourceStationId,
                    Name = a.SourceAirport.StationName,
                    Code = a.SourceAirport.StationCode,
                    Latitude = (float) (a.SourceAirport.SourceStation.Latitude ?? 0),
                    Longitude = (float) (a.SourceAirport.SourceStation.Longitude ?? 0),
                    Volume = a.SourceAirport.Volume ?? 0,
                    City = a.City?.DisplayName
                })
                .OrderByDescending(a => a.Volume)
                .ThenBy(a => a.Name);
        }

        public async Task<IEnumerable<GetAirportModel>> GetDestinationAirports(int eventId, [AllowNull] GetGeonameModel geoname)
        {
            var query = new DestinationAirportQueryBuilder(_biodZebraContext)
                .SetEventId(eventId)
                .IncludeAll();

            if (geoname != null)
            {
                query.SetGeoname(geoname);
            }
            
            var result = (await query.BuildAndExecute()).ToList();

            return result
                .Select(a => new GetAirportModel
                {
                    Id = a.DestinationAirport.DestinationStationId,
                    Name = a.DestinationAirport.StationName,
                    Code = a.DestinationAirport.StationCode,
                    Latitude = (float) (a.DestinationAirport.Latitude ?? 0),
                    Longitude = (float) (a.DestinationAirport.Longitude ?? 0),
                    Volume = a.DestinationAirport.Volume ?? 0,
                    City = a.City?.DisplayName,
                    ImportationRisk = geoname != null ? new RiskModel
                    {
                        IsModelNotRun = a.DestinationAirport.Event.IsLocalOnly,
                        MinProbability = (float) (a.DestinationAirport.MinProb ?? 0),
                        MaxProbability = (float) (a.DestinationAirport.MaxProb ?? 0),
                        MinMagnitude = (float) (a.DestinationAirport.MinExpVolume ?? 0),
                        MaxMagnitude = (float) (a.DestinationAirport.MaxExpVolume ?? 0),
                    } : null
                })
                .OrderByDescending(a => a.ImportationRisk?.MaxProbability)
                .ThenByDescending(a => a.ImportationRisk?.MaxMagnitude)
                .ThenBy(a => a.Name);
        }
    }
}