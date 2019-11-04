using System.Net;

namespace Biod.Solution.IntegrationTest
{
    /// <summary>
    /// Testing Api Response
    /// </summary>
    public class ApiResponse
    {
        public string Content { get; set; }
        public string StatusDescription { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
        public string ContentEncoding { get; set; }
        public string RequestMethod { get; set; }
        public WebHeaderCollection Headers { get; set; }
        public bool IsSuccessStatusCode { get; set; }
    }
}
