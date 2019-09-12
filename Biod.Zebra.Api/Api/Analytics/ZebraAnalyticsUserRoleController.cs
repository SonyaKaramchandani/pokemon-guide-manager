using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Biod.Zebra.Api.Analytics;
using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.Models.Analytics;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Biod.Zebra.Api.Api.Analytics
{
    [RoutePrefix("api/ZebraAnalytics")]
    public class ZebraAnalyticsUserRoleController : ZebraAnalyticsApiController
    {
        public RoleManager<IdentityRole> LocalRoleManager { get; set; } = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

        // GET api/ZebraAnalytics/UserRole
        /// <summary>
        /// Gets the list of available User Roles
        /// </summary>
        /// <returns>the list of roles</returns>
        [Route("UserRole")]
        [HttpGet]
        public HttpResponseMessage ZebraAnalyticsGetUserRole()
        {
            try
            {
                var result = LocalRoleManager.Roles
                    .Select(r => new ZebraAnalyticsGetUserRoleModel() {
                        Id = r.Id,
                        Role = r.Name
                    })
                    .OrderBy(r => r.Role)
                    .ToList();

                Logger.Info("Successfully returned list of User Roles");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to return list of User Roles", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && LocalRoleManager != null)
            {
                LocalRoleManager.Dispose();
                LocalRoleManager = null;
            }

            base.Dispose(disposing);
        }
    }
}
