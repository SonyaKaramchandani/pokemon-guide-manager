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
using System.Net.Http;
using Biod.Zebra.Api.Api;
using System.Net;
using Biod.Zebra.Library.Infrastructures;
using System.Threading.Tasks;

namespace Biod.Zebra.Api.LocalFeed
{
    public class ZebraOnboardingController : BaseApiController
    {
        /// <summary>Updates user onboarding completion status</summary>
        /// <param name="userId"></param>
        /// <param name="isComplete"></param>
        /// <returns>IdentityResult.</returns>
        public async Task<HttpResponseMessage> Get(string userId, bool isComplete)
        {
            try {
                var user = UserManager.FindById(userId);
                user.OnboardingCompleted = isComplete;
                var result = await UserManager.UpdateAsync(user);

                Logger.Info($"Successfully updated onboarding completion for user ID {userId}");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to update onboarding completion for user ID {userId}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}