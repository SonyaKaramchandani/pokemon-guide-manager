using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Data.CustomModels;
using Biod.Insights.Service.Models.Airport;

namespace Biod.Insights.Service.Interface
{
    public interface IAirportService
    {
        /// <summary>
        /// Gets the source airports for a given event
        /// </summary>
        /// <param name="config">the configuration on which event and properties to load</param>
        Task<IEnumerable<GetAirportModel>> GetSourceAirports(AirportConfig config);

        /// <summary>
        /// Gets the destination airports for a given event
        /// </summary>
        /// <param name="config">the configuration on which event and properties to load</param>
        Task<IEnumerable<GetAirportModel>> GetDestinationAirports(AirportConfig config);
    }
}