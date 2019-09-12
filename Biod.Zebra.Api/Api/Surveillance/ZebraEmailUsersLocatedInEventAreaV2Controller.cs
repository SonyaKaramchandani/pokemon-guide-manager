using Biod.Zebra.Api.Api;
using Biod.Zebra.Library.Infrastructures;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Biod.Zebra.Library.Infrastructures.Notification;
using Biod.Zebra.Library.Models.Notification;
using System.Threading.Tasks;

namespace Biod.Zebra.Api.Surveillance
{
    /// <summary>
    /// This API is to email Zebra users about and event in their local area.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ZebraEmailUsersLocatedInEventAreaV2Controller : BaseApiController
    {
        //private readonly BiodApiEntities _zebraContext;
        // GET api/values
        /// <summary>
        /// Find the users that are in the event area destination with the event information and send them email with the event details and a link to the event page.
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
                List<NotificationViewModel> notificationList = EventInfoViewModel.GetNotificationViewModelList(DbContext, eventId, true);
                await new NotificationHelper(DbContext).SendZebraNotifications(notificationList);

                Logger.Info($"Successfully processed and sent {notificationList.Count} local spread event emails for event ID {eventId}");
                return CustomResponseHandler.GetHttpResponse(true, "Success");
            }
            catch (Exception e)
            {
                Logger.Error($"An error occurred while sending Local Spread Event Email.", e);
                return CustomResponseHandler.GetHttpResponse(false, e.Message + Environment.NewLine + e.InnerException);
            }
        }
    }
}