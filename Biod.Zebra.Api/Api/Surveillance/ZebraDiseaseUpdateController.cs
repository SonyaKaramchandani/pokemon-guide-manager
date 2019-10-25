using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using Biod.Zebra.Library.Infrastructures;

namespace Biod.Zebra.Api.Api.Surveillance
{
    public class ZebraDiseaseUpdateController : BaseApiController
    {
        public IZebraUpdateService RequestResponseService { get; set; }

        public ZebraDiseaseUpdateController()
        {
            RequestResponseService = new CustomRequestResponseService();
        }

        public class ZebraDiseaseUpdatePostModel
        {
            public List<int> DiseaseIds { get; set; }
        }
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Success message");
            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> PostAsync([FromBody] ZebraDiseaseUpdatePostModel model)
        {
            foreach (var diseaseId in model.DiseaseIds)
            {
                await UpdateDiseaseExportation(diseaseId);
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Successfully processed diseases");
        }

        private async Task UpdateDiseaseExportation(int diseaseId)
        {
            var minMaxCasesClasses = new List<MinMaxCasesClass>();
            var grids = DbContext.usp_ZebraDataRenderSetSourceDestinationsPart1ByDisease(diseaseId).ToList();
            foreach (var grid in grids)
            {
                var minMaxCasesService = await RequestResponseService.GetMinMaxCasesService(grid.GridId, grid.Cases.Value.ToString());
                var minMaxCasesServiceResult = minMaxCasesService.Split(',');
                minMaxCasesClasses.Add(
                    new MinMaxCasesClass()
                    {
                        GridId = minMaxCasesServiceResult[0],
                        Cases = minMaxCasesServiceResult[1],
                        MinCases = minMaxCasesServiceResult[2],
                        MaxCases = minMaxCasesServiceResult[3]
                    });
            }
            var jsonEventGridCases = new JavaScriptSerializer().Serialize(minMaxCasesClasses);
            var diseaseCasesInfo = DbContext.usp_ZebraDataRenderSetSourceDestinationsPart2ByDisease(diseaseId, jsonEventGridCases).FirstOrDefault();
            if (diseaseCasesInfo != null)
            {
                bool isMinCaseOverPopulationSizeEqualZero = false;
                if (diseaseCasesInfo.MinCaseOverPopulationSize == 0)
                {
                    diseaseCasesInfo.MinCaseOverPopulationSize = 0.000001;
                    isMinCaseOverPopulationSizeEqualZero = true;
                }
                bool isMaxCaseOverPopulationSizeEqualZero = false;
                if (diseaseCasesInfo.MaxCaseOverPopulationSize == 0.0)
                {
                    diseaseCasesInfo.MinCaseOverPopulationSize = 0.000001;
                    isMaxCaseOverPopulationSizeEqualZero = true;
                }

                if (!isMaxCaseOverPopulationSizeEqualZero)
                {
                    var minMaxPrevalenceService = await RequestResponseService.GetMinMaxPrevalenceService(
                        Convert.ToDouble(diseaseCasesInfo.MinCaseOverPopulationSize).ToString("F20"), Convert.ToDouble(diseaseCasesInfo.MaxCaseOverPopulationSize).ToString("F20"),
                        diseaseCasesInfo.DiseaseIncubation.ToString(), diseaseCasesInfo.DiseaseSymptomatic.ToString(),
                        diseaseCasesInfo.EventStart.Value.ToString("yyyy-MM-dd"), diseaseCasesInfo.EventEnd?.ToString("yyyy-MM-dd") ?? "");

                    var minMaxPrevalenceResult = minMaxPrevalenceService.Split(',');

                    DbContext.usp_ZebraDataRenderSetSourceDestinationsPart3ByDisease(
                        diseaseId,
                        isMinCaseOverPopulationSizeEqualZero ? 0 : Convert.ToDouble(minMaxPrevalenceResult[0]),
                        isMaxCaseOverPopulationSizeEqualZero ? 0 : Convert.ToDouble(minMaxPrevalenceResult[1])
                        );
                }
            }

            Logger.Info($"Successfully updated disease with ID {diseaseId}");
        }
    }
}