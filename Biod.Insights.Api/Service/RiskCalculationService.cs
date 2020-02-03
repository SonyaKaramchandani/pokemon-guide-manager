using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Api.Data.CustomModels;
using Biod.Insights.Api.Data.EntityModels;
using Biod.Insights.Api.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Service
{
    public class RiskCalculationService : IRiskCalculationService
    {
        private readonly ILogger<RiskCalculationService> _logger;
        private readonly BiodZebraContext _biodZebraContext;

        /// <summary>
        /// Risk Calculation service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        public RiskCalculationService(
            BiodZebraContext biodZebraContext,
            ILogger<RiskCalculationService> logger)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
        }

        public async Task<bool> HasPreCalculatedImportationRisk(int geonameId)
        {
            return await _biodZebraContext.EventImportationRisksByGeoname.AnyAsync(g => g.GeonameId == geonameId);
        }

        public async Task<usp_ZebraDataRenderSetImportationRiskByGeonameId_Result.StoredProcedureReturnCode> PreCalculateImportationRisk(int geonameId)
        {
            var hasPreCalculation = await HasPreCalculatedImportationRisk(geonameId);
            if (hasPreCalculation)
            {
                // Pre-calculations done already
                return usp_ZebraDataRenderSetImportationRiskByGeonameId_Result.StoredProcedureReturnCode.NoOperation;
            }
            var result = (await _biodZebraContext.usp_ZebraDataRenderSetImportationRiskByGeonameId_Result
                    .FromSqlInterpolated($@"EXECUTE zebra.usp_ZebraDataRenderSetImportationRiskByGeonameId
                                            @GeonameId = {geonameId}")
                    .ToListAsync())
                .First();
            var resultCode = (usp_ZebraDataRenderSetImportationRiskByGeonameId_Result.StoredProcedureReturnCode) result.Result;
            _logger.LogInformation($"Ran pre-calculation of importation risk on geonameId {geonameId}: Received result status code: {resultCode}");

            return resultCode;

        }
    }
}