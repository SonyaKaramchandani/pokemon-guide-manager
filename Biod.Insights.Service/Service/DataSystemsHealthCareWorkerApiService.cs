using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.HealthCareWorker;
using Microsoft.Extensions.Logging;

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

        public async Task<string> GetInitialSuggestedDiseases(PostCaseModel postCaseModel)
        {
            _logger.LogDebug(
                $"GetApi1 called with parameters: {postCaseModel}");

            var builder = new UriBuilder($"{_httpClient.BaseAddress}/HealthCareWorker");

            var query = HttpUtility.ParseQueryString(builder.Query);
            query.Add("geonameId", postCaseModel.GeonameId.ToString());
            query.Add("arrivalDate", postCaseModel.ArrivalDate.ToString("yyyy-MM-dd"));
            query.Add("departureDate", postCaseModel.DepartureDate.ToString("yyyy-MM-dd"));
            query.Add("symptomOnsetDate", postCaseModel.SymptomOnsetDate.ToString("yyyy-MM-dd"));
            query.Add("primarySyndromes", string.Join(",", postCaseModel.PrimarySyndromes));

            builder.Query = query.ToString();
            var url = builder.ToString();

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }

        public async Task<string> GetRefinementQuestions(List<int> diseaseIds)
        {
            _logger.LogDebug(
                $"GetApi2 called with parameters: {string.Join(",", diseaseIds)}");

            var builder = new UriBuilder($"{_httpClient.BaseAddress}/RefinementQuestions");

            var query = HttpUtility.ParseQueryString(builder.Query);
            diseaseIds.ForEach(diseaseId => query.Add("diseaseId", diseaseId.ToString()));

            builder.Query = query.ToString();
            var url = builder.ToString();

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }
    }
}