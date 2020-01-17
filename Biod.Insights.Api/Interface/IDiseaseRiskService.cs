using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Api.Models.Disease;

namespace Biod.Insights.Api.Interface
{
    public interface IDiseaseRiskService
    {
        Task<RiskAggregationModel> GetDiseaseRiskForLocation(int? geonameId);
    }
}