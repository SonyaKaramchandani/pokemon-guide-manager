using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Biod.Insights.Common.HttpClients;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Notification.Engine.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Biod.Insights.Notification.Engine.Services.EmailRendering
{
    public class EmailRenderingApiService : IEmailRenderingApiService
    {
        private readonly HttpClient _httpClient;
        private readonly BiodZebraContext _biodZebraContext;
        private readonly ILogger<EmailRenderingApiService> _logger;
        private readonly HttpSettings _emailRenderingApiSettings;

        public EmailRenderingApiService(
            HttpClient httpClient,
            BiodZebraContext biodZebraContext,
            ILogger<EmailRenderingApiService> logger,
            IOptionsMonitor<NotificationSettings> notificationSettingsAccessor)
        {
            _httpClient = httpClient;
            _biodZebraContext = biodZebraContext;
            _logger = logger;
            _emailRenderingApiSettings = notificationSettingsAccessor.CurrentValue.EmailRenderingApiSettings;
        }

        public async Task<string> RenderEmail(EmailRenderingModel model)
        {
            var body = JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            var response = await _httpClient.PostAsync(_emailRenderingApiSettings.BaseUrl, new StringContent(body, Encoding.UTF8));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}