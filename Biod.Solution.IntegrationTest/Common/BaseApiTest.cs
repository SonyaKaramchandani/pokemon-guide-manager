using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace Biod.Solution.IntegrationTest.Common
{
    public class BaseApiTest
    {
        protected static TestingHttpClient _testingHttpClient;

        protected async Task FetchAndAssert_IsExpectedJsonResult(string apiEndpointUrl, string expectedOutputFileName)
        {
            var expectedOutputString = File.ReadAllText(Path.Combine("GeorgeApi", "ExpectedOutput", expectedOutputFileName));
            var expectedJson = JObject.Parse(expectedOutputString);

            var actualResponse = await _testingHttpClient.GetAsync(apiEndpointUrl);
            Assert.IsTrue(actualResponse.IsSuccessStatusCode, $"Api request {apiEndpointUrl} sent response: {actualResponse.StatusCode} {actualResponse.StatusDescription}");

            var actualJsonString = actualResponse.Content;
            // TODO: consider other content types since these tests assumes json always
            var actualJson = JObject.Parse(actualJsonString);
            
            Assert.IsTrue(
                JToken.DeepEquals(expectedJson, actualJson),
                $"Actual Output Json:{Environment.NewLine}{actualJson.ToString()}");
        }

        protected async Task FetchAndAssert_AreStatusCodeEqual(string url, HttpStatusCode expectedStatusCode)
        {
            var actualResponse = await _testingHttpClient.GetAsync(url);

            Assert.AreEqual(
                expectedStatusCode,
                actualResponse.StatusCode
            );
        }
    }
}
