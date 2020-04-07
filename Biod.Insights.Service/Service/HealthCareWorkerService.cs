using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.HealthCareWorker;
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
                    RefinementAnswers = hcwCase.RefinementAnswers,
                    RefinementDiseaseIds = hcwCase.RefinementDiseaseIds,
                    RefinementOutput = hcwCase.RefinementOutput,
                    RefinementQuestions = hcwCase.RefinementQuestions,
                    SymptomOnsetDate = hcwCase.SymptomOnsetDate,
                    GeonameId = hcwCase.GeonameId,
                    Status = hcwCase.DiagnosedDiseaseId.HasValue ? "Diagnosed" : "Undiagnosed"
                });

            return await result.FirstOrDefaultAsync();
        }

        public async Task<int> PostCase(PostCaseModel postCaseModel)
        {
            var initialSuggestedDiseasesString = await _dataSystemsHealthCareWorkerApiService.GetInitialSuggestedDiseases(postCaseModel);
            _logger.LogInformation($"PostCase received initial output from Api1: {initialSuggestedDiseasesString}");

            var initialSuggestedDiseases = JsonConvert.DeserializeObject<IEnumerable<ApiGetHealthCareWorkerModel>>(initialSuggestedDiseasesString);
            var refinementDiseaseIds = initialSuggestedDiseases.Where(d => d.DiseaseId.HasValue).Select(d => d.DiseaseId.Value).ToList();
            var refinementDiseaseIdsString = string.Join(",", refinementDiseaseIds);
            _logger.LogInformation($"PostCase refinement disease Ids: {refinementDiseaseIdsString}");

            var refinementQuestionsString = await _dataSystemsHealthCareWorkerApiService.GetRefinementQuestions(refinementDiseaseIds);
            var refinementQuestions = JsonConvert.DeserializeObject<ApiRefinementQuestionModel>(refinementQuestionsString); // validate api data model
            _logger.LogInformation($"PostCase refinement questions: {refinementQuestionsString}");

            var hcwCase = new HcwCases
            {
                UserId = postCaseModel.UserId,
                GeonameId = postCaseModel.GeonameId,
                ArrivalDate = postCaseModel.ArrivalDate,
                DepartureDate = postCaseModel.DepartureDate,
                SymptomOnsetDate = postCaseModel.SymptomOnsetDate,
                PrimarySyndromes = string.Join(",", postCaseModel.PrimarySyndromes),
                CreatedDate = DateTime.UtcNow,
                LastUpdatedDate = DateTime.UtcNow,
                InitialCaseOutput = initialSuggestedDiseasesString,
                RefinementDiseaseIds = refinementDiseaseIdsString,
                RefinementQuestions = refinementQuestionsString
            };

            _biodZebraContext.HcwCases.Add(hcwCase);
            await _biodZebraContext.SaveChangesAsync();

            _logger.LogInformation($"PostCase created successfully: {hcwCase.HcwCaseId}");

            return hcwCase.HcwCaseId;
        }

        public async Task<int> PutCase(PutCaseModel putCaseModel)
        {
            var hcwCase = new HcwCases
            {
                HcwCaseId = putCaseModel.CaseId
            };

            _biodZebraContext.HcwCases.Attach(hcwCase);
            hcwCase.DiagnosedDiseaseId = putCaseModel.DiagnosedDiseaseId;
            hcwCase.OtherDiagnosedDiseaseName = putCaseModel.OtherDiagnosedDiseaseName;
            await _biodZebraContext.SaveChangesAsync();

            _logger.LogInformation($"PutCase updated successfully: {hcwCase.HcwCaseId}");

            return hcwCase.HcwCaseId;
        }
    }
}