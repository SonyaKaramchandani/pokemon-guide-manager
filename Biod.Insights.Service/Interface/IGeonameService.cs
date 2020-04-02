using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Models;
using Biod.Insights.Service.Models.Geoname;

namespace Biod.Insights.Service.Interface
{
    public interface IGeonameService
    {
        /// <summary>
        /// Gets the geoname properties given a geoname id
        /// </summary>
        /// <param name="config">parameter configuration for data to be included in query</param>
        /// <returns>the geoname object with the specified properties</returns>
        Task<GetGeonameModel> GetGeoname(GeonameConfig config);

        /// <summary>
        /// Gets the geoname properties given a list of geoname ids
        /// </summary>
        /// <param name="config">parameter configuration for data to be included in query</param>
        /// <returns>the list of geoname object with specified properties</returns>
        Task<IEnumerable<GetGeonameModel>> GetGeonames(GeonameConfig config);

        /// <summary>
        /// Searches the geonames using the provided search term.
        /// </summary>
        /// <param name="searchTerm">the search term</param>
        /// <returns>the list of geonames that match the term</returns>
        Task<IEnumerable<SearchGeonameModel>> SearchGeonamesByTerm(string searchTerm);
        
        /// <summary>
        /// Searches the city geonames using the provided search term.
        /// </summary>
        /// <param name="searchTerm">the search term</param>
        /// <returns>the list of geonames that match the term</returns>
        Task<IEnumerable<SearchGeonameModel>> SearchCitiesByTerm(string searchTerm);

        /// <summary>
        /// Gets the Grid-Id that contains the provided geoname id
        /// </summary>
        /// <param name="geonameId">the geoname id</param>
        /// <returns>the grid id</returns>
        IEnumerable<string> GetGridIdsByGeonameId(int geonameId);
    }
}