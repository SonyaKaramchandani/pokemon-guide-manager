using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Models.Disease;
using Biod.Insights.Service.Models.Event;

namespace Biod.Insights.Service.Interface
{
    public interface IDiseaseService
    {
        Task<IEnumerable<ProximalCaseCountModel>> GetDiseaseCaseCount(int diseaseId, int? geonameId);

        Task<IEnumerable<ProximalCaseCountModel>> GetDiseaseCaseCount(int diseaseId, int? geonameId, int? eventId);

        Task<DiseaseInformationModel> GetDisease(DiseaseConfig diseaseConfig);
        
        Task<IEnumerable<DiseaseGroupModel>> GetDiseaseGroups();

        Task<IEnumerable<DiseaseInformationModel>> GetDiseases(DiseaseConfig diseaseConfig);
    }
}