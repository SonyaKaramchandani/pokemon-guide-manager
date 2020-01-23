using System.Collections.Generic;
using System.Linq;
using Biod.Insights.Api.Models;
using Biod.Insights.Api.Models.Disease;

namespace Biod.Insights.Api.Helpers
{
    public static class DiseaseRelevanceHelper
    {
        // TODO: Move to Constants or Configuration
        private const float THRESHOLD = 0.01f;

        public static IEnumerable<DiseaseRiskModel> FilterRelevantDiseases(IEnumerable<DiseaseRiskModel> diseaseRiskModels, DiseaseRelevanceSettingsModel relevanceSettings)
        {
            return diseaseRiskModels.Where(r =>
                relevanceSettings.AlwaysNotifyDiseaseIds.Contains(r.DiseaseInformation.Id)
                || (relevanceSettings.RiskOnlyDiseaseIds.Contains(r.DiseaseInformation.Id)
                    && (r.HasLocalEvents
                        || r.ImportationRisk == null
                        || r.ImportationRisk.MaxProbability > THRESHOLD
                    ))
            );
        }
    }
}