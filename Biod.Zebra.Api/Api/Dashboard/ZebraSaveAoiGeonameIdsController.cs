using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.EntityModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using Biod.Zebra.Api.Api;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using Biod.Zebra.Library.Infrastructures;

namespace Biod.Zebra.Api.LocalFeed
{
    public class ZebraSaveAoiGeonameIdsController : BaseApiController
    {
        /// <summary>Set AOI Geoname identifiers by authenticated user.</summary>
        /// <param name="geonameIds"></param>
        /// <returns>EventsInfoViewModel</returns>
        public async Task<HttpResponseMessage> GetAsync(string userId, string geonameIds = "")
        {
            var user = UserManager.FindById(userId);

            if (user == null)
            {
                Logger.Warning($"Geoname IDs not saved: User with user ID {userId} not found");
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            user.AoiGeonameIds = geonameIds;
            var result = await UserManager.UpdateAsync(user);
            AccountHelper.PrecalculateRisk(userId);

            Logger.Info($"Successfully saved Geoname IDs for user ID {userId}");
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}