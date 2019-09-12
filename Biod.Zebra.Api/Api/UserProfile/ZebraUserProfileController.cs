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
    public class ZebraUserProfileController : BaseApiController
    {
        /// <summary>Gets user profile data transfer object.</summary>
        /// <param name="UserId"></param>
        /// <returns>UserProfileDto.</returns>
        public async Task<HttpResponseMessage> Get(string UserId)
        {
            try {
                var user = UserManager.FindById(UserId);
                var result = UserProfileDto.GetUserProfileDto(DbContext, user);

                if (!result.OnboardingCompleted)
                {
                    var customRoleFilter = new CustomRolesFilter(DbContext);
                    var userRoleList = await UserManager.GetRolesAsync(user.Id);
                    var role = customRoleFilter.GetFirstPublicRole(userRoleList);
                    var notificationDescription = DbContext.AspNetRoles.Where(x => x.Name == role).SingleOrDefault().NotificationDescription;
                    result.PersonalDetails.Role = role;
                    result.PersonalDetails.RoleNotificationDescription = String.IsNullOrEmpty(notificationDescription) ? String.Empty : notificationDescription;
                }

                Logger.Info($"Successfully returned user profile user ID {UserId}");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to retrieve user profile for userId {UserId}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}