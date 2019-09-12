using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Biod.Zebra.Api.Analytics;
using Biod.Zebra.Library.Models.Analytics;

namespace Biod.Zebra.Api.Api.Analytics
{
    [RoutePrefix("api/ZebraAnalytics")]
    public class ZebraAnalyticsEmailTypeController : ZebraAnalyticsApiController
    {
        // GET api/ZebraAnalytics/EmailType
        /// <summary>
        /// Gets the list of available Email Types
        /// </summary>
        /// <returns>the list of email types</returns>
        [Route("EmailType")]
        [HttpGet]
        public HttpResponseMessage ZebraAnalyticsGetEmailType()
        {
            try
            {
                var result = DbContext.UserEmailTypes
                    .Select(e => new ZebraAnalyticsGetEmailTypeModel() {
                        Id = e.Id,
                        Type = e.Type
                    })
                    .OrderBy(e => e.Id)
                    .ToList();

                Logger.Info("Successfully returned list of Email Types");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to return list of Email Types", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
