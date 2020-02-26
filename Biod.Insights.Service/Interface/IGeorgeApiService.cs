using Biod.Insights.Service.Models.Risk;
using System.Threading.Tasks;

namespace Biod.Insights.Service.Interface
{
    public interface IGeorgeApiService
    {
        /// <summary>
        /// Gets the location risks by latitude and longitude, used for locations that are a single point
        /// </summary>
        /// <param name="latitude">the latitude value</param>
        /// <param name="longitude">the longitude value</param>
        /// <exception cref="T:System.Net.Http.HttpRequestException">The HTTP response is unsuccessful.</exception>
        /// <returns>the deserialized George response</returns>
        Task<GeorgeRiskClass> GetLocationRisk(float latitude, float longitude);

        /// <summary>
        /// Gets the location risks by geoname id, used for locations that are not single points
        /// </summary>
        /// <param name="geonameId">the geoname id</param>
        /// <exception cref="T:System.Net.Http.HttpRequestException">The HTTP response is unsuccessful.</exception>
        /// <returns>the deserialized George response</returns>
        Task<GeorgeRiskClass> GetLocationRisk(int geonameId);
    }
}