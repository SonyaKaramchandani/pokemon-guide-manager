using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Biod.Zebra.Api.Analytics;
using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.Models.Analytics;

namespace Biod.Zebra.Api.Api.Analytics
{
    [RoutePrefix("api/ZebraAnalytics")]
    public class ZebraAnalyticsUserLoginHistoryController : ZebraAnalyticsApiController
    {
        // GET api/ZebraAnalytics/UserLoginHistory
        /// <summary>
        /// Gets the list of user logins for a specific user ID or in a specific date range if parameters are provided.
        /// </summary>
        /// <remarks>
        /// Date formats can be in the form of: yyyy, yyyy-MM, yyyy-MM-dd, yyyy-MM-ddTHH:mm:ss<br />
        /// For start date, any unfilled units will default to the earliest time possible (e.g. 2019 will become 2019-01-01T00:00:00)<br />
        /// For end date, any unfilled units will default to the latest time possible (e.g. 2019 will become 2019-12-31T23:59:59)
        /// </remarks>
        /// <param name="userId">the user id</param>
        /// <param name="startDate">the start date</param>
        /// <param name="endDate">the end date</param>
        /// <returns>the list of logins for the given query</returns>
        [Route("UserLoginHistory")]
        [HttpGet]
        public HttpResponseMessage ZebraAnalyticsGetUserLoginHistory(string userId = null, string startDate = null, string endDate = null)
        {
            try
            {
                var result = DbContext.usp_ZebraAnalyticsGetUserLogin(userId, DateHelper.ParseStartDateString(startDate), DateHelper.ParseEndDateString(endDate))
                    .Select(ul => new ZebraAnalyticsGetUserLoginHistoryModel()
                    {
                        UserId = ul.UserId,
                        LoginDate = ul.LoginDateTime
                    })
                    .ToList();

                Logger.Info($"Successfully returned user login details for user id {userId}, from startDate {startDate} to endDate {endDate}");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to return user login details for user id {userId}, from startDate {startDate} to endDate {endDate}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
