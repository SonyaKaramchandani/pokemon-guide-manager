using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Service.Models.Disease;
using Biod.Insights.Service.Models.Geoname;

namespace Biod.Insights.Service.Interface
{
    public interface IOutbreakPotentialService
    {
        Task<IEnumerable<OutbreakPotentialCategoryModel>> GetOutbreakPotentialByGeonameId(int geonameId);

        Task<IEnumerable<OutbreakPotentialCategoryModel>> GetOutbreakPotentialByGeoname(GetGeonameModel geoname);

        Task<OutbreakPotentialCategoryModel> GetOutbreakPotentialByGeonameId(int diseaseId, int geonameId);

        Task<OutbreakPotentialCategoryModel> GetOutbreakPotentialByGeoname(int diseaseId, GetGeonameModel geoname);

        Task<OutbreakPotentialCategoryModel> GetOutbreakPotentialByGeoname(DiseaseInformationModel diseaseInformationModel, GetGeonameModel geoname);
    }
}