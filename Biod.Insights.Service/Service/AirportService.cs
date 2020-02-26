using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Data.QueryBuilders;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models;
using Biod.Insights.Service.Models.Airport;
using Biod.Insights.Common.Exceptions;
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

        public async Task<IEnumerable<GetAirportModel>> GetDestinationAirports(int eventId)
        {
            var @event = (await new EventQueryBuilder(_biodZebraContext)
                    .SetEventId(eventId)
                    .IncludeLocations()
                    .BuildAndExecute())
                .FirstOrDefault();

            if (@event == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested event with id {eventId} does not exist");
            }

            var result = (await new DestinationAirportQueryBuilder(_biodZebraContext)
                    .SetEventId(eventId)
                    .BuildAndExecute())
                .ToList();

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
                    ImportationRisk = new RiskModel
                    {
                        IsModelNotRun = @event.IsModelNotRun,
                        MinProbability = a.MinProb,
                        MaxProbability = a.MaxProb,
                        MinMagnitude = a.MinExpVolume,
                        MaxMagnitude = a.MaxExpVolume
                    }
                })
                .OrderByDescending(a => a.ImportationRisk?.MaxProbability)
                .ThenByDescending(a => a.ImportationRisk?.MaxMagnitude)
                .ThenBy(a => a.Name);
        }
    }
}