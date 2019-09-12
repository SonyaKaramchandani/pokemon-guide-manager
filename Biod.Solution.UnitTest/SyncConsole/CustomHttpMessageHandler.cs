using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Biod.Solution.UnitTest.SyncConsole
{
    public static class CustomHttpMessageHandler
    {
        /// <summary>
        /// Mocked client that will always return success 200 response
        /// </summary>
        public class SuccessHandler : HttpMessageHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                var task = new TaskCompletionSource<HttpResponseMessage>();
                task.SetResult(response);

                return task.Task;
            }
        }

        /// <summary>
        /// Mocked client that will always return failure 400 response
        /// </summary>
        public class FailureHandler : HttpMessageHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                var task = new TaskCompletionSource<HttpResponseMessage>();
                task.SetResult(response);

                return task.Task;
            }
        }

        /// <summary>
        /// Mocked client that will always return null response
        /// </summary>
        public class NullHandler : HttpMessageHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var task = new TaskCompletionSource<HttpResponseMessage>();
                task.SetResult(null);

                return task.Task;
            }
        }
    }
}
