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
    public class ZebraAnalyticsGetEmailTest
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
        /// Checks whether the status code is correctly returning 200
        /// </summary>
        [TestMethod]
        public void Test_StatusCode_OK()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmail();
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode, "Successful request not returning 200 OK");
        }

        /// <summary>
        /// Checks whether a non-existent user ID returns an empty list of emails
        /// </summary>
        [TestMethod]
        public async Task Test_NonExistentUserId()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmail("");
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultEmails = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetEmailModel>>(bodyContent);
            Assert.IsNotNull(resultEmails, "Emails for non-existent user ID should not return null");
            Assert.AreEqual(0, resultEmails.Count, "Emails for non-existent user ID should return an empty list");
        }

        /// <summary>
        /// Checks whether a non-existent email type returns an empty list of emails
        /// </summary>
        [TestMethod]
        public async Task Test_NonExistentEmailType()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmail(
                ZebraAnalyticsEmailMockDbSet.SINGLE_EMAIL_USER_ID,
                null,
                -1);
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultEmails = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetEmailModel>>(bodyContent);
            Assert.IsNotNull(resultEmails, "Emails for non-existent email type should not return null");
            Assert.AreEqual(0, resultEmails.Count, "Emails for non-existent email type should return an empty list");
        }

        /// <summary>
        /// Checks whether the user with a single email is returned as expected
        /// </summary>
        [TestMethod]
        public async Task Test_SingleEmailFields()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmail(ZebraAnalyticsEmailMockDbSet.SINGLE_EMAIL_USER_ID);
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultEmails = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetEmailModel>>(bodyContent);
            Assert.AreEqual(1, resultEmails.Count, "User with only single email should only return emails for that user");

            var expectedEmail = ZebraAnalyticsEmailMockDbSet.SINGLE_EMAIL;
            var resultEmail = resultEmails.First();

            Assert.AreEqual(expectedEmail.Id, resultEmail.Id, "ID field was unexpectedly modified");
            Assert.AreEqual(expectedEmail.AoiGeonameIds, resultEmail.AoiGeonameIds, "AoiGeonameIds field was unexpectedly modified");
            Assert.AreEqual(expectedEmail.EmailType, resultEmail.EmailType, "EmailType field was unexpectedly modified");
            Assert.AreEqual(expectedEmail.EventId, resultEmail.EventId, "EventId field was unexpectedly modified");
            Assert.AreEqual(expectedEmail.SentDate, resultEmail.SentDate, "SentDate field was unexpectedly modified");
            Assert.AreEqual(expectedEmail.UserEmail, resultEmail.Email, "Email field was unexpectedly modified");
            Assert.AreEqual(expectedEmail.UserId, resultEmail.UserId, "UserId field was unexpectedly modified");
        }

        /// <summary>
        /// Checks whether the date range that does not include any results returns an empty list of emails
        /// </summary>
        [TestMethod]
        public async Task Test_SingleEmail_BeforeStartDate()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmail(
                ZebraAnalyticsEmailMockDbSet.SINGLE_EMAIL_USER_ID,
                null,
                null,
                ZebraAnalyticsEmailMockDbSet.SINGLE_EMAIL_SENT_DATE.AddDays(10).ToString("yyyy-MM-dd"));
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultEmails = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetEmailModel>>(bodyContent);
            Assert.IsNotNull(resultEmails, "Emails for no results should not return null");
            Assert.AreEqual(0, resultEmails.Count, "Email that is before the queried start date should not be included in the result");
        }

        /// <summary>
        /// Checks whether the date range that does not include any results returns an empty list of emails
        /// </summary>
        [TestMethod]
        public async Task Test_SingleEmail_AfterEndDate()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmail(
                ZebraAnalyticsEmailMockDbSet.SINGLE_EMAIL_USER_ID,
                null,
                null,
                null,
                ZebraAnalyticsEmailMockDbSet.SINGLE_EMAIL_SENT_DATE.AddDays(-10).ToString("yyyy-MM-dd"));
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultEmails = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetEmailModel>>(bodyContent);
            Assert.IsNotNull(resultEmails, "Emails for no results should not return null");
            Assert.AreEqual(0, resultEmails.Count, "Emailthat is after the queried end date should not be included in the result");
        }

        /// <summary>
        /// Checks whether the date range that does include the date range returns that email
        /// </summary>
        [TestMethod]
        public async Task Test_SingleEmail_WithinDateRange()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmail(
                ZebraAnalyticsEmailMockDbSet.SINGLE_EMAIL_USER_ID,
                null,
                null,
                ZebraAnalyticsEmailMockDbSet.SINGLE_EMAIL_SENT_DATE.AddDays(-10).ToString("yyyy-MM-dd"),
                ZebraAnalyticsEmailMockDbSet.SINGLE_EMAIL_SENT_DATE.AddDays(10).ToString("yyyy-MM-dd"));
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultEmails = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetEmailModel>>(bodyContent);
            Assert.AreEqual(1, resultEmails.Count, "User with only single email should only return emails for that user");
            Assert.AreEqual(ZebraAnalyticsEmailMockDbSet.SINGLE_EMAIL.Id, resultEmails.First().Id, "Email ID unexpectedly modified");
        }

        /// <summary>
        /// Checks whether all emails are returned for user with multiple emails
        /// </summary>
        [TestMethod]
        public async Task Test_MultipleEmails_All()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmail(
                ZebraAnalyticsEmailMockDbSet.MULTIPLE_EMAIL_USER_ID);
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultEmails = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetEmailModel>>(bodyContent);
            Assert.AreEqual(11, resultEmails.Count, "Not all expected emails were included");
        }

        /// <summary>
        /// Checks whether all emails are returned for user with multiple emails for a specific user email
        /// </summary>
        [TestMethod]
        public async Task Test_MultipleEmails_EmailQuery()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmail(
                ZebraAnalyticsEmailMockDbSet.MULTIPLE_EMAIL_USER_ID,
                ZebraAnalyticsEmailMockDbSet.MULTIPLE_EMAIL_USER_EMAIL);
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultEmails = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetEmailModel>>(bodyContent);
            Assert.AreEqual(11, resultEmails.Count, "Not all expected emails were included");
        }

        /// <summary>
        /// Checks whether all emails are returned for user with multiple emails for a specific user email type
        /// </summary>
        [TestMethod]
        public async Task Test_MultipleEmails_EmailTypeQuery()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmail(ZebraAnalyticsEmailMockDbSet.MULTIPLE_EMAIL_USER_ID, null, 1);
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultEmails = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetEmailModel>>(bodyContent);
            Assert.AreEqual(5, resultEmails.Count, "Not all expected emails were included");
        }

        /// <summary>
        /// Checks whether emails are returned as expected with a full start date time
        /// </summary>
        [TestMethod]
        public async Task Test_MultipleEmails_PartialRange()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmail(
                ZebraAnalyticsEmailMockDbSet.MULTIPLE_EMAIL_USER_ID,
                null,
                null,
                "2019-01-01T00:00:00",
                "2020-01-01T00:00:00");
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultEmails = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetEmailModel>>(bodyContent);
            Assert.AreEqual(8, resultEmails.Count, "Not all expected emails were included");
        }

        /// <summary>
        /// Checks whether emails are returned as expected with a full start date time
        /// </summary>
        [TestMethod]
        public async Task Test_StartDate_FullDateTime()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmail(
                ZebraAnalyticsEmailMockDbSet.MULTIPLE_EMAIL_USER_ID,
                null,
                null,
                "2019-12-31T23:59:59");
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultEmails = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetEmailModel>>(bodyContent);
            Assert.AreEqual(2, resultEmails.Count, "Not all expected emails were included");
        }

        /// <summary>
        /// Checks whether emails are returned as expected with a full start date with no time
        /// </summary>
        [TestMethod]
        public async Task Test_StartDate_FullDate()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmail(
                ZebraAnalyticsEmailMockDbSet.MULTIPLE_EMAIL_USER_ID,
                null,
                null,
                "2019-12-31");
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultEmails = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetEmailModel>>(bodyContent);
            Assert.AreEqual(3, resultEmails.Count, "Not all expected emails were included");
        }

        /// <summary>
        /// Checks whether emails are returned as expected with a start year and month only
        /// </summary>
        [TestMethod]
        public async Task Test_StartDate_YearAndMonth()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmail(
                ZebraAnalyticsEmailMockDbSet.MULTIPLE_EMAIL_USER_ID,
                null,
                null,
                "2019-12");
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultEmails = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetEmailModel>>(bodyContent);
            Assert.AreEqual(5, resultEmails.Count, "Not all expected emails were included");
        }

        /// <summary>
        /// Checks whether emails are returned as expected with start year only
        /// </summary>
        [TestMethod]
        public async Task Test_StartDate_YearOnly()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmail(
                ZebraAnalyticsEmailMockDbSet.MULTIPLE_EMAIL_USER_ID,
                null,
                null,
                "2019");
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultEmails = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetEmailModel>>(bodyContent);
            Assert.AreEqual(9, resultEmails.Count, "Not all expected emails were included");
        }

        /// <summary>
        /// Checks whether emails are returned as expected with a full end date time
        /// </summary>
        [TestMethod]
        public async Task Test_EndDate_FullDateTime()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmail(
                ZebraAnalyticsEmailMockDbSet.MULTIPLE_EMAIL_USER_ID,
                null,
                null,
                null,
                "2019-01-01T00:00:00");
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultEmails = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetEmailModel>>(bodyContent);
            Assert.AreEqual(2, resultEmails.Count, "Not all expected emails were included");
        }

        /// <summary>
        /// Checks whether emails are returned as expected with a full end date with no time
        /// </summary>
        [TestMethod]
        public async Task Test_EndDate_FullDate()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmail(
                ZebraAnalyticsEmailMockDbSet.MULTIPLE_EMAIL_USER_ID,
                null,
                null,
                null,
                "2019-01-01");
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultEmails = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetEmailModel>>(bodyContent);
            Assert.AreEqual(3, resultEmails.Count, "Not all expected emails were included");
        }

        /// <summary>
        /// Checks whether emails are returned as expected with a end year and month only
        /// </summary>
        [TestMethod]
        public async Task Test_EndDate_YearAndMonth()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmail(
                ZebraAnalyticsEmailMockDbSet.MULTIPLE_EMAIL_USER_ID,
                null,
                null,
                null,
                "2019-01");
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultEmails = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetEmailModel>>(bodyContent);
            Assert.AreEqual(5, resultEmails.Count, "Not all expected emails were included");
        }

        /// <summary>
        /// Checks whether emails are returned as expected with end year only
        /// </summary>
        [TestMethod]
        public async Task Test_EndDate_YearOnly()
        {
            HttpResponseMessage result = controller.ZebraAnalyticsGetEmail(
                ZebraAnalyticsEmailMockDbSet.MULTIPLE_EMAIL_USER_ID,
                null,
                null,
                null,
                "2019");
            string bodyContent = await result.Content.ReadAsStringAsync();

            var resultEmails = JsonConvert.DeserializeObject<List<ZebraAnalyticsGetEmailModel>>(bodyContent);
            Assert.AreEqual(9, resultEmails.Count, "Not all expected emails were included");
        }
    }
}
