using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Biod.Solution.IntegrationTest.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Biod.Solution.IntegrationTest.ZebraApi
{
    [TestClass]
    public class ZebraApiTests : BaseApiTest
    {
        const string zebraEmailUsersLocatedInEventAreaV2ControllerUrl = "/api/ZebraEmailUsersLocatedInEventAreaV2";
        const string zebraEmailUsersLocatedInEventDestinationAreaV2ControllerUrl = "/api/ZebraEmailUsersLocatedInEventDestinationAreaV2";
        const string zebraEmailUsersProximalEmailControllerUrl = "/api/ZebraEmailUsersProximalEmail";
        const string ZebraEmailUsersWeeklyEmailControllerUrl = "/api/ZebraEmailUsersWeeklyEmail";
        const string ZebraAnalyticsEvent = "/api/ZebraAnalytics/Event";

        const string eventId = "137";
        readonly List<string> eventIds = new List<string> { "115", "116", "122", "124", "126", "127", "136", "137" };


        protected override string TestApiName => "ZebraApi";
        protected override string ApiBaseUrl => ConfigurationManager.AppSettings["url.ZebraApi"];

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            _testingHttpClient = new TestingHttpClient(new HttpClient()); //one http client per test class
        }

        [TestMethod]
        public async Task GetZebraEmailUsersLocatedInEventAreaV2_Success()
        {
            await FetchAndAssert_AreStatusCodeEqualAsync($"{zebraEmailUsersLocatedInEventAreaV2ControllerUrl}?eventId={eventId}", HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task GetZebraEmailUsersLocatedInEventDestinationAreaV2_Success()
        {
            await FetchAndAssert_AreStatusCodeEqualAsync($"{zebraEmailUsersLocatedInEventDestinationAreaV2ControllerUrl}?eventId={eventId}", HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task GetZebraEmailUsersProximalEmail_Success()
        {
            await FetchAndAssert_AreStatusCodeEqualAsync($"{zebraEmailUsersProximalEmailControllerUrl}?eventId={eventId}", HttpStatusCode.OK);
        }
        
        [TestMethod]
        public async Task GetZebraEmailUsersWeeklyEmail_Success()
        {
            await FetchAndAssert_AreStatusCodeEqualAsync($"{ZebraEmailUsersWeeklyEmailControllerUrl}?eventId={eventId}", HttpStatusCode.OK);
        }

        [TestMethod]
        public void Get_Trigger_Multiple_Event_Emails_Success()
        {
            List<Task> tasks = new List<Task>();
            eventIds.ForEach(eventId =>
            {
                tasks.Add(FetchAndAssert_AreStatusCodeEqualAsync($"{zebraEmailUsersLocatedInEventAreaV2ControllerUrl}?eventId={eventId}", HttpStatusCode.OK));
                tasks.Add(FetchAndAssert_AreStatusCodeEqualAsync($"{zebraEmailUsersLocatedInEventDestinationAreaV2ControllerUrl}?eventId={eventId}", HttpStatusCode.OK));
                tasks.Add(FetchAndAssert_AreStatusCodeEqualAsync($"{zebraEmailUsersProximalEmailControllerUrl}?eventId={eventId}", HttpStatusCode.OK));
            });
           Task.WaitAll(tasks.ToArray(), TimeSpan.FromSeconds(240));
        }
        #region Event
        [TestMethod]
        public async Task GetZebraAnalyticsEvent_Success()
        {
            await FetchAndAssert_IsExpectedJsonResultAsync(
                $"{ZebraAnalyticsEvent}?eventId={eventId}",
                $"GetZebraAnalyticsEvent_Success.json");
        }
        [TestMethod]
        public async Task GetZebraAnalyticsEvent_NotFound()
        {
            await FetchAndAssert_IsExpectedJsonResultAsync(
                $"{ZebraAnalyticsEvent}?eventId=-1",
                $"GetZebraAnalyticsEvent_NotFound.json");
        }
        [TestMethod]
        public async Task GetZebraAnalyticsEvent_InvalidId()
        {
            await FetchAndAssert_AreStatusCodeEqualAsync(
                $"{ZebraAnalyticsEvent}?eventId=invalid",
                HttpStatusCode.BadRequest);
        }
       
        [TestMethod]
        public async Task GetZebraEventInfo_Success()
        {
            await FetchAndAssert_AreStatusCodeEqualAsync(
                $"/api/ZebraEventInfo?GeonameId=387257",
                HttpStatusCode.OK);
        }
        #endregion
        #region User
        [TestMethod]
        public async Task GetZebraAnalyticsUser_Success()
        {
            await FetchAndAssert_IsExpectedJsonResultAsync(
                "/api/ZebraAnalytics/User?userId=08565091-a8f3-4290-9b83-6000979bb5dc"
                , "GetZebraAnalyticsUser_Success.json");
        }
        [TestMethod]
        public async Task GetZebraAnalyticsUser_NotFound()
        {
            await FetchAndAssert_AreStatusCodeEqualAsync(
                "/api/ZebraAnalytics/User?userId=98565091-a8f3-4290-9b83-6000979bb5db"
                , HttpStatusCode.NotFound);
        }
        [TestMethod]
        public async Task GetZebraAnalyticsUserGroups_Success()
        {
            await FetchAndAssert_IsExpectedJsonResultAsync(
                "/api/ZebraAnalytics/UserGroup"
                , "GetZebraAnalyticsUserGroups.json");
        }
        [TestMethod]
        public async Task GetZebraAnalyticsUserLoginHistory_Success()
        {
            await FetchAndAssert_AreStatusCodeEqualAsync(
               "/api/ZebraAnalytics/UserLoginHistory?userId=4034e30c-8b1e-47be-a881-5fab3c4017d5&startDate=2018-10-25&endDate=2018-10-25"
               , HttpStatusCode.OK);
        }
        [TestMethod]
        public async Task GetZebraAnalyticsUserRole_Success()
        {
            await FetchAndAssert_IsExpectedJsonResultAsync(
                "/api/ZebraAnalytics/UserRole"
                , "GetZebraAnalyticsUserRole_Success.json");
        }
        [TestMethod]
        public async Task ZebraAoiLocationAutocomplete_Success()
        {
            await FetchAndAssert_IsExpectedJsonResultAsync(
                "/api/ZebraAoiLocationAutocomplete?aoiGeonameIds=387257%2C433897"
                , "ZebraAoiLocationAutocomplete_Success.json");
        }

        #endregion
        #region Location
        [TestMethod]
        public async Task GetZebraDestinationAirports_Success()
        {
            await FetchAndAssert_IsExpectedJsonResultAsync(
                $"/api/ZebraDestinationAirports?EventId={eventId}"
                , "GetZebraDestinationAirports_Success.json");
        }
        [TestMethod]
        public async Task GetZebraInitDashboard_Success()
        {
            await FetchAndAssert_AreStatusCodeEqualAsync(
            "/api/ZebraInitDashboard?userId=4034e30c-8b1e-47be-a881-5fab3c4017d5&geonameIds=6167865&diseasesIds=85&locationOnly=true"
            , HttpStatusCode.OK); //TODO: inspect the expected output json once we configure a test database
        }
        #endregion
        [TestMethod]
        [ClassCleanup]
        public static void ClassCleanup()
        {
            _testingHttpClient.Dispose();
        }
    }
}
