using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Biod.Zebra.Controllers.api;
using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.Models.FilterEventResult;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace Biod.Solution.UnitTest.Zebra.Disease.GetAggregatedRiskTest
{
    /// <summary>
    /// Tests the API controller for getting the Aggregated Risk for diseases
    /// </summary>
    [TestClass]
    public class GetAggregatedRiskTest
    {
        private DiseaseController controller;
        private Mock<BiodZebraEntities> mockDbContext;
        private GetAggregatedRiskMockDbSet dbMock;

        [TestInitialize()]
        public void Initialize()
        {
            dbMock = new GetAggregatedRiskMockDbSet();
            mockDbContext = dbMock.MockContext;

            // Configure the controller request
            controller = new DiseaseController
            {
                Request = new HttpRequestMessage()
            };
            controller.Request.SetConfiguration(new HttpConfiguration());

            // Replace db context in controller
            controller.DbContext = mockDbContext.Object;
        }

        /// <summary>
        /// Checks whether the status code is correctly returning 200
        /// </summary>
        [TestMethod]
        public void Test_StatusCode_OK()
        {
            HttpResponseMessage result = controller.GetAggregatedRisk(GetAggregatedRiskMockDbSet.NULL_RESULT_DISEASE_ID, "1,2,3");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode, "Successful request not returning 200 OK");
        }

        /// <summary>
        /// Checks whether null values returned by the SP is defaulting to the correct values
        /// </summary>
        [TestMethod]
        public async Task Test_NullValues()
        {
            var result = controller.GetAggregatedRisk(GetAggregatedRiskMockDbSet.NULL_RESULT_DISEASE_ID, "1,2,3");
            var bodyContent = await result.Content.ReadAsStringAsync();

            var resultRisk = JsonConvert.DeserializeObject<DiseaseGroupResultViewModel>(bodyContent);
            Assert.AreEqual(0, resultRisk.TotalCases, "Null total cases not defaulting to 0");
            Assert.AreEqual(0, resultRisk.MinTravellers, "Null min travellers not defaulting to 0");
            Assert.AreEqual(0, resultRisk.MaxTravellers, "Null max travellers not defaulting to 0");
            Assert.AreEqual("No cases reported in your locations", resultRisk.TotalCasesText, "Null total cases not showing correct message");
            Assert.AreEqual("Negligible", resultRisk.TravellersText, "Null max travellers not showing correct message");
        }

        /// <summary>
        /// Checks whether zero values returned by the SP is defaulting to the correct values
        /// </summary>
        [TestMethod]
        public async Task Test_ZeroValues()
        {
            var result = controller.GetAggregatedRisk(GetAggregatedRiskMockDbSet.ZERO_RESULT_DISEASE_ID, "1,2,3");
            var bodyContent = await result.Content.ReadAsStringAsync();

            var resultRisk = JsonConvert.DeserializeObject<DiseaseGroupResultViewModel>(bodyContent);
            Assert.AreEqual(0, resultRisk.TotalCases, "Zero total cases not defaulting to 0");
            Assert.AreEqual(0, resultRisk.MinTravellers, "Zero min travellers not defaulting to 0");
            Assert.AreEqual(0, resultRisk.MaxTravellers, "Zero max travellers not defaulting to 0");
            Assert.AreEqual("No cases reported in your locations", resultRisk.TotalCasesText, "Zero total cases not showing correct message");
            Assert.AreEqual("Negligible", resultRisk.TravellersText, "Zero max travellers not showing correct message");
        }

        /// <summary>
        /// Checks whether the numerical value is formatted property when the total case count is under 1000 
        /// </summary>
        [TestMethod]
        public async Task Test_SmallTotalCaseCount()
        {
            var result = controller.GetAggregatedRisk(GetAggregatedRiskMockDbSet.TOTAL_CASE_SMALL_RESULT_DISEASE_ID, "1,2,3");
            var bodyContent = await result.Content.ReadAsStringAsync();

            var resultRisk = JsonConvert.DeserializeObject<DiseaseGroupResultViewModel>(bodyContent);
            Assert.AreEqual(GetAggregatedRiskMockDbSet.TOTAL_CASE_SMALL, resultRisk.TotalCases, "Total cases not returning as expected");
            Assert.AreEqual("123", resultRisk.TotalCasesText, "Total cases under 1000 not showing correctly");
        }

        /// <summary>
        /// Checks whether the numerical value is formatted property when the total case count is 1000 or higher
        /// </summary>
        [TestMethod]
        public async Task Test_LargeTotalCaseCount()
        {
            var result = controller.GetAggregatedRisk(GetAggregatedRiskMockDbSet.TOTAL_CASE_LARGE_RESULT_DISEASE_ID, "1,2,3");
            var bodyContent = await result.Content.ReadAsStringAsync();

            var resultRisk = JsonConvert.DeserializeObject<DiseaseGroupResultViewModel>(bodyContent);
            Assert.AreEqual(GetAggregatedRiskMockDbSet.TOTAL_CASE_LARGE, resultRisk.TotalCases, "Total cases not returning as expected");
            Assert.AreEqual("1,234,567", resultRisk.TotalCasesText, "Total cases 1000 or higher not showing correctly");
        }

        /// <summary>
        /// Checks whether the numerical value is formatted property when the total case count is 1000 or higher
        /// </summary>
        [TestMethod]
        public async Task Test_SmallTravellersRisk()
        {
            var result = controller.GetAggregatedRisk(GetAggregatedRiskMockDbSet.TRAVELLERS_SMALL_RESULT_DISEASE_ID, "1,2,3");
            var bodyContent = await result.Content.ReadAsStringAsync();

            var resultRisk = JsonConvert.DeserializeObject<DiseaseGroupResultViewModel>(bodyContent);
            Assert.AreEqual(GetAggregatedRiskMockDbSet.MIN_TRAVELLERS_SMALL, resultRisk.MinTravellers, "Min Travellers not returning as expected");
            Assert.AreEqual(GetAggregatedRiskMockDbSet.MAX_TRAVELLERS_SMALL, resultRisk.MaxTravellers, "Max Travellers not returning as expected");
            Assert.AreEqual("Negligible", resultRisk.TravellersText, "Travellers risk under 1% not showing correctly");
        }

        /// <summary>
        /// Checks whether the numerical value is formatted property when the total case count is 1000 or higher
        /// </summary>
        [TestMethod]
        public async Task Test_LargeTravellersRisk()
        {
            var result = controller.GetAggregatedRisk(GetAggregatedRiskMockDbSet.TRAVELLERS_LARGE_RESULT_DISEASE_ID, "1,2,3");
            var bodyContent = await result.Content.ReadAsStringAsync();

            var resultRisk = JsonConvert.DeserializeObject<DiseaseGroupResultViewModel>(bodyContent);
            Assert.AreEqual(GetAggregatedRiskMockDbSet.MIN_TRAVELLERS_LARGE, resultRisk.MinTravellers, "Min Travellers not returning as expected");
            Assert.AreEqual(GetAggregatedRiskMockDbSet.MAX_TRAVELLERS_LARGE, resultRisk.MaxTravellers, "Max Travellers not returning as expected");
            Assert.AreEqual(StringFormattingHelper.GetInterval(GetAggregatedRiskMockDbSet.MIN_TRAVELLERS_LARGE, GetAggregatedRiskMockDbSet.MAX_TRAVELLERS_LARGE), 
                resultRisk.TravellersText, 
                "Travellers risk 1% or higher not showing correctly");
        }
    }
}
