using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Biod.Zebra.Api.Api.Analytics;
using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Models.Analytics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace Biod.Solution.UnitTest.Api.Analytics
{
    /// <summary>
    /// Tests the API controller for getting the list of Email Types
    /// </summary>
    [TestClass]
    public class ZebraAnalyticsEmailTypeTest
    {
        private ZebraAnalyticsEmailTypeController controller;
        private Mock<BiodZebraEntities> mockDbContext;
        private ZebraAnalyticsEmailTypeMockDbSet dbMock;

        [TestInitialize()]
        public void Initialize()
        {
            dbMock = new ZebraAnalyticsEmailTypeMockDbSet();
            mockDbContext = dbMock.MockContext;

            // Configure the controller request
            controller = new ZebraAnalyticsEmailTypeController
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
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmailType();
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode, "Successful request not returning 200 OK");
        }

        /// <summary>
        /// Checks whether all the email types are being returned
        /// </summary>
        [TestMethod]
        public async Task Test_EmailTypeCount()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmailType();
            string bodyContent = await result.Content.ReadAsStringAsync();

            var emailTypes = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetEmailTypeModel>>(bodyContent);
            Assert.AreEqual(mockDbContext.Object.UserEmailTypes.Count(), emailTypes.Count, "Not all email types from the database are returned in the response");
        }

        /// <summary>
        /// Checks whether a specific email type is being returned
        /// </summary>
        [TestMethod]
        public async Task Test_SpecificEmailType()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmailType();
            string bodyContent = await result.Content.ReadAsStringAsync();

            var emailTypes = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetEmailTypeModel>>(bodyContent);
            var resultEmailType = emailTypes.FirstOrDefault(e => e.Id == ZebraAnalyticsEmailTypeMockDbSet.EMAIL_TYPE_1_ID);
            Assert.IsNotNull(resultEmailType, "One of the expected email types was not returned");

            var expectedEmailType = mockDbContext.Object.UserEmailTypes.First(e => e.Id == ZebraAnalyticsEmailTypeMockDbSet.EMAIL_TYPE_1_ID);

            Assert.AreEqual(expectedEmailType.Type, resultEmailType.Type, "The Type field has been unexpectedly modified");
        }
    }
}
