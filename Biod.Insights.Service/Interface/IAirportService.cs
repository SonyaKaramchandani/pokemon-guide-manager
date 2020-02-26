using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Biod.Insights.Service.Models.Airport;
using Biod.Insights.Service.Models.Geoname;

namespace Biod.Insights.Service.Interface
{
    public interface IAirportService
    {
        Task<IEnumerable<GetAirportModel>> GetSourceAirports(int eventId);

        Task<IEnumerable<GetAirportModel>> GetDestinationAirports(int eventId);
    }
}