using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Service.Models;
using Biod.Insights.Service.Models.Disease;

namespace Biod.Insights.Service.Interface
{
    public interface IDiseaseService
    {
        Task<CaseCountModel> GetDiseaseCaseCount(int diseaseId, int? geonameId);
        
        Task<CaseCountModel> GetDiseaseCaseCount(int diseaseId, int? geonameId, int? eventId);
        
        Task<DiseaseInformationModel> GetDisease(int diseaseId);
        
        Task<IEnumerable<DiseaseInformationModel>> GetDiseases();
    }
}