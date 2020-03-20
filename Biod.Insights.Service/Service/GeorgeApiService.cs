using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.Risk;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Biod.Insights.Service.Service
{
    public class GeorgeApiService : IGeorgeApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GeorgeApiService> _logger;

        public GeorgeApiService(HttpClient httpClient, ILogger<GeorgeApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<GeorgeRiskClass> GetLocationRisk(float latitude, float longitude)
        {
            _logger.LogDebug(@"GetLocationRisk called with parameters {Lat} and {Long}", latitude, longitude);

            var builder = new UriBuilder($"{_httpClient.BaseAddress}/location/risks"); //TODO use constants

            var query = HttpUtility.ParseQueryString(builder.Query);
            query.Add("latitude", latitude.ToString(CultureInfo.InvariantCulture));
            query.Add("longitude", longitude.ToString(CultureInfo.InvariantCulture));
            builder.Query = query.ToString();
            var url = builder.ToString();

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<GeorgeRiskClass>(body);
        }

        public async Task<GeorgeRiskClass> GetLocationRisk(int geonameId)
        {
            _logger.LogDebug(@"GetLocationRisk called with parameters {geonameId}", geonameId);

            var builder = new UriBuilder($"{_httpClient.BaseAddress}/location/risksbygeonameId");

            var query = HttpUtility.ParseQueryString(builder.Query);
            query.Add("geonameId", geonameId.ToString(CultureInfo.InvariantCulture));
            builder.Query = query.ToString();
            var url = builder.ToString();

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<GeorgeRiskClass>(body);
        }
    }
}