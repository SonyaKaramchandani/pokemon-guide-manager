using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Service.Models;
using Biod.Insights.Service.Models.Disease;

namespace Biod.Insights.Service.Interface
{
    public interface IDiseaseRiskService
    {
        Task<RiskAggregationModel> GetDiseaseRiskForLocation(int? geonameId);

        Task<RiskAggregationModel> GetDiseaseRiskForLocation(int? geonameId, IEnumerable<int> diseaseIds);

        Task<RiskAggregationModel> GetDiseaseRiskForLocation(int? geonameId, DiseaseRelevanceSettingsModel relevanceSettings);
    }
}