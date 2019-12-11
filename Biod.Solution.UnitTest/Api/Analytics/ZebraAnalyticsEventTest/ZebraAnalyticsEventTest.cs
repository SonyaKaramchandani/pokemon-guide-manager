using Biod.Zebra.Api.Api.Analytics;
using Biod.Zebra.Library.EntityModels.Zebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Biod.Solution.UnitTest.Api.Analytics
{
    /// <summary>
    /// Tests the API controller for getting the Event Details by Event ID
    /// </summary>
    [TestClass]
    public class ZebraAnalyticsEventTest
    {
        private ZebraAnalyticsEventController controller;
        private Mock<BiodZebraEntities> mockDbContext;
        private ZebraAnalyticsEventMockDbSet dbMock;

        [TestInitialize()]
        public void Initialize()
        {
            dbMock = new ZebraAnalyticsEventMockDbSet();
            mockDbContext = dbMock.MockContext;

            // Configure the controller request
            controller = new ZebraAnalyticsEventController
            {
                Request = new HttpRequestMessage()
            };
            controller.Request.SetConfiguration(new HttpConfiguration());

            // Replace db context in controller
            controller.DbContext = mockDbContext.Object;
        }

        /// <summary>
        /// Checks whether the status code is correctly returning 404
        /// </summary>
        [TestMethod]
        public void Test_StatusCode_NotFound()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEventDetailInfoById(-1);
            Assert.AreEqual(System.Net.HttpStatusCode.NotFound, result.StatusCode, "Non-existent event id request not returning 404 Not Found");
        }

        /// <summary>
        /// Checks whether the status code is correctly returning 200
        /// </summary>
        [TestMethod]
        public void Test_StatusCode_OK()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEventDetailInfoById(ZebraAnalyticsEventMockDbSet.EVENT_1_ID);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode, "Successful request not returning 200 OK");
        }

        /// <summary>
        /// Checks whether the properties of the event are not modified
        /// </summary>
        [TestMethod]
        public async Task Test_EventProperties()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEventDetailInfoById(ZebraAnalyticsEventMockDbSet.EVENT_1_ID);
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultEvent = JsonConvert.DeserializeObject<usp_ZebraAnalyticsGetEventByEventId_Result>(bodyContent);
            var expectedEvent = ZebraAnalyticsEventMockDbSet.EVENT_1;

            Assert.AreEqual(expectedEvent.Brief, resultEvent.Brief, "The event Brief field was unexpectedly modified");
            Assert.AreEqual(expectedEvent.CaseConf, resultEvent.CaseConf, "The event CaseConf field was unexpectedly modified");
            Assert.AreEqual(expectedEvent.CasesRpted, resultEvent.CasesRpted, "The event CasesRpted field was unexpectedly modified");
            Assert.AreEqual(expectedEvent.CaseSusp, resultEvent.CaseSusp, "The event CaseSusp field was unexpectedly modified");
            Assert.AreEqual(expectedEvent.Deaths, resultEvent.Deaths, "The event Brief field was unexpectedly modified");
            Assert.AreEqual(expectedEvent.DiseaseName, resultEvent.DiseaseName, "The event DiseaseName field was unexpectedly modified");
            Assert.AreEqual(expectedEvent.EndDate, resultEvent.EndDate, "The event EndDate field was unexpectedly modified");
            Assert.AreEqual(expectedEvent.EventTitle, resultEvent.EventTitle, "The event EventTitle field was unexpectedly modified");
            Assert.AreEqual(expectedEvent.IncubationPeriod, resultEvent.IncubationPeriod, "The event IncubationPeriod field was unexpectedly modified");
            Assert.AreEqual(expectedEvent.LastUpdatedDate, resultEvent.LastUpdatedDate, "The event LastUpdatedDate field was unexpectedly modified");
            Assert.AreEqual(expectedEvent.MicrobeType, resultEvent.MicrobeType, "The event MicrobeType field was unexpectedly modified");
            Assert.AreEqual(expectedEvent.PriorityTitle, resultEvent.PriorityTitle, "The event PriorityTitle field was unexpectedly modified");
            Assert.AreEqual(expectedEvent.ProbabilityName, resultEvent.ProbabilityName, "The event ProbabilityName field was unexpectedly modified");
            Assert.AreEqual(expectedEvent.Reasons, resultEvent.Reasons, "The event Reasons field was unexpectedly modified");
            Assert.AreEqual(expectedEvent.StartDate, resultEvent.StartDate, "The event StartDate field was unexpectedly modified");
            Assert.AreEqual(expectedEvent.TransmittedBy, resultEvent.TransmittedBy, "The event TransmittedBy field was unexpectedly modified");
            Assert.AreEqual(expectedEvent.Vaccination, resultEvent.Vaccination, "The event Vaccination field was unexpectedly modified");
        }

        /// <summary>
        /// Checks whether the properties of the event are still null
        /// </summary>
        [TestMethod]
        public async Task Test_EventNullProperties()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEventDetailInfoById(ZebraAnalyticsEventMockDbSet.EVENT_NULL_FIELDS_ID);
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultEvent = JsonConvert.DeserializeObject<usp_ZebraAnalyticsGetEventByEventId_Result>(bodyContent);

            Assert.IsNull(resultEvent.Brief, "The event Brief field should be null");
            Assert.IsNull(resultEvent.CaseConf, "The event CaseConf field should be null");
            Assert.IsNull(resultEvent.CasesRpted, "The event CasesRpted field should be null");
            Assert.IsNull(resultEvent.CaseSusp, "The event CaseSusp field should be null");
            Assert.IsNull(resultEvent.Deaths, "The event Brief field should be null");
            Assert.IsNull(resultEvent.DiseaseName, "The event DiseaseName field should be null");
            Assert.IsNull(resultEvent.EndDate, "The event EndDate field should be null");
            Assert.IsNull(resultEvent.EventTitle, "The event EventTitle field should be null");
            Assert.IsNull(resultEvent.IncubationPeriod, "The event IncubationPeriod field should be null");
            Assert.IsNull(resultEvent.LastUpdatedDate, "The event LastUpdatedDate field should be null");
            Assert.IsNull(resultEvent.MicrobeType, "The event MicrobeType field should be null");
            Assert.IsNull(resultEvent.PriorityTitle, "The event PriorityTitle field should be null");
            Assert.IsNull(resultEvent.ProbabilityName, "The event ProbabilityName field should be null");
            Assert.IsNull(resultEvent.Reasons, "The event Reasons field should be null");
            Assert.IsNull(resultEvent.StartDate, "The event StartDate field should be null");
            Assert.IsNull(resultEvent.TransmittedBy, "The event TransmittedBy field should be null");
            Assert.IsNull(resultEvent.Vaccination, "The event Vaccination field should be null");
        }
    }
}
