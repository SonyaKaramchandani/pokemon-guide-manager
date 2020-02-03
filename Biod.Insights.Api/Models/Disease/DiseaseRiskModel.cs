using System;

namespace Biod.Insights.Api.Models.Disease
{
    public class DiseaseRiskModel
    {
        public DiseaseInformationModel DiseaseInformation { get; set; }

        public RiskModel ImportationRisk { get; set; }

        public RiskModel ExportationRisk { get; set; }

        public DateTime LastUpdatedEventDate { get; set; }

        public OutbreakPotentialCategoryModel OutbreakPotentialCategory { get; set; }
        
        public CaseCountModel CaseCounts { get; set; }
        
        public bool HasLocalEvents { get; set; }
    }
}