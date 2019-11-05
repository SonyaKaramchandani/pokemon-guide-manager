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
            await FetchAndAssert_AreStatusCodeEqual($"{zebraEmailUsersLocatedInEventAreaV2ControllerUrl}?eventId={eventId}", HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task GetZebraEmailUsersLocatedInEventDestinationAreaV2_Success()
        {
            await FetchAndAssert_AreStatusCodeEqual($"{zebraEmailUsersLocatedInEventDestinationAreaV2ControllerUrl}?eventId={eventId}", HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task GetZebraEmailUsersProximalEmail_Success()
        {
            await FetchAndAssert_AreStatusCodeEqual($"{zebraEmailUsersProximalEmailControllerUrl}?eventId={eventId}", HttpStatusCode.OK);
        }
        
        //[Ignore]
        [TestMethod]
        public async Task GetZebraEmailUsersWeeklyEmail_Success()
        {
            await FetchAndAssert_AreStatusCodeEqual($"{ZebraEmailUsersWeeklyEmailControllerUrl}?eventId={eventId}", HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task Get_Trigger_Multiple_Event_Emails_Success()
        {
            List<Task> tasks = new List<Task>();
            eventIds.ForEach(eventId =>
            {
                tasks.Add(FetchAndAssert_AreStatusCodeEqual($"{zebraEmailUsersLocatedInEventAreaV2ControllerUrl}?eventId={eventId}", HttpStatusCode.OK));
                tasks.Add(FetchAndAssert_AreStatusCodeEqual($"{zebraEmailUsersLocatedInEventDestinationAreaV2ControllerUrl}?eventId={eventId}", HttpStatusCode.OK));
                tasks.Add(FetchAndAssert_AreStatusCodeEqual($"{zebraEmailUsersProximalEmailControllerUrl}?eventId={eventId}", HttpStatusCode.OK));
            });
            Task.WaitAll(tasks.ToArray(), TimeSpan.FromSeconds(240));
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _testingHttpClient.Dispose();
        }
    }
}
