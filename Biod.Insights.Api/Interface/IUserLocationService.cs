using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Api.Models.Geoname;

namespace Biod.Insights.Api.Interface
{ //TODO move all interfaces to a common project
    public interface IUserLocationService
    {
        /// <summary>
        /// Gets the user's AOI geoname ids
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <returns>list of geoname IDs for the user's AOI</returns>
        Task<IEnumerable<GetGeonameModel>> GetAoi(string userId);

        /// <summary>
        /// Adds a geoname id to a user's AOI
        /// </summary>
        /// <param name="userId">the id of the user</param>
        /// <param name="geonameId">the geoname id to add</param>
        /// <returns>list of geoname IDs for the user's AOI after adding</returns>
        Task<IEnumerable<GetGeonameModel>> AddAoi(string userId, int geonameId);

        /// <summary>
        /// Removes a geoname id from a user's AOI
        /// </summary>
        /// <param name="userId">the id of the user</param>
        /// <param name="geonameId">the geoname id to add</param>
        /// <returns>list of geoname IDs for the user's AOI after removing</returns>
        Task<IEnumerable<GetGeonameModel>> RemoveAoi(string userId, int geonameId);

        Task<IEnumerable<GetGeonameModel>> SetAois(AspNetUsers user, ICollection<int> geonameIds);
    }
}