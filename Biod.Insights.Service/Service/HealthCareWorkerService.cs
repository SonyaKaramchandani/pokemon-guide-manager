using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.HealthCareWorker;
using Biod.Insights.Service.Models.HealthCareWorker.DataSystemsApiModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Biod.Insights.Service.Service
{
    public class HealthCareWorkerService : IHealthCareWorkerService
    {
        private readonly ILogger<HealthCareWorkerService> _logger;
        private readonly BiodZebraContext _biodZebraContext;
        private readonly IDataSystemsHealthCareWorkerApiService _dataSystemsHealthCareWorkerApiService;

        public HealthCareWorkerService(ILogger<HealthCareWorkerService> logger, BiodZebraContext biodZebraContext, IDataSystemsHealthCareWorkerApiService dataSystemsHealthCareWorkerApiService)
        {
            _logger = logger;
            _biodZebraContext = biodZebraContext;
            _dataSystemsHealthCareWorkerApiService = dataSystemsHealthCareWorkerApiService;
        }

        public async Task<IEnumerable<GetCaseModel>> GetCaseList()
        {
            var result = _biodZebraContext.HcwCases
                .AsNoTracking()
                .OrderByDescending(hcwCase => hcwCase.LastUpdatedDate)
                .Select(hcwCase => new GetCaseModel
                {
                    CaseId = hcwCase.HcwCaseId,
                    CountryName = hcwCase.Geoname.CountryName,
                    LocationName = hcwCase.Geoname.Name,
                    CreatedDate = hcwCase.CreatedDate,
                    LastUpdatedDate = hcwCase.LastUpdatedDate,
                    Status = hcwCase.DiagnosedDiseaseId.HasValue ? "Diagnosed" : "Undiagnosed"
                });

            return await result.ToListAsync();
        }

        public async Task<GetCaseModel> GetCase(int caseId)
        {
            var result = _biodZebraContext.HcwCases
                .AsNoTracking()
                .Where(hcwCase => hcwCase.HcwCaseId == caseId)
                .Select(hcwCase => new GetCaseModel
                {
                    CaseId = hcwCase.HcwCaseId,
                    UserId = hcwCase.UserId,
                    CountryName = hcwCase.Geoname.CountryName,
                    LocationName = hcwCase.Geoname.Name,
                    CreatedDate = hcwCase.CreatedDate,
                    LastUpdatedDate = hcwCase.LastUpdatedDate,
                    ArrivalDate = hcwCase.ArrivalDate,
                    DepartureDate = hcwCase.DepartureDate,
                    DiagnosedDiseaseId = hcwCase.DiagnosedDiseaseId,
                    OtherDiagnosedDiseaseName = hcwCase.OtherDiagnosedDiseaseName,
                    InitialCaseOutput = hcwCase.InitialCaseOutput,
                    PrimarySyndromes = hcwCase.PrimarySyndromes.Split(',', StringSplitOptions.RemoveEmptyEntries),
                    RefinementBySymptomsDiseaseIds = hcwCase.RefinementBySymptomsDiseaseIds,
                    RefinementBySymptomsQuestions = hcwCase.RefinementBySymptomsQuestions,
                    RefinementBySymptomsAnswers = hcwCase.RefinementBySymptomsAnswers,
                    RefinementBySymptomsOutput = hcwCase.RefinementBySymptomsOutput,
                    RefinementByActivitiesAndVaccinesDiseaseIds = hcwCase.RefinementByActivitiesAndVaccinesDiseaseIds,
                    RefinementByActivitiesAndVaccinesQuestions = hcwCase.RefinementByActivitiesAndVaccinesQuestions,
                    RefinementByActivitiesAndVaccinesAnswers = hcwCase.RefinementByActivitiesAndVaccinesAnswers,
                    RefinementByActivitiesAndVaccinesOutput = hcwCase.RefinementByActivitiesAndVaccinesOutput,
                    SymptomOnsetDate = hcwCase.SymptomOnsetDate,
                    GeonameId = hcwCase.GeonameId,
                    Status = hcwCase.DiagnosedDiseaseId.HasValue ? "Diagnosed" : "Undiagnosed"
                });

            return await result.FirstOrDefaultAsync();
        }

        public async Task<int> CreateCase(CreateCaseModel createCaseModel)
        {
            var initialSuggestedDiseasesString = await _dataSystemsHealthCareWorkerApiService.GetInitialSuggestedDiseases(createCaseModel);
            var initialSuggestedDiseases = JsonConvert.DeserializeObject<IEnumerable<HealthCareWorkerModel>>(initialSuggestedDiseasesString);

            var refinementBySymptomsDiseaseIds = initialSuggestedDiseases.Where(d => d.DiseaseId.HasValue && (d.Tier == 1 || d.Tier == 2)).Select(d => d.DiseaseId.Value).ToList();
            var refinementBySymptomsDiseaseIdsString = string.Join(",", refinementBySymptomsDiseaseIds);
            var refinementBySymptomsQuestionsString = await _dataSystemsHealthCareWorkerApiService.GetSymptomsQuestions(refinementBySymptomsDiseaseIds);
            var refinementBySymptomsQuestions = JsonConvert.DeserializeObject<RefinementBySymptomsQuestionModel>(refinementBySymptomsQuestionsString); // validate api data model

            var refinementByActivitiesAndVaccinesDiseaseIds = initialSuggestedDiseases.Where(d => d.DiseaseId.HasValue).Select(d => d.DiseaseId.Value).ToList();
            var refinementByActivitiesAndVaccinesDiseaseIdsString = string.Join(",", refinementByActivitiesAndVaccinesDiseaseIds);
            var refinementByActivitiesAndVaccinesQuestionsString = await _dataSystemsHealthCareWorkerApiService.GetActivitiesAndVaccinesQuestions(refinementByActivitiesAndVaccinesDiseaseIds);
            var refinementByActivitiesAndVaccinesQuestions =
                JsonConvert.DeserializeObject<RefinementByActivitiesAndVaccinesQuestionModel>(refinementByActivitiesAndVaccinesQuestionsString); // validate api data model

            var hcwCase = new HcwCases
            {
                UserId = createCaseModel.UserId,
                GeonameId = createCaseModel.GeonameId,
                ArrivalDate = createCaseModel.ArrivalDate,
                DepartureDate = createCaseModel.DepartureDate,
                SymptomOnsetDate = createCaseModel.SymptomOnsetDate,
                PrimarySyndromes = string.Join(",", createCaseModel.PrimarySyndromes),
                CreatedDate = DateTimeOffset.UtcNow,
                LastUpdatedDate = DateTimeOffset.UtcNow,
                InitialCaseOutput = initialSuggestedDiseasesString,
                RefinementBySymptomsDiseaseIds = refinementBySymptomsDiseaseIdsString,
                RefinementBySymptomsQuestions = refinementBySymptomsQuestionsString,
                RefinementByActivitiesAndVaccinesDiseaseIds = refinementByActivitiesAndVaccinesDiseaseIdsString,
                RefinementByActivitiesAndVaccinesQuestions = refinementByActivitiesAndVaccinesQuestionsString,
            };

            _biodZebraContext.HcwCases.Add(hcwCase);
            await _biodZebraContext.SaveChangesAsync();

            return hcwCase.HcwCaseId;
        }

        public async Task<int> UpdateCase(UpdateCaseModel updateCaseModel)
        {
            var hcwCase = new HcwCases
            {
                HcwCaseId = updateCaseModel.CaseId
            };

            _biodZebraContext.HcwCases.Attach(hcwCase);
            hcwCase.DiagnosedDiseaseId = updateCaseModel.DiagnosedDiseaseId;
            hcwCase.OtherDiagnosedDiseaseName = updateCaseModel.OtherDiagnosedDiseaseName;
            hcwCase.LastUpdatedDate = DateTimeOffset.UtcNow;
            await _biodZebraContext.SaveChangesAsync();

            return hcwCase.HcwCaseId;
        }

        public async Task<IEnumerable<RefinementSymptomsHealthCareWorkerModel>> RefineCaseBySymptoms(RefineCaseBySymptomsModel refineCaseModel)
        {
            var caseModel = await GetCase(refineCaseModel.CaseId);
            var refinementOutputString =
                await _dataSystemsHealthCareWorkerApiService.GetRefinementDiseasesBySymptoms(refineCaseModel.SymptomAnswers, caseModel.InitialCaseOutput);
            var refinementOutput = JsonConvert.DeserializeObject<IEnumerable<RefinementSymptomsHealthCareWorkerModel>>(refinementOutputString);

            var hcwCase = new HcwCases
            {
                HcwCaseId = refineCaseModel.CaseId
            };

            _biodZebraContext.HcwCases.Attach(hcwCase);
            hcwCase.RefinementBySymptomsAnswers = JsonConvert.SerializeObject(new {symptomAnswers = refineCaseModel.SymptomAnswers});
            hcwCase.RefinementBySymptomsOutput = refinementOutputString;
            hcwCase.LastUpdatedDate = DateTimeOffset.UtcNow;
            await _biodZebraContext.SaveChangesAsync();

            return refinementOutput;
        }

        public async Task<IEnumerable<RefinementActivitiesAndVaccinesHealthCareWorkerModel>> RefineCaseByActivitiesAndVaccines(RefineCaseByActivitiesAndVaccinesModel refineCaseModel)
        {
            var caseModel = await GetCase(refineCaseModel.CaseId);
            var refinementOutputString =
                await _dataSystemsHealthCareWorkerApiService.GetRefinementDiseasesByActivitiesAndVaccines(refineCaseModel.ActivityAnswers, refineCaseModel.VaccineAnswers, caseModel.InitialCaseOutput);
            var refinementOutput = JsonConvert.DeserializeObject<IEnumerable<RefinementActivitiesAndVaccinesHealthCareWorkerModel>>(refinementOutputString);

            var hcwCase = new HcwCases
            {
                HcwCaseId = refineCaseModel.CaseId
            };

            _biodZebraContext.HcwCases.Attach(hcwCase);
            hcwCase.RefinementByActivitiesAndVaccinesAnswers = JsonConvert.SerializeObject(new {activityAnswers = refineCaseModel.ActivityAnswers, vaccineAnswers = refineCaseModel.VaccineAnswers});
            hcwCase.RefinementByActivitiesAndVaccinesOutput = refinementOutputString;
            hcwCase.LastUpdatedDate = DateTimeOffset.UtcNow;
            await _biodZebraContext.SaveChangesAsync();

            return refinementOutput;
        }
    }
}