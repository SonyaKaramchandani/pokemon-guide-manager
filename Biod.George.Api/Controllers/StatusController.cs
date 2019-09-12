using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlueDot.DiseasesAPI;
using BlueDot.DiseasesAPI.Models;


namespace BlueDot.DiseasesAPI.Controllers
{
    /// <summary>
    /// Status Controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class StatusController : ApiController
    {
        /// <summary>Check the status of the API.</summary>
        /// <returns>cacheTag integer indicating the latest model version.</returns>
        [Route("status")]
        public IHttpActionResult GetStatus()
        {
            try
            {
                using (var db = new DiseasesModel())
                    return Ok(new { cacheTag = Globals.GetCacheTag(db) });
            }
            catch (Exception e)
            {
                Trace.TraceWarning("StatusController::GetStatus() - Caught unexpected exception:  ", e);
            }
            return NotFound();
        }
    }
}
