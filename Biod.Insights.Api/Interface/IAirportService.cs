using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Api.Models.Airport;
using Biod.Insights.Api.Models.Geoname;

namespace Biod.Insights.Api.Interface
{
    public interface IAirportService
    {
        Task<IEnumerable<GetAirportModel>> GetSourceAirports(int eventId);

        Task<IEnumerable<GetAirportModel>> GetDestinationAirports(int eventId, GetGeonameModel geoname);
    }
}