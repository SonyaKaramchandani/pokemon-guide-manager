using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using Biod.Zebra.Library.Models.FilterEventResult;

namespace Biod.Zebra.Controllers.api
{
    [Authorize]
    public class DiseaseController : BaseApiController
    {
        [Route("mvcapi/disease/AggregatedCaseCount")]
        public HttpResponseMessage GetAggregatedCaseCount([QueryString] int diseaseId, [QueryString] string geonameIds)
        {
            try
            {
                var caseCount = DbContext.usp_ZebraDiseaseGetLocalCaseCount(diseaseId, geonameIds).FirstOrDefault() ?? 0;
                var result = new DiseaseGroupResultViewModel
                {
                    TotalCases = caseCount,
                    TotalCasesText = caseCount > 0 ? caseCount.ToString("N0") : "No cases reported in or near your locations",
                    IsVisible = caseCount > 0
                };
            
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to retrieve aggregated case count for disease ID {diseaseId} and geoname IDs {geonameIds}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}