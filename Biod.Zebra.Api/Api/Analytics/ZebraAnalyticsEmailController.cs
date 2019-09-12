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
    public class ZebraAnalyticsEmailController : ZebraAnalyticsApiController
    {
        // GET api/ZebraAnalytics/Email
        /// <summary>
        /// Gets the list of emails for a specific user ID or Email, email Type, or in a specific date range if parameters are provided.
        /// </summary>
        /// <remarks>
        /// Date formats can be in the form of: yyyy, yyyy-MM, yyyy-MM-dd, yyyy-MM-ddTHH:mm:ss<br />
        /// For start date, any unfilled units will default to the earliest time possible (e.g. 2019 will become 2019-01-01T00:00:00)<br />
        /// For end date, any unfilled units will default to the latest time possible (e.g. 2019 will become 2019-12-31T23:59:59)
        /// </remarks>
        /// <param name="userId">the user id</param>
        /// <param name="email">the user email</param>
        /// <param name="emailType">the email type</param>
        /// <param name="startDate">the start date</param>
        /// <param name="endDate">the end date</param>
        /// <returns>the list of emails for the given query</returns>
        [Route("Email")]
        [HttpGet]
        public HttpResponseMessage ZebraAnalyticsGetEmail(
            string userId = null,
            string email = null,
            int? emailType = null,
            string startDate = null,
            string endDate = null
        )
        {
            try
            {
                var result = DbContext.usp_ZebraAnalyticsGetUserEmailNotification(
                    userId, 
                    email, 
                    emailType, 
                    DateHelper.ParseStartDateString(startDate), 
                    DateHelper.ParseEndDateString(endDate))
                    .Select(e => new ZebraAnalyticsGetEmailModel()
                    {
                        Id = e.Id,
                        Email = e.UserEmail,
                        UserId = e.UserId,
                        SentDate = e.SentDate,
                        AoiGeonameIds = e.AoiGeonameIds,
                        EmailType = e.EmailType,
                        EventId = e.EventId
                    })
                    .ToList();

                Logger.Info($"Successfully returned email details for email id");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to return email details for email id", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/ZebraAnalytics/EmailContent
        /// <summary>
        /// Gets the body content of the Email given an email id
        /// </summary>
        /// <param name="emailId">the email id</param>
        /// <returns>the body content of the email</returns>
        [HttpGet]
        [Route("EmailContent")]
        public HttpResponseMessage ZebraAnalyticsGetEmailContentById(int emailId)
        {
            try
            {
                var emailResult = DbContext.UserEmailNotifications.FirstOrDefault(e => e.Id == emailId);

                if (emailResult == null)
                {
                    Logger.Warning($"Email details for email id {emailId} not found");
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                var result = new ZebraAnalyticsGetEmailContentModel()
                {
                    Id = emailResult.Id,
                    Content = emailResult.Content
                };

                Logger.Info($"Successfully returned email details for email id {emailId}");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to return email details for email id {emailId}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
