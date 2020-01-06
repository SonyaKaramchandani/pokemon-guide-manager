using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Api.Models;
using Biod.Insights.Api.Models.Disease;

namespace Biod.Insights.Api.Interface
{
    public interface IDiseaseService
    {
        Task<CaseCountModel> GetDiseaseCaseCount(int diseaseId, int? geonameId);
        
        Task<DiseaseInformationModel> GetDisease(int diseaseId);
        
        Task<IEnumerable<DiseaseInformationModel>> GetDiseases();
    }
}