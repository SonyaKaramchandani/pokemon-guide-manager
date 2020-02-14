using Biod.Zebra.Controllers.api;
using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Models.FilterEventResult;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Biod.Solution.UnitTest.Zebra.Disease.GetAggregatedRiskTest
{
    /// <summary>
    /// Tests the API controller for getting the Aggregated Risk for diseases
    /// </summary>
    [TestClass]
    public class GetAggregatedCaseCountTest
    {
        private DiseaseController controller;
        private Mock<BiodZebraEntities> mockDbContext;
        private GetAggregatedCaseCountMockDbSet dbMock;

        [TestInitialize()]
        public void Initialize()
        {
            dbMock = new GetAggregatedCaseCountMockDbSet();
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
            HttpResponseMessage result = controller.GetAggregatedCaseCount(GetAggregatedCaseCountMockDbSet.NULL_RESULT_DISEASE_ID, "1,2,3", GetAggregatedCaseCountMockDbSet.NULL_RESULT_EVENT_ID);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode, "Successful request not returning 200 OK");
        }

        /// <summary>
        /// Checks whether null values returned by the SP is defaulting to the correct values
        /// </summary>
        [TestMethod]
        public async Task Test_NullValues()
        {
            var result = controller.GetAggregatedCaseCount(GetAggregatedCaseCountMockDbSet.NULL_RESULT_DISEASE_ID, "1,2,3", GetAggregatedCaseCountMockDbSet.NULL_RESULT_EVENT_ID);
            var bodyContent = await result.Content.ReadAsStringAsync();

            var resultCaseCount = JsonConvert.DeserializeObject<DiseaseGroupResultViewModel>(bodyContent);
            Assert.AreEqual(0, resultCaseCount.TotalCases, "Null total cases not defaulting to 0");
            Assert.AreEqual("No cases reported in or near your locations", resultCaseCount.TotalCasesText, "Null total cases not showing correct message");
        }

        /// <summary>
        /// Checks whether zero values returned by the SP is defaulting to the correct values
        /// </summary>
        [TestMethod]
        public async Task Test_ZeroValues()
        {
            var result = controller.GetAggregatedCaseCount(GetAggregatedCaseCountMockDbSet.ZERO_RESULT_DISEASE_ID, "1,2,3", GetAggregatedCaseCountMockDbSet.NULL_RESULT_EVENT_ID);
            var bodyContent = await result.Content.ReadAsStringAsync();

            var resultCaseCount = JsonConvert.DeserializeObject<DiseaseGroupResultViewModel>(bodyContent);
            Assert.AreEqual(0, resultCaseCount.TotalCases, "Zero total cases not defaulting to 0");
            Assert.AreEqual("No cases reported in or near your locations", resultCaseCount.TotalCasesText, "Zero total cases not showing correct message");
        }

        /// <summary>
        /// Checks whether the numerical value is formatted property when the total case count is under 1000 
        /// </summary>
        [TestMethod]
        public async Task Test_SmallTotalCaseCount()
        {
            var result = controller.GetAggregatedCaseCount(GetAggregatedCaseCountMockDbSet.TOTAL_CASE_SMALL_RESULT_DISEASE_ID, "1,2,3", GetAggregatedCaseCountMockDbSet.NULL_RESULT_EVENT_ID);
            var bodyContent = await result.Content.ReadAsStringAsync();

            var resultCaseCount = JsonConvert.DeserializeObject<DiseaseGroupResultViewModel>(bodyContent);
            Assert.AreEqual(GetAggregatedCaseCountMockDbSet.TOTAL_CASE_SMALL, resultCaseCount.TotalCases, "Total cases not returning as expected");
            Assert.AreEqual("123", resultCaseCount.TotalCasesText, "Total cases under 1000 not showing correctly");
        }

        /// <summary>
        /// Checks whether the numerical value is formatted property when the total case count is 1000 or higher
        /// </summary>
        [TestMethod]
        public async Task Test_LargeTotalCaseCount()
        {
            var result = controller.GetAggregatedCaseCount(GetAggregatedCaseCountMockDbSet.TOTAL_CASE_LARGE_RESULT_DISEASE_ID, "1,2,3", GetAggregatedCaseCountMockDbSet.NULL_RESULT_EVENT_ID);
            var bodyContent = await result.Content.ReadAsStringAsync();

            var resultCaseCount = JsonConvert.DeserializeObject<DiseaseGroupResultViewModel>(bodyContent);
            Assert.AreEqual(GetAggregatedCaseCountMockDbSet.TOTAL_CASE_LARGE, resultCaseCount.TotalCases, "Total cases not returning as expected");
            Assert.AreEqual("1,234,567", resultCaseCount.TotalCasesText, "Total cases 1000 or higher not showing correctly");
        }
        
        /// <summary>
        /// Checks whether the numerical value is formatted property when the total case count is 1000 or higher
        /// </summary>
        [TestMethod]
        public async Task Test_IsVisible_ZeroCaseCount()
        {
            var result = controller.GetAggregatedCaseCount(GetAggregatedCaseCountMockDbSet.ZERO_RESULT_DISEASE_ID, "1,2,3", GetAggregatedCaseCountMockDbSet.NULL_RESULT_EVENT_ID);
            var bodyContent = await result.Content.ReadAsStringAsync();

            var resultCaseCount = JsonConvert.DeserializeObject<DiseaseGroupResultViewModel>(bodyContent);
            Assert.IsFalse(resultCaseCount.IsVisible, "Zero cases not returning false for visibility");
        }
        
        /// <summary>
        /// Checks whether the numerical value is formatted property when the total case count is 1000 or higher
        /// </summary>
        [TestMethod]
        public async Task Test_IsVisible_NonZeroCaseCount()
        {
            var result = controller.GetAggregatedCaseCount(GetAggregatedCaseCountMockDbSet.TOTAL_CASE_LARGE_RESULT_DISEASE_ID, "1,2,3", GetAggregatedCaseCountMockDbSet.NULL_RESULT_EVENT_ID);
            var bodyContent = await result.Content.ReadAsStringAsync();

            var resultCaseCount = JsonConvert.DeserializeObject<DiseaseGroupResultViewModel>(bodyContent);
            Assert.IsTrue(resultCaseCount.IsVisible, "Non-zero cases not returning true for visibility");
        }
    }
}
