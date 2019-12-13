using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Api.Models;
using Biod.Insights.Api.Models.Disease;

namespace Biod.Insights.Api.Interface
{
    public interface IDiseaseRiskService
    {
        Task<IEnumerable<GetDiseaseRiskModel>> GetDiseaseRiskForLocation(int geonameId);
    }
}