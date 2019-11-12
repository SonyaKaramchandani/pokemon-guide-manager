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
    /// Tests the API controller for getting the User Login History
    /// </summary>
    [TestClass]
    public class ZebraAnalyticsUserLoginHistoryTest
    {
        private ZebraAnalyticsUserLoginHistoryController controller;
        private Mock<BiodZebraEntities> mockContext;
        private ZebraAnalyticsUserLoginHistoryMockDbSet dbMock;

        [TestInitialize()]
        public void Initialize()
        {
            dbMock = new ZebraAnalyticsUserLoginHistoryMockDbSet();
            mockContext = dbMock.MockContext;

            // Configure the controller request
            controller = new ZebraAnalyticsUserLoginHistoryController
            {
                Request = new HttpRequestMessage()
            };
            controller.Request.SetConfiguration(new HttpConfiguration());

            // Replace db context in controller
            controller.DbContext = mockContext.Object;
        }

        /// <summary>
        /// Checks whether the status code is correctly returning 200
        /// </summary>
        [TestMethod]
        public void Test_StatusCode_OK()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserLoginHistory(ZebraAnalyticsUserLoginHistoryMockDbSet.SINGLE_LOGIN_HISTORY_USER_ID);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode, "Successful request not returning 200 OK");
        }

        /// <summary>
        /// Checks whether a non-existent user ID returns an empty list of logins
        /// </summary>
        [TestMethod]
        public async Task Test_NonExistentUserId()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserLoginHistory("");
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultLogins = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetUserLoginHistoryModel>>(bodyContent);
            Assert.IsNotNull(resultLogins, "Login history for non-existent user ID should not return null");
            Assert.AreEqual(0, resultLogins.Count, "Login history for non-existent user ID should return an empty list");
        }

        /// <summary>
        /// Checks whether the user with a single login history is returned as expected
        /// </summary>
        [TestMethod]
        public async Task Test_SingleLoginHistory_NoDateRange()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserLoginHistory(ZebraAnalyticsUserLoginHistoryMockDbSet.SINGLE_LOGIN_HISTORY_USER_ID);
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultLogins = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetUserLoginHistoryModel>>(bodyContent);
            Assert.AreEqual(1, resultLogins.Count, "User with only single login history should only return logins for that user");
            Assert.AreEqual(ZebraAnalyticsUserLoginHistoryMockDbSet.SINGLE_LOGIN_HISTORY_LOGIN_DATE, resultLogins.First().LoginDate, "Login date in history unexpectedly modified");
        }

        /// <summary>
        /// Checks whether the date range that does not include any results returns an empty list of logins
        /// </summary>
        [TestMethod]
        public async Task Test_SingleLoginHistory_BeforeStartDate()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserLoginHistory(
                ZebraAnalyticsUserLoginHistoryMockDbSet.SINGLE_LOGIN_HISTORY_USER_ID,
                ZebraAnalyticsUserLoginHistoryMockDbSet.SINGLE_LOGIN_HISTORY_LOGIN_DATE.AddDays(10).ToString("yyyy-MM-dd")) ;
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultLogins = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetUserLoginHistoryModel>>(bodyContent);
            Assert.IsNotNull(resultLogins, "Login history for no results should not return null");
            Assert.AreEqual(0, resultLogins.Count, "User Login that is before the queried start date should not be included in the result");
        }

        /// <summary>
        /// Checks whether the date range that does not include any results returns an empty list of logins
        /// </summary>
        [TestMethod]
        public async Task Test_SingleLoginHistory_AfterEndDate()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserLoginHistory(
                ZebraAnalyticsUserLoginHistoryMockDbSet.SINGLE_LOGIN_HISTORY_USER_ID,
                null,
                ZebraAnalyticsUserLoginHistoryMockDbSet.SINGLE_LOGIN_HISTORY_LOGIN_DATE.AddDays(-10).ToString("yyyy-MM-dd"));
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultLogins = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetUserLoginHistoryModel>>(bodyContent);
            Assert.IsNotNull(resultLogins, "Login history for no results should not return null");
            Assert.AreEqual(0, resultLogins.Count, "User Login that is after the queried end date should not be included in the result");
        }

        /// <summary>
        /// Checks whether the date range that does include the date range returns that login history
        /// </summary>
        [TestMethod]
        public async Task Test_SingleLoginHistory_WithinDateRange()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserLoginHistory(
                ZebraAnalyticsUserLoginHistoryMockDbSet.SINGLE_LOGIN_HISTORY_USER_ID,
                ZebraAnalyticsUserLoginHistoryMockDbSet.SINGLE_LOGIN_HISTORY_LOGIN_DATE.AddDays(-10).ToString("yyyy-MM-dd"),
                ZebraAnalyticsUserLoginHistoryMockDbSet.SINGLE_LOGIN_HISTORY_LOGIN_DATE.AddDays(10).ToString("yyyy-MM-dd"));
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultLogins = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetUserLoginHistoryModel>>(bodyContent);
            Assert.AreEqual(1, resultLogins.Count, "User with only single login history should only return logins for that user");
            Assert.AreEqual(ZebraAnalyticsUserLoginHistoryMockDbSet.SINGLE_LOGIN_HISTORY_LOGIN_DATE, resultLogins.First().LoginDate, "Login date in history unexpectedly modified");
        }

        /// <summary>
        /// Checks whether all logins are returned for user with multiple login history
        /// </summary>
        [TestMethod]
        public async Task Test_MultipleLoginHistory_All()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserLoginHistory(
                ZebraAnalyticsUserLoginHistoryMockDbSet.MULTIPLE_LOGIN_HISTORY_USER_ID);
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultLogins = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetUserLoginHistoryModel>>(bodyContent);
            Assert.AreEqual(11, resultLogins.Count, "Not all expected login dates were included");
        }

        /// <summary>
        /// Checks whether all logins are returned for user with multiple login history within a specific date range
        /// </summary>
        [TestMethod]
        public async Task Test_MultipleLoginHistory_PartialRange()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserLoginHistory(
                ZebraAnalyticsUserLoginHistoryMockDbSet.MULTIPLE_LOGIN_HISTORY_USER_ID,
                "2019-01-01T00:00:00",
                "2020-01-01T00:00:00");
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultLogins = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetUserLoginHistoryModel>>(bodyContent);
            Assert.AreEqual(8, resultLogins.Count, "Not all expected login dates were included");
        }

        /// <summary>
        /// Checks whether logins are returned as expected with a full start date time
        /// </summary>
        [TestMethod]
        public async Task Test_StartDate_FullDateTime()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserLoginHistory(
                ZebraAnalyticsUserLoginHistoryMockDbSet.MULTIPLE_LOGIN_HISTORY_USER_ID,
                "2019-12-31T23:59:59");
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultLogins = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetUserLoginHistoryModel>>(bodyContent);
            Assert.AreEqual(2, resultLogins.Count, "Not all expected login dates were included");
        }

        /// <summary>
        /// Checks whether logins are returned as expected with a full start date with no time
        /// </summary>
        [TestMethod]
        public async Task Test_StartDate_FullDate()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserLoginHistory(
                ZebraAnalyticsUserLoginHistoryMockDbSet.MULTIPLE_LOGIN_HISTORY_USER_ID,
                "2019-12-31");
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultLogins = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetUserLoginHistoryModel>>(bodyContent);
            Assert.AreEqual(3, resultLogins.Count, "Not all expected login dates were included");
        }

        /// <summary>
        /// Checks whether logins are returned as expected with a start year and month only
        /// </summary>
        [TestMethod]
        public async Task Test_StartDate_YearAndMonth()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserLoginHistory(
                ZebraAnalyticsUserLoginHistoryMockDbSet.MULTIPLE_LOGIN_HISTORY_USER_ID,
                "2019-12");
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultLogins = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetUserLoginHistoryModel>>(bodyContent);
            Assert.AreEqual(5, resultLogins.Count, "Not all expected login dates were included");
        }

        /// <summary>
        /// Checks whether logins are returned as expected with start year only
        /// </summary>
        [TestMethod]
        public async Task Test_StartDate_YearOnly()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserLoginHistory(
                ZebraAnalyticsUserLoginHistoryMockDbSet.MULTIPLE_LOGIN_HISTORY_USER_ID,
                "2019");
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultLogins = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetUserLoginHistoryModel>>(bodyContent);
            Assert.AreEqual(9, resultLogins.Count, "Not all expected login dates were included");
        }

        /// <summary>
        /// Checks whether logins are returned as expected with a full end date time
        /// </summary>
        [TestMethod]
        public async Task Test_EndDate_FullDateTime()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserLoginHistory(
                ZebraAnalyticsUserLoginHistoryMockDbSet.MULTIPLE_LOGIN_HISTORY_USER_ID,
                null,
                "2019-01-01T00:00:00");
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultLogins = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetUserLoginHistoryModel>>(bodyContent);
            Assert.AreEqual(2, resultLogins.Count, "Not all expected login dates were included");
        }

        /// <summary>
        /// Checks whether logins are returned as expected with a full end date with no time
        /// </summary>
        [TestMethod]
        public async Task Test_EndDate_FullDate()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserLoginHistory(
                ZebraAnalyticsUserLoginHistoryMockDbSet.MULTIPLE_LOGIN_HISTORY_USER_ID,
                null,
                "2019-01-01");
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultLogins = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetUserLoginHistoryModel>>(bodyContent);
            Assert.AreEqual(3, resultLogins.Count, "Not all expected login dates were included");
        }

        /// <summary>
        /// Checks whether logins are returned as expected with a end year and month only
        /// </summary>
        [TestMethod]
        public async Task Test_EndDate_YearAndMonth()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserLoginHistory(
                ZebraAnalyticsUserLoginHistoryMockDbSet.MULTIPLE_LOGIN_HISTORY_USER_ID,
                null,
                "2019-01");
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultLogins = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetUserLoginHistoryModel>>(bodyContent);
            Assert.AreEqual(5, resultLogins.Count, "Not all expected login dates were included");
        }

        /// <summary>
        /// Checks whether logins are returned as expected with a end year only
        /// </summary>
        [TestMethod]
        public async Task Test_EndDate_YearOnly()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetUserLoginHistory(
                ZebraAnalyticsUserLoginHistoryMockDbSet.MULTIPLE_LOGIN_HISTORY_USER_ID,
                null,
                "2019");
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultLogins = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetUserLoginHistoryModel>>(bodyContent);
            Assert.AreEqual(9, resultLogins.Count, "Not all expected login dates were included");
        }
    }
}
