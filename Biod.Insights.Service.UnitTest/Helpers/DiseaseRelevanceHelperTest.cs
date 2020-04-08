using System.Collections.Generic;
using System.Linq;
using Biod.Insights.Service.Helpers;
using Biod.Insights.Service.Models;
using Biod.Insights.Service.Models.Disease;
using Xunit;

namespace Biod.Insights.Service.UnitTest.Helpers
{
    public class DiseaseRelevanceHelperTest
    {
        #region FilterRelevantDiseases

        [Theory]
        [MemberData(nameof(DiseaseRelevanceHelperTestData.EmptyResults), MemberType = typeof(DiseaseRelevanceHelperTestData))]
        public void FilterRelevantDiseases_Empty(IEnumerable<DiseaseRiskModel> diseaseRiskModels, DiseaseRelevanceSettingsModel relevanceSettings)
        {
            var result = DiseaseRelevanceHelper.FilterRelevantDiseases(diseaseRiskModels, relevanceSettings);
            Assert.Empty(result);
        }

        [Theory]
        [MemberData(nameof(DiseaseRelevanceHelperTestData.NonEmptyResults), MemberType = typeof(DiseaseRelevanceHelperTestData))]
        public void FilterRelevantDiseases_NonEmpty(IEnumerable<DiseaseRiskModel> diseaseRiskModels, DiseaseRelevanceSettingsModel relevanceSettings, int[] expectedDiseaseIds)
        {
            var result = DiseaseRelevanceHelper.FilterRelevantDiseases(diseaseRiskModels, relevanceSettings).ToList();
            Assert.Equal(expectedDiseaseIds.Length, result.Count);
            Assert.All(result, m => Assert.Contains(m.DiseaseInformation.Id, expectedDiseaseIds));
        }

        #endregion
    }
}