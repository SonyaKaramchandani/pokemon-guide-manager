using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Biod.Zebra.Api.Analytics;
using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.Models.Analytics;

namespace Biod.Zebra.Api.Api.Analytics
{
    [RoutePrefix("api/ZebraAnalytics")]
    public class ZebraAnalyticsUserController : ZebraAnalyticsApiController
    {
        // GET api/ZebraAnalytics/User
        /// <summary>
        /// Gets the latest profile information for a given user
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <returns>the profile information</returns>
        [Route("User")]
        [HttpGet]
        public HttpResponseMessage ZebraAnalyticsGetUserById(string userId)
        {
            try
            {
                var userResult = UserManager.Users.FirstOrDefault(u => u.Id == userId);

                if (userResult == null)
                {
                    Logger.Warning($"User details for user id {userId} not found");
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                var result = new ZebraAnalyticsGetUserModel()
                {
                    Id = userResult.Id,
                    FirstName = userResult.FirstName,
                    LastName = userResult.LastName,
                    UserName = userResult.UserName,
                    GroupId = userResult.UserGroupId,
                    AoiGeonameIds = userResult.AoiGeonameIds?.Split(',').Select(int.Parse).ToArray() ?? new int[0],
                    Location = userResult.Location,
                    LocationGeonameId = userResult.GeonameId,
                    FirstLoginDate = DbContext.usp_GetFirstLoginDateByUser(userResult.Id).FirstOrDefault()?.FirstLoginDate,
                    LastModifiedDate = DbContext.usp_ZebraAnalyticsGetUserLastModifiedDate(userResult.Id).FirstOrDefault()?.ModifiedDate,
                    NotificationSettings = new[]
                    {
                        new NotificationSettingViewModel() { NotificationType = nameof(userResult.NewCaseNotificationEnabled), IsEnabled = userResult.NewCaseNotificationEnabled },
                        new NotificationSettingViewModel() { NotificationType = nameof(userResult.NewOutbreakNotificationEnabled), IsEnabled = userResult.NewOutbreakNotificationEnabled },
                        new NotificationSettingViewModel() { NotificationType = nameof(userResult.PeriodicNotificationEnabled), IsEnabled = userResult.PeriodicNotificationEnabled },
                        new NotificationSettingViewModel() { NotificationType = nameof(userResult.WeeklyOutbreakNotificationEnabled), IsEnabled = userResult.WeeklyOutbreakNotificationEnabled }
                    },
                    Organization = userResult.Organization,
                    Roles = userResult.Roles?.Select(r => r.RoleId).ToArray() ?? new string[0]
                };

                Logger.Info($"Successfully returned user details for user id {userId}");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to return user details for user id {userId}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
