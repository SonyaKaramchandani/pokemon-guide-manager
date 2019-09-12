using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Biod.Zebra.Api.Analytics;
using Biod.Zebra.Api.Api.Analytics;
using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Models.Analytics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace Biod.Solution.UnitTest.Api.Analytics
{
    /// <summary>
    /// Tests the API controller for getting the Email Content by Email ID
    /// </summary>
    [TestClass]
    public class ZebraAnalyticsGetEmailContentByIdTest
    {
        private ZebraAnalyticsEmailController controller;
        private Mock<BiodZebraEntities> mockDbContext;
        private ZebraAnalyticsEmailMockDbSet dbMock;

        [TestInitialize()]
        public void Initialize()
        {
            dbMock = new ZebraAnalyticsEmailMockDbSet();
            mockDbContext = dbMock.MockContext;

            // Configure the controller request
            controller = new ZebraAnalyticsEmailController
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
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmailContentById(-1);
            Assert.AreEqual(System.Net.HttpStatusCode.NotFound, result.StatusCode, "Non-existent user id request not returning 404 Not Found");
        }

        /// <summary>
        /// Checks whether the status code is correctly returning 200
        /// </summary>
        [TestMethod]
        public void Test_StatusCode_OK()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmailContentById(ZebraAnalyticsEmailMockDbSet.EMAIL_1_ID);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode, "Successful request not returning 200 OK");
        }

        /// <summary>
        /// Checks whether the content of the email is not modified
        /// </summary>
        [TestMethod]
        public async Task Test_EmailContent()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmailContentById(ZebraAnalyticsEmailMockDbSet.EMAIL_1_ID);
            string bodyContent = await result.Content.ReadAsStringAsync();

            var email = JsonConvert.DeserializeObject<ZebraAnalyticsGetEmailContentModel>(bodyContent);
            Assert.AreEqual(ZebraAnalyticsEmailMockDbSet.EMAIL_1_CONTENT, email.Content, "The email Content field was unexpectedly modified");
        }

        /// <summary>
        /// Checks whether the content of the email is still null
        /// </summary>
        [TestMethod]
        public async Task Test_EmailNullContent()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmailContentById(ZebraAnalyticsEmailMockDbSet.EMAIL_NULL_CONTENT_ID);
            string bodyContent = await result.Content.ReadAsStringAsync();

            var email = JsonConvert.DeserializeObject<ZebraAnalyticsGetEmailContentModel>(bodyContent);
            Assert.IsNull(email.Content, "The email Content field should be null");
        }
    }
}
