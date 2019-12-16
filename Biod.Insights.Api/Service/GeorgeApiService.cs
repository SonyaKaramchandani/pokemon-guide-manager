using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models.Risk;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Biod.Insights.Api.Service
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
            var builder = new UriBuilder($"{_httpClient.BaseAddress}/location/risks");//TODO use constants
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["latitude"] = latitude.ToString();
            query["longitude"] = longitude.ToString();
            builder.Query = query.ToString();
            string url = builder.ToString();

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode(); //TODO: handle failed responses when applicable
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GeorgeRiskClass>(body);
        }
    }
}
