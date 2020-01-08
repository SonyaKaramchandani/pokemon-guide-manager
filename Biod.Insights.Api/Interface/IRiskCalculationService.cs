using System.Threading.Tasks;
using Biod.Insights.Api.Data.CustomModels;

namespace Biod.Insights.Api.Interface
{
    public interface IRiskCalculationService
    {
        public Task<bool> HasPreCalculatedImportationRisk(int geonameId);

        public Task<usp_ZebraDataRenderSetImportationRiskByGeonameId_Result.StoredProcedureReturnCode> PreCalculateImportationRisk(int geonameId);
    }
}