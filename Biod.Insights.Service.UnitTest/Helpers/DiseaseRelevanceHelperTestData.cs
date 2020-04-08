using System.Collections.Generic;
using Biod.Insights.Service.Helpers;
using Biod.Insights.Service.Models;
using Biod.Insights.Service.Models.Disease;

namespace Biod.Insights.Service.UnitTest.Helpers
{
    public sealed class DiseaseRelevanceHelperTestData
    {
        private static readonly DiseaseRelevanceSettingsModel AlwaysSettings = new DiseaseRelevanceSettingsModel
        {
            AlwaysNotifyDiseaseIds = new HashSet<int> {1, 2, 3, 4, 5, 6},
            RiskOnlyDiseaseIds = new HashSet<int>(),
            NeverNotifyDiseaseIds = new HashSet<int>()
        };

        private static readonly DiseaseRelevanceSettingsModel RiskOnlySettings = new DiseaseRelevanceSettingsModel
        {
            AlwaysNotifyDiseaseIds = new HashSet<int>(),
            RiskOnlyDiseaseIds = new HashSet<int> {1, 2, 3, 4, 5, 6},
            NeverNotifyDiseaseIds = new HashSet<int>()
        };

        private static readonly DiseaseRelevanceSettingsModel NeverSettings = new DiseaseRelevanceSettingsModel
        {
            AlwaysNotifyDiseaseIds = new HashSet<int>(),
            RiskOnlyDiseaseIds = new HashSet<int>(),
            NeverNotifyDiseaseIds = new HashSet<int> {1, 2, 3, 4, 5, 6}
        };

        private static readonly DiseaseRiskModel NullRisk = new DiseaseRiskModel // Importation Risk not computed
        {
            DiseaseInformation = new DiseaseInformationModel {Id = 1},
            HasLocalEvents = false,
            ImportationRisk = null
        };

        private static readonly DiseaseRiskModel NoLocalEvents = new DiseaseRiskModel // No local events and no risk
        {
            DiseaseInformation = new DiseaseInformationModel {Id = 2},
            HasLocalEvents = false,
            ImportationRisk = new RiskModel()
        };

        private static readonly DiseaseRiskModel HasLocalEvents = new DiseaseRiskModel // Local events and no risk
        {
            DiseaseInformation = new DiseaseInformationModel {Id = 3},
            HasLocalEvents = true,
            ImportationRisk = new RiskModel()
        };

        private static readonly DiseaseRiskModel RiskLessThanThreshold = new DiseaseRiskModel // Importation Risk less than threshold
        {
            DiseaseInformation = new DiseaseInformationModel {Id = 4},
            HasLocalEvents = false,
            ImportationRisk = new RiskModel {MaxProbability = DiseaseRelevanceHelper.THRESHOLD - 0.001f}
        };

        private static readonly DiseaseRiskModel RiskEqualThreshold = new DiseaseRiskModel // Importation Risk equal to threshold
        {
            DiseaseInformation = new DiseaseInformationModel {Id = 5},
            HasLocalEvents = false,
            ImportationRisk = new RiskModel {MaxProbability = DiseaseRelevanceHelper.THRESHOLD}
        };

        private static readonly DiseaseRiskModel RiskGreaterThanThreshold = new DiseaseRiskModel // Importation Risk greater than threshold
        {
            DiseaseInformation = new DiseaseInformationModel {Id = 6},
            HasLocalEvents = false,
            ImportationRisk = new RiskModel {MaxProbability = DiseaseRelevanceHelper.THRESHOLD + 0.001f}
        };

        /// <summary>
        /// Set of data that will yield no disease risk models after filtering
        /// </summary>
        public static IEnumerable<object[]> EmptyResults = new[]
        {
            // List to filter is already empty
            new object[]
            {
                new DiseaseRiskModel[0],
                AlwaysSettings
            },

            // All disease risk models in list are not of interest in settings
            new object[]
            {
                new[] {NullRisk, NoLocalEvents, HasLocalEvents, RiskLessThanThreshold, RiskEqualThreshold, RiskGreaterThanThreshold},
                NeverSettings
            },

            // All disease risk models in list are risk only in settings, but does not meet risk only criteria
            new object[]
            {
                new[] {NoLocalEvents, RiskLessThanThreshold},
                RiskOnlySettings
            }
        };

        /// <summary>
        /// Set of data that will yield the expected results after filtering
        /// </summary>
        public static IEnumerable<object[]> NonEmptyResults = new[]
        {
            // All disease risk models in list are of interest in settings
            new object[]
            {
                new[] {NullRisk, NoLocalEvents, HasLocalEvents, RiskLessThanThreshold, RiskEqualThreshold, RiskGreaterThanThreshold},
                AlwaysSettings,
                new[] {1, 2, 3, 4, 5, 6}
            },

            // All disease risk models in list are risk only in settings, but meets a risk only criteria
            new object[]
            {
                new[] {NullRisk, HasLocalEvents, RiskEqualThreshold, RiskGreaterThanThreshold},
                RiskOnlySettings,
                new[] {1, 3, 5, 6}
            }
        };
    }
}