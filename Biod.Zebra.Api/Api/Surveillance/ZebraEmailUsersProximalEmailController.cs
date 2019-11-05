using Biod.Zebra.Library.Infrastructures;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Biod.Zebra.Api.Api;
using Biod.Zebra.Library.Infrastructures.Notification;
using Biod.Zebra.Library.Models.Notification;
using System.Threading.Tasks;

namespace Biod.Zebra.Api.Surveillance
{
    /// <summary>
    /// This API is to email Zebra users proximal email
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ZebraEmailUsersProximalEmailController : BaseApiController
    {
        /// <summary>
        /// For the provided event, check if there are changes and send the email if there are accordingly
        /// </summary>
        /// <param name="eventId">the event id</param>
        public async Task<HttpResponseMessage> Get(int eventId)
        {
            try
            {
                List<NotificationViewModel> notificationList = ProximalViewModel.GetNotificationViewModelList(DbContext, UserManager, eventId);
                await new NotificationHelper(NotificationDependencyFactory).SendZebraNotifications(notificationList);

                Logger.Info($"Successfully processed and sent {notificationList.Count} proximal emails for event ID {eventId}");
                return CustomResponseHandler.GetHttpResponse(true, "Success");
            }
            catch (Exception e)
            {
                Logger.Error($"An error occurred while sending Proximal Email.", e);
                return CustomResponseHandler.GetHttpResponse(false, e.Message + Environment.NewLine + e.InnerException);
            }
        }
    }
}