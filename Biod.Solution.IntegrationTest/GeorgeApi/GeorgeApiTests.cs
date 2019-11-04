using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Biod.Solution.IntegrationTest.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Biod.Solution.IntegrationTest.GeorgeApi
{
    [TestClass]
    public class GeorgeApiTests : BaseApiTest
    {
        private static string baseUrl;
        private static string statusUrl;
        private static string diseaseUrl;
        private static string locationUrl;
        private static string risksUrl;
        
        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            baseUrl = ConfigurationManager.AppSettings["url.GeorgeApi"];
            statusUrl = "/status";
            diseaseUrl = "/disease";
            locationUrl = "/location";
            risksUrl = "/risks";
            _testingHttpClient = new TestingHttpClient(new HttpClient()); //one http client per test class
        }
        #region Status
        [TestMethod]
        public async Task GetStatus_Success()
        {
           await FetchAndAssert_IsExpectedJsonResult(baseUrl + statusUrl, "GetStatus_Success.json");
        }
        #endregion
        #region Disease
        [TestMethod]
        public async Task GetDisease_Success()
        {
            await FetchAndAssert_IsExpectedJsonResult($"{baseUrl}{diseaseUrl}?id=2&modifiedSince=110188884&context=testing", "GetDisease_Success.json");
        }
        [TestMethod]
        public async Task GetDisease_NotFound()
        {
            await FetchAndAssert_IsExpectedJsonResult($"{baseUrl}{diseaseUrl}?id=0&modifiedSince=110188884&context=testing", "GetDisease_NotFound.json");
        }

        #endregion
        #region Location
        [TestMethod]
        public async Task GetLocation_Risks_Success()
        {
            await FetchAndAssert_IsExpectedJsonResult(
                $"{baseUrl}{locationUrl}{risksUrl}?latitude=-33.45694&longitude=-70.64827&context=testing",
                "GetLocation_Risks_Success.json");
        }
        [TestMethod]
        public async Task GetLocation_Risks_Invalid_Geolocation()
        {
            await FetchAndAssert_AreStatusCodeEqual(
                $"{baseUrl}{locationUrl}{risksUrl}?latitude=-11111111&longitude=-1111111&context=testing",
                HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public async Task GetLocation_Risks_Invalid_Lat()
        {
            await FetchAndAssert_AreStatusCodeEqual(
                $"{baseUrl}{locationUrl}{risksUrl}?latitude=-11111111&longitude=-70.64827&context=testing",
                HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public async Task GetLocation_Risks_Invalid_Long()
        {
            await FetchAndAssert_AreStatusCodeEqual(
                $"{baseUrl}{locationUrl}{risksUrl}?latitude=-33.45694&longitude=-1111111&context=testing",
                HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public async Task GetRisksbygeonameId_Success()
        {
            await FetchAndAssert_IsExpectedJsonResult(
               $"{baseUrl}{locationUrl}/risksbygeonameId?geonameId=6252001",
               "GetRisksbygeonameId_Success.json");
        }
        [TestMethod]
        public async Task GetRisksbygeonameId_NotFound()
        {
            await FetchAndAssert_AreStatusCodeEqual(
               $"{baseUrl}{locationUrl}/risksbygeonameId?geonameId=unknown",
               HttpStatusCode.NotFound);
        }
        [TestMethod]
        public async Task GetRiskswithtier_Success()
        {
            await FetchAndAssert_IsExpectedJsonResult(
               $"{baseUrl}{locationUrl}/riskswithtier?latitude=10&longitude=10&tier=1",
               "GetRiskswithTier_Success.json");
        }
        [TestMethod]
        public async Task GetRiskswithtier_Invalid_Geolocation()
        {
            await FetchAndAssert_AreStatusCodeEqual(
               $"{baseUrl}{locationUrl}/riskswithtier?latitude=1111111&longitude=11111111&tier=1",
               HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public async Task GetRiskswithtier_Invalid_Lat()
        {
            await FetchAndAssert_AreStatusCodeEqual(
               $"{baseUrl}{locationUrl}/riskswithtier?latitude=1111111&longitude=10&tier=1",
               HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public async Task GetRiskswithtier_Invalid_Long()
        {
            await FetchAndAssert_AreStatusCodeEqual(
               $"{baseUrl}{locationUrl}/riskswithtier?latitude=10&longitude=11111111&tier=1",
               HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public async Task GetRiskswithtier_Invalid_Tier()
        {
            await FetchAndAssert_AreStatusCodeEqual(
               $"{baseUrl}{locationUrl}/riskswithtier?latitude=10&longitude=10&tier=invalid",
               HttpStatusCode.BadRequest);
        }
        #endregion
        #region Image
        [TestMethod]
        public async Task GetImage_Success()
        {
            var img = await _testingHttpClient
                     .GetImageAsync($"{baseUrl}{locationUrl}/image?context=testing&latitude=54.99721&longitude=-7.30917&percentFromTop=0.3823529&pixelHeight=850&pixelWidth=800&transparent=false");
            Assert.IsNotNull(img);
            Assert.AreEqual<int>(850, img.Height, "Invalid image height");
            Assert.AreEqual<int>(800, img.Width, "Invalid image width");
        }
        [TestMethod]
        public async Task GetImage_Invalid_Geolocation()
        {
            await FetchAndAssert_AreStatusCodeEqual(
               $"{baseUrl}{locationUrl}/image?context=testing&latitude=111111&longitude=1111111&percentFromTop=0.3823529&pixelHeight=850&pixelWidth=800&transparent=false",
               HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public async Task GetImage_Invalid_Lat()
        {
            await FetchAndAssert_AreStatusCodeEqual(
               $"{baseUrl}{locationUrl}/image?context=testing&latitude=111111&longitude=7.30917&percentFromTop=0.3823529&pixelHeight=850&pixelWidth=800&transparent=false",
               HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public async Task GetImage_Invalid_Long()
        {
            await FetchAndAssert_AreStatusCodeEqual(
               $"{baseUrl}{locationUrl}/image?context=testing&latitude=54.99721&longitude=1111111&percentFromTop=0.3823529&pixelHeight=850&pixelWidth=800&transparent=false",
               HttpStatusCode.BadRequest);
        }
        #endregion
        #region Symptoms
        [TestMethod]
        public async Task GetSystems_Success()
        {
            await FetchAndAssert_IsExpectedJsonResult(
               $"{baseUrl}/systems?context=testing",
               "GetSystems_Success.json");
        }
        #endregion
        [ClassCleanup]
        public static void ClassCleanup()
        {
            _testingHttpClient.Dispose();
        }
    }
}
