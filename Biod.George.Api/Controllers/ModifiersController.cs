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
    /// Modifiers Controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ModifiersController : ApiController
    {
        /// <summary>Get risk and severity personalization/customization modifiers.</summary>
        /// <param name="context">Optional string to specify how the results will be used.</param>
        [Route("modifiers")]
        public IHttpActionResult GetModifiers(string context = "web")
        {
            try
            {
                using (var db = new DiseasesModel(true))
                {
                    return Ok(new { modifierGroups = db.ModifierCategories.ToList().ConvertAll(c => c.getProjection()),
                                    cacheTag = Globals.GetCacheTag(db) });
                }
            }
            catch (Exception e)
            {
                Trace.TraceWarning("ModifiersController::GetModifiers() - Caught unexpected exception:  ", e);
            }
            return NotFound();
        }
    }
}
