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
    public class ZebraAnalyticsUserGroupController : ZebraAnalyticsApiController
    {
        // GET api/ZebraAnalytics/UserGroup
        /// <summary>
        /// Gets the list of available User Groups
        /// </summary>
        /// <returns>the list of user groups</returns>
        [Route("UserGroup")]
        [HttpGet]
        public HttpResponseMessage ZebraAnalyticsGetUserGroup()
        {
            try
            {
                var result = DbContext.UserGroups
                    .Select(e => new ZebraAnalyticsGetUserGroupModel() {
                        Id = e.Id,
                        Name = e.Name
                    })
                    .OrderBy(e => e.Id)
                    .ToList();

                Logger.Info("Successfully returned list of User Groups");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to return list of User Groups", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
