using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlueDot.DiseasesAPI.Models;


namespace BlueDot.DiseasesAPI.Controllers
{
    /// <summary>
    /// Disease Controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class DiseaseController : ApiController
    {
        private IHttpActionResult DoGetDiseases(int[] id, double modifiedSince, string context)
        {
            string cacheTag = "-1";
            DateTime modSinceRefDatum = Globals.referenceDatum.AddSeconds(modifiedSince);
            List<Disease.Projection> result = new List<Disease.Projection>();
            using (var db = new DiseasesModel(id.Length != 1))
            {
                cacheTag = Globals.GetCacheTag(db);
                string query = "SELECT * FROM bd.Disease WHERE lastModified > '" + modSinceRefDatum.ToString("yyyyMMdd HH:mm:ss") + "'";
                if (id.Length > 0)
                    query += " AND diseaseId IN (" + string.Join(",", id) + ")";
                foreach (Disease dd in db.Diseases.SqlQuery(query))
                {
                    try
                    {
                        result.Add(dd.GetProjection());
                    }
                    catch (Exception e)
                    {
                        Trace.TraceWarning("DiseaseController::DoGetDiseases() - Caught unexpected exception:  ", e);
                    }
                } // foreach
            }
            if (result.Count() > 0  ||  modifiedSince > 0.0)
                // TODO:  add a latestLastModified field here
                return Ok(new { cacheTag = cacheTag, diseaseArray = result });
            else
                return NotFound();
        } // DoGetDiseases()


        /// <summary>Get information about one or more diseases given their ids.</summary>
        /// <param name="id">One or more ids of the diseases whose info to get.</param>
        /// <param name="modifiedSince">If specified, only diseases that have been modified since the specified date (in milliseconds since the reference datum) will be included.</param>
        /// <param name="context">Optional string to specify how the results will be used.</param>
        [Route("disease", Name="GetDiseasesByIds")]
        public IHttpActionResult GetDiseasesByIds([FromUri] int[] id, double modifiedSince = 0.0, string context = "web")
        {
            if (id.Length > 128)        // TODO:  Hardcoded upper bound to avoid unnecessary db queries, but should probably just query once and cache.
                return BadRequest("Too many disease ids specified.");
            if (id.Length <= 0)
                return BadRequest("Must specify at least one disease id.");
            return DoGetDiseases(id, modifiedSince, context);
        } // GetDiseasesByIds()


        /// <summary>Get information about all diseases tracked by the server.</summary>
        /// <param name="modifiedSince">If specified, only diseases that have been modified since the specified date (in milliseconds since the reference datum) will be included.</param>
        /// <param name="context">Optional string to specify how the results will be used.</param>
        [Route("diseases", Name="GetDiseases")]
        public IHttpActionResult GetDiseases(double modifiedSince = 0.0, string context = "web")
        {
            return DoGetDiseases(new int[0], modifiedSince, context);
        } // GetDiseases()


        /*
        [Route("disease/{id}/thumbnail", Name="GetDiseaseThumbnail")]
        public IHttpActionResult GetDiseaseThumbnail([FromUri] int id, string context = "web")
        {
            string imgPath = @"Images\thumbnails\d" + id.ToString();
            Image img = Image.fromFile(imagePath, true);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(ms.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            return result;
        } // GetDiseasesByIds()
        */

    }
}
