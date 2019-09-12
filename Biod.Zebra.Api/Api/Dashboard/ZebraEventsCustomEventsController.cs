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
using System.Net.Http;
using Biod.Zebra.Api.Api;
using System.Net;

namespace Biod.Zebra.Api.LocalFeed
{
    public class ZebraEventsCustomEventsController : BaseApiController
    {
        /// <summary>Get sorted and grouped filtered events based on the user's custom filter.</summary>
        /// <param name="userId"></param>
        /// <param name="groupType"></param>
        /// <param name="sortType"></param>
        /// <returns>string</returns>
        public HttpResponseMessage Get(
            string userId,
            int groupType = 1,
            string sortType = "LastUpdatedDate")
        {
            try
            {
                var result = new EventsInfoViewModel().CustomGroupSort(
                    userId,
                    groupType,
                    sortType);

                Logger.Info($"Successfully returned custom zebra events for user {userId}");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to return custom zebra events for user {userId}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}