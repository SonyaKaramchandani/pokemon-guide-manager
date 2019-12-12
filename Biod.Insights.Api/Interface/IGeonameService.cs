using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Api.Models;

namespace Biod.Insights.Api.Interface
{
    public interface IGeonameService
    {
        /// <summary>
        /// Gets the geoname properties given a geoname id
        /// </summary>
        /// <param name="geonameId">the geoname id</param>
        /// <returns>the geoname object with all properties</returns>
        Task<GetGeonameModel> GetGeoname(int geonameId);

        /// <summary>
        /// Gets the geoname properties given a list of geoname ids
        /// </summary>
        /// <param name="geonameIds">the geoname ids</param>
        /// <returns>the list of geoname object with all properties</returns>
        Task<IEnumerable<GetGeonameModel>> GetGeonames(IEnumerable<int> geonameIds);

        /// <summary>
        /// Searches the geonames using the provided search term.
        /// </summary>
        /// <param name="searchTerm">the search term</param>
        /// <returns>the list of geonames that match the term</returns>
        Task<IEnumerable<SearchGeonameModel>> SearchGeonamesByTerm(string searchTerm);
    }
}