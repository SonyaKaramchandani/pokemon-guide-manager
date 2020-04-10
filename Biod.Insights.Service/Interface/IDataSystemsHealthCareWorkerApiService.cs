using Biod.Insights.Service.Models.HealthCareWorker;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biod.Insights.Service.Interface
{
    public interface IDataSystemsHealthCareWorkerApiService
    {
        Task<string> GetInitialSuggestedDiseases(CreateCaseModel createCaseModel);
        Task<string> GetRefinementQuestions(List<int> diseaseIds);
        Task<string> GetRefinedSuggestedDiseases(List<ActivityAnswer> activityAnswers, List<VaccineAnswer> vaccineAnswers, string initialOutput);
    }
}