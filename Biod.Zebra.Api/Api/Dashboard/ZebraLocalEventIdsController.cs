using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.EntityModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Data.Entity.Core.Objects;
using Biod.Zebra.Library;
using Newtonsoft.Json;
using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Api.Api;
using System.Net.Http;
using System.Net;

namespace Biod.Zebra.Api.LocalFeed
{
    public class ZebraLocalEventIdsController : BaseApiController
    {
        /// <summary>Get local events by user identifier.</summary>
        /// <param name="userId"></param>
        /// <returns>string</returns>
        public HttpResponseMessage Get(string userId)
        {
            try
            {
                string userProfileResult = JsonStringResultClass.GetJsonStringResultAsync(
                ConfigurationManager.AppSettings.Get("ZebraApiBaseUrl"),
                "/api/ZebraUserProfile?userId=" + userId,
                ConfigurationManager.AppSettings.Get(@"ZebraApiUserName"),
                ConfigurationManager.AppSettings.Get("ZebraApiPassword")).Result;

                if (string.IsNullOrEmpty(userProfileResult))
                {
                    Logger.Warning($"Failed to retrieve User Profile information for user ID {userId}");
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

                UserProfileDto userProfile = JsonConvert.DeserializeObject<UserProfileDto>(userProfileResult);
                var result = DbContext.usp_ZebraApiGetEventByGeonameId(userProfile.PersonalDetails.GeonameId, Convert.ToInt32(ConfigurationManager.AppSettings.Get("EventDistanceBuffer")))
                    .Select(x => x.EventId)
                    .ToList();

                Logger.Info($"Successfully returned local events for user ID {userId}");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to get local events for user ID {userId}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}