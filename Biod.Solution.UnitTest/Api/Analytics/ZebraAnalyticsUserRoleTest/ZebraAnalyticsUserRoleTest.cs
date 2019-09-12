using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Biod.Zebra.Api.Analytics;
using Biod.Zebra.Api.Api.Analytics;
using Biod.Zebra.Library.Models.Analytics;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace Biod.Solution.UnitTest.Api.Analytics
{
    /// <summary>
    /// Tests the API controller for getting the list of User Roles
    /// </summary>
    [TestClass]
    public class ZebraAnalyticsUserRoleTest
    {
        private ZebraAnalyticsUserRoleController controller;
        private Mock<RoleManager<IdentityRole>> mockRoleManagerContext;
        private ZebraAnalyticsUserRoleMockDbSet dbMock;

        [TestInitialize()]
        public void Initialize()
        {
            dbMock = new ZebraAnalyticsUserRoleMockDbSet();
            mockRoleManagerContext = dbMock.MockContext;

            // Configure the controller request
            controller = new ZebraAnalyticsUserRoleController
            {
                Request = new HttpRequestMessage()
            };
            controller.Request.SetConfiguration(new HttpConfiguration());

            // Replace Role Manager in controller
            controller.LocalRoleManager = mockRoleManagerContext.Object;
        }

        /// <summary>
        /// Checks whether the status code is correctly returning 200
        /// </summary>
        [TestMethod]
        public void Test_StatusCode_OK()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserRole();
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode, "Successful request not returning 200 OK");
        }

        /// <summary>
        /// Checks whether all the user roles are being returned
        /// </summary>
        [TestMethod]
        public async Task Test_UserRoleCount()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserRole();
            string bodyContent = await result.Content.ReadAsStringAsync();

            var userRoles = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetUserRoleModel>>(bodyContent);
            Assert.AreEqual(mockRoleManagerContext.Object.Roles.Count(), userRoles.Count, "Not all user roles from the database are returned in the response");
        }

        /// <summary>
        /// Checks whether a specific user role is being returned
        /// </summary>
        [TestMethod]
        public async Task Test_SpecificUserRole()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserRole();
            string bodyContent = await result.Content.ReadAsStringAsync();

            var userRoles = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetUserRoleModel>>(bodyContent);
            var resultUserRole = userRoles.FirstOrDefault(e => e.Id == ZebraAnalyticsUserRoleMockDbSet.USER_ROLE_1_ID);
            Assert.IsNotNull(resultUserRole, "One of the expected user roles was not returned");

            var expectedUserRole = mockRoleManagerContext.Object.Roles.First(r => r.Id == ZebraAnalyticsUserRoleMockDbSet.USER_ROLE_1_ID);

            Assert.AreEqual(expectedUserRole.Name, resultUserRole.Role, "The Role field has been unexpectedly modified");
        }
    }
}
