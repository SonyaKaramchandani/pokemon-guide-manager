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
    /// Symptoms Controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class SymptomsController : ApiController
    {
        /// <summary>Get symptoms for all diseases.</summary>
        /// <param name="context">Optional string to specify how the results will be used.</param>
        [Route("symptoms", Name="GetSymptoms")]
        public IHttpActionResult GetSymptoms(string context = "web")
        {
            try
            {
                using (var db = new DiseasesModel(true))
                {
                    return Ok(new { symptoms = db.Symptoms.ToList().ConvertAll(s => s.GetProjection()),
                                    cacheTag = Globals.GetCacheTag(db) });
                }
            }
            catch (Exception e)
            {
                Trace.TraceWarning("SymptomsController::GetSymptoms() - Caught unexpected exception:  ", e);
            }
            return NotFound();
        }


        /// <summary>Get information about one or more symptoms given their ids.</summary>
        /// <param name="id">One or more ids of the symptoms whose info to get.</param>
        /// <param name="context">Optional string to specify how the results will be used.</param>
        [Route("symptom", Name="GetSymptomsByIds")]
        public IHttpActionResult GetSymptomsByIds([FromUri] int[] id, string context = "web")
        {
            if (id.Length > 256)        // TODO:  Hardcoded upper bound to avoid unnecessary db queries, but should probably just query once and cache.
                return BadRequest("Too many symptom ids specified.");
            if (id.Length <= 0)
                return BadRequest("Must specify at least one symptom id.");
            using (var db = new DiseasesModel(id.Length != 1))
            {
                List<Symptom.Projection> result = new List<Symptom.Projection>();
                for (int i = 0;  i < id.Length;  ++i)
                {
                    try
                    {
                        result.Add(db.Symptoms.Find(id[i]).GetProjection());
                    }
                    catch (Exception e)
                    {
                        Trace.TraceWarning("SymptomsController::GetSymptomsByIds() - Caught unexpected exception:  ", e);
                    }
                } // for
                if (result.Count() > 0)
                    return Ok(new { symptoms = result, cacheTag = Globals.GetCacheTag(db) });
                else
                    return NotFound();
            }
        } // GetSymptomsByIds()


        /// <summary>Get symptom categories.</summary>
        /// <param name="context">Optional string to specify how the results will be used.</param>
        [Route("systems", Name="GetSymptomCategories")]
        public IHttpActionResult GetSymptomCategories(string context = "web")
        {
            try
            {
                using (var db = new DiseasesModel(true))
                {
                    return Ok(new { systems = db.SymptomCategories.ToList().ConvertAll(c => c.GetProjection()),
                                    cacheTag = Globals.GetCacheTag(db) });
                }
            }
            catch (Exception e)
            {
                Trace.TraceWarning("SymptomsController::GetSymptomCategories() - Caught unexpected exception:  ", e);
            }
            return NotFound();
        } // GetSymptomCategories()


    }
}
