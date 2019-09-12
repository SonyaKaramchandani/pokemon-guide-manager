using Biod.Zebra.Library.Infrastructures;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Biod.Zebra.Api.Api;
using Biod.Zebra.Library.Infrastructures.Notification;
using Biod.Zebra.Library.Models.Notification;
using System.Linq;
using System.Threading.Tasks;

namespace Biod.Zebra.Api.Surveillance
{
    /// <summary>
    /// This API is to email Zebra users their weekly brief
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ZebraEmailUsersWeeklyEmailController : BaseApiController
    {
        /// <summary>
        /// Group the events to be sent to the users by user, and send them email with the weekly brief
        /// </summary>
        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                List<NotificationViewModel> notificationList = WeeklyViewModel.GetNotificationViewModelList(DbContext, UserManager);
                await new NotificationHelper(DbContext).SendZebraNotifications(notificationList);

                Logger.Info($"Successfully processed and sent {notificationList.Count} weekly emails.");
                return CustomResponseHandler.GetHttpResponse(true, "Success");
            }
            catch (Exception e)
            {
                Logger.Error($"An error occurred while sending Weekly Email.", e);
                return CustomResponseHandler.GetHttpResponse(false, e.Message + Environment.NewLine + e.InnerException);
            }
        }
    }
}