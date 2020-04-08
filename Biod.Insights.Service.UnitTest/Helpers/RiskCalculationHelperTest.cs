using System.Collections.Generic;
using Biod.Insights.Service.Data.CustomModels;
using Biod.Insights.Service.Helpers;
using Xunit;

namespace Biod.Insights.Service.UnitTest.Helpers
{
    public class RiskCalculationHelperTest
    {
        #region CalculateImportationRisk

        [Theory]
        [MemberData(nameof(RiskCalculationHelperTestData.NoModelsRun), MemberType = typeof(RiskCalculationHelperTestData))]
        public void CalculateImportationRisk_NoModelsRun(List<EventJoinResult> eventsList)
        {
            var result = RiskCalculationHelper.CalculateImportationRisk(eventsList);
            Assert.True(result.IsModelNotRun);
            Assert.Equal(0, result.MinMagnitude);
            Assert.Equal(0, result.MaxMagnitude);
            Assert.Equal(0, result.MinProbability);
            Assert.Equal(0, result.MaxProbability);
        }

        [Theory]
        [MemberData(nameof(RiskCalculationHelperTestData.ImportationRiskModels), MemberType = typeof(RiskCalculationHelperTestData))]
        public void CalculateImportationRisk_MinProbability(
            List<EventJoinResult> eventsList,
            float expectedMinMagnitude,
            float expectedMaxMagnitude,
            float expectedMinProbability,
            float expectedMaxProbability)
        {
            var result = RiskCalculationHelper.CalculateImportationRisk(eventsList);
            Assert.False(result.IsModelNotRun);
            Assert.Equal(expectedMinMagnitude, result.MinMagnitude, 4); // SQL column is decimal(5, 4)
            Assert.Equal(expectedMaxMagnitude, result.MaxMagnitude, 4); // SQL column is decimal(5, 4)
            Assert.Equal(expectedMinProbability, result.MinProbability, 3); // SQL column is decimal(10, 3)
            Assert.Equal(expectedMaxProbability, result.MaxProbability, 3); // SQL column is decimal(10, 3)
        }

        #endregion

        #region CalculateExportationRisk

        [Theory]
        [MemberData(nameof(RiskCalculationHelperTestData.NoModelsRun), MemberType = typeof(RiskCalculationHelperTestData))]
        public void CalculateExportationRisk_NoModelsRun(List<EventJoinResult> eventsList)
        {
            var result = RiskCalculationHelper.CalculateExportationRisk(eventsList);
            Assert.True(result.IsModelNotRun);
            Assert.Equal(0, result.MinMagnitude);
            Assert.Equal(0, result.MaxMagnitude);
            Assert.Equal(0, result.MinProbability);
            Assert.Equal(0, result.MaxProbability);
        }

        [Theory]
        [MemberData(nameof(RiskCalculationHelperTestData.ExportationRiskModels), MemberType = typeof(RiskCalculationHelperTestData))]
        public void CalculateExportationRisk_MinProbability(
            List<EventJoinResult> eventsList,
            float expectedMinMagnitude,
            float expectedMaxMagnitude,
            float expectedMinProbability,
            float expectedMaxProbability)
        {
            var result = RiskCalculationHelper.CalculateExportationRisk(eventsList);
            Assert.False(result.IsModelNotRun);
            Assert.Equal(expectedMinMagnitude, result.MinMagnitude, 4); // SQL column is decimal(5, 4)
            Assert.Equal(expectedMaxMagnitude, result.MaxMagnitude, 4); // SQL column is decimal(5, 4)
            Assert.Equal(expectedMinProbability, result.MinProbability, 3); // SQL column is decimal(10, 3)
            Assert.Equal(expectedMaxProbability, result.MaxProbability, 3); // SQL column is decimal(10, 3)
        }

        #endregion

        #region GetAggregatedRiskOfAnyEvent

        [Fact]
        public void GetAggregatedRiskOfAnyEvent_SingleZeroRisk()
        {
            var risk = RiskCalculationHelper.GetAggregatedRiskOfAnyEvent(new[] {0f});
            Assert.Equal(0f, risk);
        }

        [Fact]
        public void GetAggregatedRiskOfAnyEvent_SingleHundredPercentRisk()
        {
            var risk = RiskCalculationHelper.GetAggregatedRiskOfAnyEvent(new[] {1f});
            Assert.Equal(1f, risk);
        }

        [Fact]
        public void GetAggregatedRiskOfAnyEvent_SingleFiftyPercentRisk()
        {
            var risk = RiskCalculationHelper.GetAggregatedRiskOfAnyEvent(new[] {0.5f});
            Assert.Equal(0.5f, risk);
        }

        [Fact]
        public void GetAggregatedRiskOfAnyEvent_MultipleRiskWithHundredPercent()
        {
            var risk = RiskCalculationHelper.GetAggregatedRiskOfAnyEvent(new[] {0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 1f});
            Assert.Equal(1f, risk);
        }

        [Fact]
        public void GetAggregatedRiskOfAnyEvent_MultipleRiskWithZeroRisk()
        {
            var risk = RiskCalculationHelper.GetAggregatedRiskOfAnyEvent(new[] {0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0f});
            Assert.Equal(0.8488f, risk);
        }

        #endregion
    }
}