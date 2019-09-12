using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using Biod.Zebra.Api.Analytics;
using Biod.Zebra.Api.Api.Analytics;
using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.Models.Analytics;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace Biod.Solution.UnitTest.Api.Analytics
{
    /// <summary>
    /// Tests the API controller for getting the User Details by User ID
    /// </summary>
    [TestClass]
    public class ZebraAnalyticsUserTest
    {
        private ZebraAnalyticsUserController controller;
        private Mock<UserManager<ApplicationUser>> mockUserManagerContext;
        private Mock<BiodZebraEntities> mockDbContext;
        private ZebraAnalyticsUserMockDbSet dbMock;

        [TestInitialize()]
        public void Initialize()
        {
            dbMock = new ZebraAnalyticsUserMockDbSet();
            mockUserManagerContext = dbMock.MockContext;
            mockDbContext = dbMock.MockDbContext;

            // Configure the controller request
            controller = new ZebraAnalyticsUserController
            {
                Request = new HttpRequestMessage()
            };
            controller.Request.SetConfiguration(new HttpConfiguration());

            // Replace db context in controller
            controller.DbContext = mockDbContext.Object;

            // Replace User Manager in controller
            controller.UserManager = mockUserManagerContext.Object;
        }

        /// <summary>
        /// Checks whether the status code is correctly returning 404
        /// </summary>
        [TestMethod]
        public void Test_StatusCode_NotFound()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserById("");
            Assert.AreEqual(System.Net.HttpStatusCode.NotFound, result.StatusCode, "Non-existent user id request not returning 404 Not Found");
        }

        /// <summary>
        /// Checks whether the status code is correctly returning 200
        /// </summary>
        [TestMethod]
        public void Test_StatusCode_OK()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserById(ZebraAnalyticsUserMockDbSet.USER_1_ID);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode, "Successful request not returning 200 OK");
        }

        /// <summary>
        /// Checks whether the properties of the user are not modified
        /// </summary>
        [TestMethod]
        public async Task Test_UserProperties()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserById(ZebraAnalyticsUserMockDbSet.USER_1_ID);
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultUser = JsonConvert.DeserializeObject<ZebraAnalyticsGetUserModel>(bodyContent);
            var expectedUser = ZebraAnalyticsUserMockDbSet.USER_1;

            Assert.AreEqual(expectedUser.AoiGeonameIds, string.Join(",", resultUser.AoiGeonameIds), "The user AoiGeonameIds field was unexpectedly modified");
            Assert.AreEqual(ZebraAnalyticsUserMockDbSet.USER_1_FIRST_LOGIN_DATE, resultUser.FirstLoginDate, "The user FirstLoginDate field was unexpectedly modified");
            Assert.AreEqual(ZebraAnalyticsUserMockDbSet.USER_1_LAST_MODIFIED_DATE, resultUser.LastModifiedDate, "The user LastModifiedDate field was unexpectedly modified");
            Assert.AreEqual(expectedUser.UserGroupId, resultUser.GroupId, "The user GroupId field was unexpectedly modified");
            Assert.AreEqual(expectedUser.Location, resultUser.Location, "The user Location field was unexpectedly modified");
            Assert.AreEqual(expectedUser.GeonameId, resultUser.LocationGeonameId, "The user LocationGeonameId field was unexpectedly modified");
            Assert.AreEqual(expectedUser.Organization, resultUser.Organization, "The user Organization field was unexpectedly modified");
            Assert.AreEqual(expectedUser.UserName, resultUser.UserName, "The user UserName field was unexpectedly modified");
            Assert.AreEqual(expectedUser.FirstName, resultUser.FirstName, "The user FirstName field was unexpectedly modified");
            Assert.AreEqual(expectedUser.LastName, resultUser.LastName, "The user LastName field was unexpectedly modified");
        }

        /// <summary>
        /// Checks whether the roles of the user are not modified
        /// </summary>
        [TestMethod]
        public async Task Test_UserRoles()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserById(ZebraAnalyticsUserMockDbSet.USER_1_ID);
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultUser = JsonConvert.DeserializeObject<ZebraAnalyticsGetUserModel>(bodyContent);
            var expectedUser = ZebraAnalyticsUserMockDbSet.USER_1;

            var resultRoles = new HashSet<string>(resultUser.Roles);
            var expectedRoles = new HashSet<string>(expectedUser.Roles.Select(r => r.RoleId));

            Assert.AreEqual(expectedRoles.Count, resultRoles.Count, "The user Roles field was unexpectedly modified");
            Assert.AreEqual(0, expectedRoles.Except(resultRoles).Count(), "Not all user Roles were returned");
            Assert.AreEqual(0, resultRoles.Except(expectedRoles).Count(), "Unexpected user Roles were added");
        }

        /// <summary>
        /// Checks whether the notification settings of the user are not modified
        /// </summary>
        [TestMethod]
        public async Task Test_UserNotificationSettings()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserById(ZebraAnalyticsUserMockDbSet.USER_1_ID);
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultUser = JsonConvert.DeserializeObject<ZebraAnalyticsGetUserModel>(bodyContent);
            var expectedUser = ZebraAnalyticsUserMockDbSet.USER_1;

            Assert.IsTrue(resultUser.NotificationSettings.Count() > 0, "The user NotificationSettings field cannot be an empty array");

            Type type = typeof(ApplicationUser);
            foreach (var setting in resultUser.NotificationSettings)
            {
                PropertyInfo info = type.GetProperty(setting.NotificationType);
                Assert.IsNotNull(info, $"Unexpected setting type {setting.NotificationType}");

                var fieldValue = info.GetValue(expectedUser);
                Assert.IsInstanceOfType(fieldValue, typeof(bool), $"Returned referenced setting {setting.NotificationType} is not a notification setting");

                Assert.AreEqual((bool)fieldValue, setting.IsEnabled, $"The user setting {setting.NotificationType} was unexpectedly modified");
            }
        }

        /// <summary>
        /// Checks whether the properties of the user are still null/defaulted to empty
        /// </summary>
        [TestMethod]
        public async Task Test_UserNullProperties()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserById(ZebraAnalyticsUserMockDbSet.USER_NULL_FIELDS_ID);
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultUser = JsonConvert.DeserializeObject<ZebraAnalyticsGetUserModel>(bodyContent);

            Assert.IsNull(resultUser.FirstLoginDate, "The user FirstLoginDate field should be null");
            Assert.IsNull(resultUser.LastModifiedDate, "The user LastModifiedDate field should be null");
            Assert.IsNull(resultUser.GroupId, "The user GroupId field should be null");
            Assert.IsNull(resultUser.Location, "The user Location field should be null");
            Assert.IsNull(resultUser.Organization, "The user Organization field should be null");
            Assert.IsNull(resultUser.UserName, "The user UserName field should be null");
            Assert.IsNull(resultUser.FirstName, "The user FirstName field should be null");
            Assert.IsNull(resultUser.LastName, "The user LastName field should be null");

            Assert.AreEqual(0, resultUser.LocationGeonameId, "The user LocationGeonameId field should be 0");

            Assert.IsNotNull(resultUser.AoiGeonameIds, "The user AoiGeonameIds field should be an empty array");
            Assert.IsNotNull(resultUser.Roles, "The user Roles field should be an empty array");
            Assert.IsNotNull(resultUser.NotificationSettings, "The user NotificationSettings field should not be null");

            Assert.IsTrue(resultUser.NotificationSettings.Count() > 0, "The user NotificationSettings field cannot be an empty array");
            foreach (var setting in resultUser.NotificationSettings)
            {
                Assert.IsFalse(setting.IsEnabled, $"The notification setting {setting.NotificationType} should be false");
            }
        }
    }
}
