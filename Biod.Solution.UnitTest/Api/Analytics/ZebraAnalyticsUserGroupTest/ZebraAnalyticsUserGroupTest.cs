using Biod.Zebra.Api.Api.Analytics;
using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Models.Analytics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Biod.Solution.UnitTest.Api.Analytics
{
    /// <summary>
    /// Tests the API controller for getting the list of User Groups
    /// </summary>
    [TestClass]
    public class ZebraAnalyticsUserGroupTest
    {
        private ZebraAnalyticsUserGroupController controller;
        private Mock<BiodZebraEntities> mockDbContext;
        private ZebraAnalyticsUserGroupMockDbSet dbMock;

        [TestInitialize()]
        public void Initialize()
        {
            dbMock = new ZebraAnalyticsUserGroupMockDbSet();
            mockDbContext = dbMock.MockContext;

            // Configure the controller request
            controller = new ZebraAnalyticsUserGroupController
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
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserGroup();
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode, "Successful request not returning 200 OK");
        }

        /// <summary>
        /// Checks whether all the user groups are being returned
        /// </summary>
        [TestMethod]
        public async Task Test_UserGroupCount()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserGroup();
            string bodyContent = await result.Content.ReadAsStringAsync();

            var userGroups = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetUserGroupModel>>(bodyContent);
            Assert.AreEqual(mockDbContext.Object.UserGroups.Count(), userGroups.Count, "Not all user groups from the database are returned in the response");
        }

        /// <summary>
        /// Checks whether a specific user group is being returned
        /// </summary>
        [TestMethod]
        public async Task Test_SpecificUserGroup()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserGroup();
            string bodyContent = await result.Content.ReadAsStringAsync();

            var userGroups = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetUserGroupModel>>(bodyContent);
            var resultUserGroup = userGroups.FirstOrDefault(e => e.Id == ZebraAnalyticsUserGroupMockDbSet.USER_GROUP_1_ID);
            Assert.IsNotNull(resultUserGroup, "One of the expected user groups was not returned");

            var expectedUserGroup = mockDbContext.Object.UserGroups.First(e => e.Id == ZebraAnalyticsUserGroupMockDbSet.USER_GROUP_1_ID);

            Assert.AreEqual(expectedUserGroup.Name, resultUserGroup.Name, "The Type field has been unexpectedly modified");
        }
    }
}
