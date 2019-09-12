using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Biod.Surveillance.Zebra.SyncConsole.EntityModels;
using Biod.Surveillance.Zebra.SyncConsole;
using Biod.Surveillance.Zebra.SyncConsole.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Biod.Solution.UnitTest.SyncConsole
{
    /// <summary>
    /// Tests the SendEventUpdate method in the SyncConsole program
    /// </summary>
    [TestClass]
    public class SendEventUpdateTest
    {
        private readonly HttpClient successHttpClient = new HttpClient(new CustomHttpMessageHandler.SuccessHandler());
        private readonly HttpClient failureHttpClient = new HttpClient(new CustomHttpMessageHandler.FailureHandler());
        private readonly HttpClient nullHttpClient = new HttpClient(new CustomHttpMessageHandler.NullHandler());

        [TestInitialize]
        public void Initialize()
        {
            successHttpClient.BaseAddress = new Uri("http://localhost");
            failureHttpClient.BaseAddress = new Uri("http://localhost");
            nullHttpClient.BaseAddress = new Uri("http://localhost");
        }

        /// <summary>
        /// Tests whether the exception is thrown when null is passed for the event model
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "A null model was inappropriately allowed")]
        public async Task NullObject()
        {
            await Program.SendEventUpdate(successHttpClient, null);
        }

        /// <summary>
        /// Tests whether the null response from the HttpClient is returned as is
        /// </summary>
        [TestMethod]
        public async Task NullResponse()
        {
            var response = await Program.SendEventUpdate(nullHttpClient, new EventUpdateModel());

            Assert.IsNull(response, "Null response from the request not returned");
        }

        /// <summary>
        /// Tests whether the success response is returned and the status code is as expected
        /// </summary>
        [TestMethod]
        public async Task SuccessResponse()
        {
            var response = await Program.SendEventUpdate(successHttpClient, new EventUpdateModel());

            Assert.IsNotNull(response, "Successful response from the request not returned");
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK, "Response from the request does not match expected status code");
        }

        /// <summary>
        /// Tests whether a null response is returned if the request was a failure (non-200 status codes)
        /// </summary>
        [TestMethod]
        public async Task FailureResponse()
        {
            var response = await Program.SendEventUpdate(failureHttpClient, new EventUpdateModel());

            Assert.IsNull(response, "Null not returned from a failed request");
        }
    }
}
