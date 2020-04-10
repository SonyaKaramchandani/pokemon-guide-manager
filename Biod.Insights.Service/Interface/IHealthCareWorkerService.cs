using Biod.Insights.Service.Models.HealthCareWorker;
using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Service.Models.HealthCareWorker.DataSystemsApiModels;

namespace Biod.Insights.Service.Interface
{
    public interface IHealthCareWorkerService
    {
        Task<IEnumerable<GetCaseModel>> GetCaseList();
        Task<GetCaseModel> GetCase(int caseId);
        Task<int> CreateCase(CreateCaseModel createCaseModel);
        Task<int> UpdateCase(UpdateCaseModel updateCaseModel);
        Task<IEnumerable<RefinedHealthCareWorkerModel>> RefineCase(RefineCaseModel refineCaseModel);
    }
}