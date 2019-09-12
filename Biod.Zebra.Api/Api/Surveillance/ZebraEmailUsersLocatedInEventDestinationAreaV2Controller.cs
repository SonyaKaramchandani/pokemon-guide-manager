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
    /// This API is to email Zebra users about a threat of an event that is highly connected to their location.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ZebraEmailUsersLocatedInEventDestinationAreaV2Controller : BaseApiController
    {
        // GET api/values
        /// <summary>
        /// Find the users that are in the event area with the event information and send them email with the event details and a link to the event page
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <returns></returns>
        //[System.Web.Http.HttpGet]
        //[System.Web.Http.AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<HttpResponseMessage> Get(int eventId)
        {
            try
            {
                List<NotificationViewModel> notificationList = EventInfoViewModel.GetNotificationViewModelList(DbContext, eventId, false);
                await new NotificationHelper(DbContext).SendZebraNotifications(notificationList);

                Logger.Info($"Successfully processed and sent {notificationList.Count} importation risk event emails for event ID {eventId}");
                return CustomResponseHandler.GetHttpResponse(true, "Success");
            }
            catch (Exception e)
            {
                Logger.Error($"An error occurred while sending Importation Risk Event Email.", e);
                return CustomResponseHandler.GetHttpResponse(false, e.Message + Environment.NewLine + e.InnerException);
            }
        }
    }
}