using System.Threading.Tasks;
using Biod.Insights.Api.Data.CustomModels;

namespace Biod.Insights.Api.Interface
{
    public interface IRiskCalculationService
    {
        Task<bool> HasPreCalculatedImportationRisk(int geonameId);

        Task<usp_ZebraDataRenderSetImportationRiskByGeonameId_Result.StoredProcedureReturnCode> PreCalculateImportationRisk(int geonameId);
    }
}