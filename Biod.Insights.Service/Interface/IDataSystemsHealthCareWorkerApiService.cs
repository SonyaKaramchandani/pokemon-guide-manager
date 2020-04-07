using Biod.Insights.Service.Models.HealthCareWorker;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biod.Insights.Service.Interface
{
    public interface IDataSystemsHealthCareWorkerApiService
    {
        Task<string> GetInitialSuggestedDiseases(PostCaseModel postCaseModel);
        Task<string> GetRefinementQuestions(List<int> diseaseIds);
    }
}