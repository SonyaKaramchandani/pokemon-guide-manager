using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.Models.FilterEventResult;

namespace Biod.Zebra.Controllers.api
{
    [Authorize]
    public class DiseaseController : BaseApiController
    {
        [Route("mvcapi/disease/aggregatedrisk")]
        public HttpResponseMessage GetAggregatedRisk([QueryString] int diseaseId, [QueryString] string geonameIds)
        {
            try
            {
                var caseCount = DbContext.usp_ZebraDiseaseGetLocalCaseCount(diseaseId, geonameIds).FirstOrDefault();
                var risk = DbContext.usp_ZebraDiseaseGetImportationRisk(diseaseId, geonameIds).FirstOrDefault();
                
                var minTravellers = Convert.ToDecimal(risk?.ImportationMinExpTravelers);
                var maxTravellers = Convert.ToDecimal(risk?.ImportationMaxExpTravelers);

                var result = new DiseaseGroupResultViewModel
                {
                    TotalCases = caseCount ?? 0,
                    TotalCasesText = caseCount.HasValue && caseCount > 0 ? caseCount.Value.ToString("N0") : "No cases reported in your locations",
                    MinTravellers = minTravellers,
                    MaxTravellers = maxTravellers,
                    TravellersText = maxTravellers >= 0.01m ? StringFormattingHelper.GetInterval(minTravellers, maxTravellers) : "Negligible"
                };
            
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to retrieve aggregated risks for disease ID {diseaseId} and geoname IDs {geonameIds}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}