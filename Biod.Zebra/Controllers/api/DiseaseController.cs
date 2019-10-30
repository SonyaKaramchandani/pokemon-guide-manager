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
                // TODO: Move to configuration
                const decimal threshold = 0.01m;
                
                var caseCount = DbContext.usp_ZebraDiseaseGetLocalCaseCount(diseaseId, geonameIds).FirstOrDefault() ?? 0;
                var risk = DbContext.usp_ZebraDiseaseGetImportationRisk(diseaseId, geonameIds).FirstOrDefault();
                
                var minTravellers = Convert.ToDecimal(risk?.ImportationMinExpTravelers);
                var maxTravellers = Convert.ToDecimal(risk?.ImportationMaxExpTravelers);
                var maxProbability = Convert.ToDecimal(risk?.ImportationMaxProbability);

                var result = new DiseaseGroupResultViewModel
                {
                    TotalCases = caseCount,
                    TotalCasesText = caseCount > 0 ? caseCount.ToString("N0") : "No cases reported in or near your locations",
                    MinTravellers = minTravellers,
                    MaxTravellers = maxTravellers,
                    TravellersText = maxTravellers >= 0.01m ? StringFormattingHelper.GetTravellerInterval(minTravellers, maxTravellers, true) : "Negligible",
                    IsVisible = caseCount > 0 || maxProbability >= threshold
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