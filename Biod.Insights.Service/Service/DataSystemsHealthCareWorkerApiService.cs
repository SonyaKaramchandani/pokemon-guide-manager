using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.HealthCareWorker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Biod.Insights.Service.Service
{
    public class DataSystemsHealthCareWorkerApiService : IDataSystemsHealthCareWorkerApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DataSystemsHealthCareWorkerApiService> _logger;

        public DataSystemsHealthCareWorkerApiService(HttpClient httpClient,
            ILogger<DataSystemsHealthCareWorkerApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> GetInitialSuggestedDiseases(CreateCaseModel createCaseModel)
        {
            var builder = new UriBuilder($"{_httpClient.BaseAddress}/HealthCareWorker");

            var query = HttpUtility.ParseQueryString(builder.Query);
            query.Add("geonameId", createCaseModel.GeonameId.ToString());
            query.Add("arrivalDate", createCaseModel.ArrivalDate.ToString("yyyy-MM-dd"));
            query.Add("departureDate", createCaseModel.DepartureDate.ToString("yyyy-MM-dd"));
            query.Add("symptomOnsetDate", createCaseModel.SymptomOnsetDate.ToString("yyyy-MM-dd"));
            createCaseModel.PrimarySyndromes.ForEach(primarySyndrome => query.Add("primarySyndromes", primarySyndrome));

            builder.Query = query.ToString();
            var url = builder.ToString();

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }

        public async Task<string> GetSymptomsQuestions(List<int> diseaseIds)
        {
            var builder = new UriBuilder($"{_httpClient.BaseAddress}/RefinementQuestionsSymptom");

            var query = HttpUtility.ParseQueryString(builder.Query);
            diseaseIds.ForEach(diseaseId => query.Add("diseaseId", diseaseId.ToString()));

            builder.Query = query.ToString();
            var url = builder.ToString();

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }

        public async Task<string> GetActivitiesAndVaccinesQuestions(List<int> diseaseIds)
        {
            var builder = new UriBuilder($"{_httpClient.BaseAddress}/RefinementQuestionsActivityAndVaccine");

            var query = HttpUtility.ParseQueryString(builder.Query);
            diseaseIds.ForEach(diseaseId => query.Add("diseaseId", diseaseId.ToString()));

            builder.Query = query.ToString();
            var url = builder.ToString();

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }

        public async Task<string> GetRefinementDiseasesBySymptoms(List<SymptomAnswer> symptomAnswers, string initialOutput)
        {
            var url = new UriBuilder($"{_httpClient.BaseAddress}/HCWRefinementSymptom").ToString();
            var jsonSerializerSettings = new JsonSerializerSettings() {ContractResolver = new CamelCasePropertyNamesContractResolver()};
            var symptomAnswersString = JsonConvert.SerializeObject(symptomAnswers, jsonSerializerSettings);
            var param = JsonConvert.SerializeObject(new {symptomAnswers = symptomAnswersString, hcwOutput = initialOutput}, jsonSerializerSettings);

            var encodedContent = new StringContent(param, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, encodedContent);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }

        public async Task<string> GetRefinementDiseasesByActivitiesAndVaccines(List<ActivityAnswer> activityAnswers, List<VaccineAnswer> vaccineAnswers, string initialOutput)
        {
            var url = new UriBuilder($"{_httpClient.BaseAddress}/HCWRefinementActivityAndVaccine").ToString();
            var jsonSerializerSettings = new JsonSerializerSettings() {ContractResolver = new CamelCasePropertyNamesContractResolver()};
            var activityAnswersString = JsonConvert.SerializeObject(activityAnswers, jsonSerializerSettings);
            var vaccineAnswersString = JsonConvert.SerializeObject(vaccineAnswers, jsonSerializerSettings);
            var param = JsonConvert.SerializeObject(new {activityAnswers = activityAnswersString, vaccineAnswers = vaccineAnswersString, hcwOutput = initialOutput}, jsonSerializerSettings);

            var encodedContent = new StringContent(param, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, encodedContent);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }
    }
}