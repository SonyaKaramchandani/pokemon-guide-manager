using Biod.Zebra.Library.Infrastructures.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Biod.Zebra.Controllers.api
{
    [TokenAuthentication]
    public class NotificationController : BaseApiController
    {
        [HttpGet]
        [Route("api/notification/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var email = DbContext.UserEmailNotifications.FirstOrDefault(e => e.Id == id && e.UserId == CurrentUserId);
                if (email == null)
                {
                    Logger.Warning($"No user email notification found with the id {id} for user with id {CurrentUserId}");
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(email.Content, System.Text.Encoding.UTF8, "text/html");

                Logger.Info($"Successfully returned user email notification content with id {id} for user with id {CurrentUserId}");
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to retrieve content body for user email notification with id {id} for user with id {CurrentUserId}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
