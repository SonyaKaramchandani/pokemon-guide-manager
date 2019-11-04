using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Biod.Solution.IntegrationTest
{
    /// <summary>
    /// Integration tests Http client
    /// </summary>
    public class TestingHttpClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        public TestingHttpClient(HttpClient httpClient)
        {
            var timeoutInSeconds = int.Parse(ConfigurationManager.AppSettings["httpClient.TimeoutSeconds"]);

            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
        }
        /// <summary>
        /// Get Api response with string contants
        /// </summary>
        /// <param name="url">Request url</param>
        /// <returns>Api Response with the body contents as string</returns>
        public async Task<ApiResponse> GetAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);

            return new ApiResponse()
            {
                IsSuccessStatusCode = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode,
                StatusDescription = response.ReasonPhrase,
                Content = await response.Content.ReadAsStringAsync()
            };
        }
        /// <summary>
        /// Get an image from a url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<Bitmap> GetImageAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                var memStream = new MemoryStream();
                await stream.CopyToAsync(memStream);
                memStream.Position = 0;
                return new Bitmap(memStream);
            }
        }
        public void Dispose()
        {// TODO: Consider using HttpClientFactory to implement resilient requests and handle lifecycle retry policy ..etc.
            _httpClient.Dispose();
        }
    }
}
