using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Api.Models;

namespace Biod.Insights.Api.Interface
{
    public interface IDiseaseRiskService
    {
        Task<IEnumerable<GetDiseaseRiskModel>> GetDiseaseRiskForLocation(int geonameId);
    }
}