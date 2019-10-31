using Biod.Zebra.Library.Infrastructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Biod.Solution.UnitTest.Infrastructures
{
    [TestClass]
    public class RiskProbabilityHelperTest
    {
        #region GetProbabilityName

        [TestMethod]
        public void TestGetProbabilityName_Null()
        {
            var probabilityName = RiskProbabilityHelper.GetProbabilityName(null);
            Assert.AreEqual("NotAvailable", probabilityName, "Null probability not returning expected probability name");
        }
        
        [TestMethod]
        public void TestGetProbabilityName_NegativeProbability()
        {
            var probabilityName = RiskProbabilityHelper.GetProbabilityName(-1);
            Assert.AreEqual("NotAvailable", probabilityName, "Negative probability not returning expected probability name");
        }
        
        [TestMethod]
        public void TestGetProbabilityName_ZeroProbability()
        {
            var probabilityName = RiskProbabilityHelper.GetProbabilityName(0);
            Assert.AreEqual("None", probabilityName, "Zero probability not returning expected probability name");
        }
        
        [TestMethod]
        public void TestGetProbabilityName_LessThanOnePercentProbability()
        {
            var probabilityName = RiskProbabilityHelper.GetProbabilityName(0.005m);
            Assert.AreEqual("None", probabilityName, "Probability less than 1% not returning expected probability name");
        }
        
        [TestMethod]
        public void TestGetProbabilityName_MinimumLowRiskProbability()
        {
            var probabilityName = RiskProbabilityHelper.GetProbabilityName(0.1m);
            Assert.AreEqual("Low", probabilityName, "1% Probability not returning expected probability name");
        }
        
        [TestMethod]
        public void TestGetProbabilityName_MaximumLowRiskProbability()
        {
            var probabilityName = RiskProbabilityHelper.GetProbabilityName(0.199m);
            Assert.AreEqual("Low", probabilityName, "19.9% Probability not returning expected probability name");
        }
        
        [TestMethod]
        public void TestGetProbabilityName_MinimumMedRiskProbability()
        {
            var probabilityName = RiskProbabilityHelper.GetProbabilityName(0.2m);
            Assert.AreEqual("Medium", probabilityName, "20% Probability not returning expected probability name");
        }
        
        [TestMethod]
        public void TestGetProbabilityName_MaximumMedRiskProbability()
        {
            var probabilityName = RiskProbabilityHelper.GetProbabilityName(0.7m);
            Assert.AreEqual("Medium", probabilityName, "70% Probability not returning expected probability name");
        }
        
        [TestMethod]
        public void TestGetProbabilityName_MinimumHighRiskProbability()
        {
            var probabilityName = RiskProbabilityHelper.GetProbabilityName(0.701m);
            Assert.AreEqual("High", probabilityName, "70.1% Probability not returning expected probability name");
        }
        
        [TestMethod]
        public void TestGetProbabilityName_MaximumHighRiskProbability()
        {
            var probabilityName = RiskProbabilityHelper.GetProbabilityName(0.99m);
            Assert.AreEqual("High", probabilityName, "99% Probability not returning expected probability name");
        }
        
        [TestMethod]
        public void TestGetProbabilityName_HundredPercentProbability()
        {
            var probabilityName = RiskProbabilityHelper.GetProbabilityName(1);
            Assert.AreEqual("High", probabilityName, "100% Probability not returning expected probability name");
        }

        #endregion

        #region GetRiskLevel

        [TestMethod]
        public void TestGetRiskLevel_Null()
        {
            var riskLevel = RiskProbabilityHelper.GetRiskLevel(null);
            Assert.AreEqual(-1, riskLevel, "Null probability not returning expected risk Level");
        }
        
        [TestMethod]
        public void TestGetRiskLevel_NegativeProbability()
        {
            var riskLevel = RiskProbabilityHelper.GetRiskLevel(-1);
            Assert.AreEqual(-1, riskLevel, "Negative probability not returning expected risk Level");
        }
        
        [TestMethod]
        public void TestGetRiskLevel_ZeroProbability()
        {
            var riskLevel = RiskProbabilityHelper.GetRiskLevel(0);
            Assert.AreEqual(0, riskLevel, "Zero probability not returning expected risk Level");
        }
        
        [TestMethod]
        public void TestGetRiskLevel_LessThanOnePercentProbability()
        {
            var riskLevel = RiskProbabilityHelper.GetRiskLevel(0.005m);
            Assert.AreEqual(0, riskLevel, "Probability less than 1% not returning expected risk Level");
        }
        
        [TestMethod]
        public void TestGetRiskLevel_MinimumLowRiskProbability()
        {
            var riskLevel = RiskProbabilityHelper.GetRiskLevel(0.1m);
            Assert.AreEqual(1, riskLevel, "1% Probability not returning expected risk Level");
        }
        
        [TestMethod]
        public void TestGetRiskLevel_MaximumLowRiskProbability()
        {
            var riskLevel = RiskProbabilityHelper.GetRiskLevel(0.199m);
            Assert.AreEqual(1, riskLevel, "19.9% Probability not returning expected risk Level");
        }
        
        [TestMethod]
        public void TestGetRiskLevel_MinimumMedRiskProbability()
        {
            var riskLevel = RiskProbabilityHelper.GetRiskLevel(0.2m);
            Assert.AreEqual(2, riskLevel, "20% Probability not returning expected risk Level");
        }
        
        [TestMethod]
        public void TestGetRiskLevel_MaximumMedRiskProbability()
        {
            var riskLevel = RiskProbabilityHelper.GetRiskLevel(0.7m);
            Assert.AreEqual(2, riskLevel, "70% Probability not returning expected risk Level");
        }
        
        [TestMethod]
        public void TestGetRiskLevel_MinimumHighRiskProbability()
        {
            var riskLevel = RiskProbabilityHelper.GetRiskLevel(0.701m);
            Assert.AreEqual(3, riskLevel, "70.1% Probability not returning expected risk Level");
        }
        
        [TestMethod]
        public void TestGetRiskLevel_MaximumHighRiskProbability()
        {
            var riskLevel = RiskProbabilityHelper.GetRiskLevel(0.99m);
            Assert.AreEqual(3, riskLevel, "99% Probability not returning expected risk Level");
        }
        
        [TestMethod]
        public void TestGetRiskLevel_HundredPercentProbability()
        {
            var riskLevel = RiskProbabilityHelper.GetRiskLevel(1);
            Assert.AreEqual(3, riskLevel, "100% Probability not returning expected risk Level");
        }

        #endregion

        #region GetAggregatedRiskOfAnyEvent

        [TestMethod]
        public void TestGetAggregatedRiskOfAnyEvent_SingleZeroRisk()
        {
            var risk = RiskProbabilityHelper.GetAggregatedRiskOfAnyEvent(new [] { 0m });
            Assert.AreEqual(0m, risk, "Single risk that is 0% not returning 0% risk");
        }

        [TestMethod]
        public void TestGetAggregatedRiskOfAnyEvent_SingleHundredPercentRisk()
        {
            var risk = RiskProbabilityHelper.GetAggregatedRiskOfAnyEvent(new [] { 1m });
            Assert.AreEqual(1m, risk, "Single risk that is 100% not returning 100% risk");
        }

        [TestMethod]
        public void TestGetAggregatedRiskOfAnyEvent_SingleFiftyPercentRisk()
        {
            var risk = RiskProbabilityHelper.GetAggregatedRiskOfAnyEvent(new [] { 0.5m });
            Assert.AreEqual(0.5m, risk, "Single risk that is not returning the same risk as the single event");
        }

        [TestMethod]
        public void TestGetAggregatedRiskOfAnyEvent_MultipleRiskWithHundredPercent()
        {
            var risk = RiskProbabilityHelper.GetAggregatedRiskOfAnyEvent(new [] { 0.1m, 0.2m, 0.3m, 0.4m, 0.5m, 1m });
            Assert.AreEqual(1m, risk, "Multiple risks with one being 100% risk, not returning 100% risk");
        }

        [TestMethod]
        public void TestGetAggregatedRiskOfAnyEvent_MultipleRiskWithZeroRisk()
        {
            var risk = RiskProbabilityHelper.GetAggregatedRiskOfAnyEvent(new [] { 0.1m, 0.2m, 0.3m, 0.4m, 0.5m, 0m });
            Assert.AreEqual(0.8488m, risk, "Multiple risks with one being 0% risk, not returning as expected");
        }

        #endregion
    }
}