using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Http;
using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Infrastructures;
using System.Net.Http;
using System.Net;
using System.Configuration;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using Biod.Zebra.Api.Api;

namespace Biod.Zebra.Api.Surveillance
{
    [AllowAnonymous]
    public class ZebraEmailUpdateWeeklyDataController : BaseApiController
    {
        // POST api/ZebraEmailUpdateWeeklyData
        /// <summary>
        /// Triggers the stored procedure to update the weekly email data
        /// </summary>
        [HttpPost]
        public HttpResponseMessage Post()
        {
            try
            {
                var result = DbContext.usp_ZebraEmailSetWeeklyData();

                if (result?.FirstOrDefault() != 1)
                {
                    Logger.Warning($"Failed to update weekly email data, stored procedure returned {result?.FirstOrDefault()}");
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }

                Logger.Info($"Successfully updated weekly email data");
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to update weekly email data", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }

}