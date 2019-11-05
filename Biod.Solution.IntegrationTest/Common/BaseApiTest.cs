using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace Biod.Solution.IntegrationTest.Common
{
    public abstract class BaseApiTest
    {
        const string EXPECTED_OUTPUT_FOLDER_NAME = "ExpectedOutput";
        /// <summary>
        /// The name of the Api to be tested it must match the test data folder name 
        /// </summary>
        protected abstract string TestApiName { get; }
       /// <summary>
       /// The base address of the Api
       /// </summary>
        protected abstract string ApiBaseUrl { get; }

        protected static TestingHttpClient _testingHttpClient;
        protected async Task FetchAndAssert_IsExpectedJsonResult(string apiEndpointUrl, string expectedOutputFileName)
        {
            var expectedOutputString = GetExpectedOutputText(expectedOutputFileName);
            var expectedJson = JObject.Parse(expectedOutputString);
            
            apiEndpointUrl = $"{ApiBaseUrl}{apiEndpointUrl}";
            var actualResponse = await _testingHttpClient.GetAsync(apiEndpointUrl);
            Assert.IsTrue(actualResponse.IsSuccessStatusCode, $"Api request {apiEndpointUrl} sent response: {actualResponse.StatusCode} {actualResponse.StatusDescription}");

            var actualJsonString = actualResponse.Content;
            // TODO: consider other content types since these tests assumes json always
            var actualJson = JObject.Parse(actualJsonString);

            Assert.IsTrue(
                JToken.DeepEquals(expectedJson, actualJson),
                $"Actual Output Json:{Environment.NewLine}{actualJson.ToString()}");
        }

        protected async Task FetchAndAssert_AreStatusCodeEqual(string apiEndpointUrl, HttpStatusCode expectedStatusCode)
        {
            var actualResponse = await _testingHttpClient.GetAsync($"{ApiBaseUrl}{apiEndpointUrl}");

            Assert.AreEqual(
                expectedStatusCode,
                actualResponse.StatusCode
            );
        }
        private string GetExpectedOutputText(string expectedOutputFileName)
        {
            var expectedOutputText = File.ReadAllText(
                Path.Combine(TestApiName, EXPECTED_OUTPUT_FOLDER_NAME, expectedOutputFileName)
                );
            //transform the text 
            string baseUrlKey = "{Template.ApiBaseUrl}";
            if (expectedOutputText.Contains(baseUrlKey))
            {
                expectedOutputText = expectedOutputText.Replace(baseUrlKey, ApiBaseUrl);
            }
            return expectedOutputText;
        }
    }
}
