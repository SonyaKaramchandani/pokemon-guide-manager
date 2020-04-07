using Biod.Insights.Service.Models.HealthCareWorker;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biod.Insights.Service.Interface
{
    public interface IHealthCareWorkerService
    {
        Task<IEnumerable<GetCaseModel>> GetCaseList();
        Task<GetCaseModel> GetCase(int caseId);
        Task<int> PostCase(PostCaseModel postCaseModel);
        Task<int> PutCase(PutCaseModel putCaseModel);
    }
}