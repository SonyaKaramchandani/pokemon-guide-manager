using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Api.Models.Disease;

namespace Biod.Insights.Api.Interface
{
    public interface IOutbreakPotentialService
    {
        Task<IEnumerable<OutbreakPotentialCategoryModel>> GetOutbreakPotentialByPoint(float longitude, float latitude);
        
        Task<IEnumerable<OutbreakPotentialCategoryModel>> GetOutbreakPotentialByGeonameId(int geonameId);
    }
}