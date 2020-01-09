using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Api.Models.Disease;
using Biod.Insights.Api.Models.Geoname;

namespace Biod.Insights.Api.Interface
{
    public interface IOutbreakPotentialService
    {
        Task<IEnumerable<OutbreakPotentialCategoryModel>> GetOutbreakPotentialByGeonameId(int geonameId);

        Task<IEnumerable<OutbreakPotentialCategoryModel>> GetOutbreakPotentialByGeoname(GetGeonameModel geoname);
        
        Task<OutbreakPotentialCategoryModel> GetOutbreakPotentialByGeonameId(int diseaseId, int geonameId);

        Task<OutbreakPotentialCategoryModel> GetOutbreakPotentialByGeoname(int diseaseId, GetGeonameModel geoname);
    }
}