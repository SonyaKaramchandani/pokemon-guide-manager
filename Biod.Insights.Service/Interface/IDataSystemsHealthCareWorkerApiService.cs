using Biod.Insights.Service.Models.HealthCareWorker;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biod.Insights.Service.Interface
{
    public interface IDataSystemsHealthCareWorkerApiService
    {
        Task<string> GetInitialSuggestedDiseases(CreateCaseModel createCaseModel);
        Task<string> GetSymptomsQuestions(List<int> diseaseIds);
        Task<string> GetActivitiesAndVaccinesQuestions(List<int> diseaseIds);
        Task<string> GetRefinementDiseasesBySymptoms(List<SymptomAnswer> answers, string initialOutput);
        Task<string> GetRefinementDiseasesByActivitiesAndVaccines(List<ActivityAnswer> activityAnswers, List<VaccineAnswer> vaccineAnswers, string initialOutput);
    }
}