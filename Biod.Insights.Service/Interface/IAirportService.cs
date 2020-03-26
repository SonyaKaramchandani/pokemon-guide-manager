using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Models.Airport;
using Biod.Insights.Service.Models.Geoname;

namespace Biod.Insights.Service.Interface
{
    public interface IAirportService
    {
        Task<IEnumerable<GetAirportModel>> GetSourceAirports(SourceAirportConfig config);

        Task<IEnumerable<GetAirportModel>> GetDestinationAirports(DestinationAirportConfig config);
    }
}