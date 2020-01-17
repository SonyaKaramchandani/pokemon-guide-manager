using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Api.Models;
using Biod.Insights.Api.Models.Disease;

namespace Biod.Insights.Api.Interface
{
    public interface IDiseaseRiskService
    {
        Task<RiskAggregationModel> GetDiseaseRiskForLocation(int? geonameId);

        Task<RiskAggregationModel> GetDiseaseRiskForLocation(int? geonameId, IEnumerable<int> diseaseIds);

        Task<RiskAggregationModel> GetDiseaseRiskForLocation(int? geonameId, DiseaseRelevanceSettingsModel relevanceSettings);
    }
}