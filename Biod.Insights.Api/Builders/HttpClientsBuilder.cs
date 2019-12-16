using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models;
using Biod.Insights.Api.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Biod.Insights.Api.Builders
{
    public static class HttpClientsBuilder
    {
        /// <summary>
        /// Configure external services that requires httpClient
        /// </summary>
        /// <param name="services">The Service Collection</param>
        /// <param name="configuration">The api configuration</param>
        /// <returns></returns>
        private const string HttpSetting_ConfigSection = "GeorgeApi";
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            var httpSettings = configuration.GetSection(HttpSetting_ConfigSection).Get<GeorgeApiSettings>();

            services.AddHttpClient<IGeorgeApiService, GeorgeApiService>(client =>
            {
                client.BaseAddress = new Uri(httpSettings.BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(httpSettings.TimeoutSeconds);
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(httpSettings.HandlerLifetimeMinutes))
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                Credentials = new NetworkCredential(httpSettings.NetworkUser, httpSettings.NetworkPassword)
            })
            .AddPolicyHandler(GetRetryPolicy(httpSettings));
            // TODO implement Polly Circuit Breaker Policy
            return services;
        }
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(HttpSettings httpSettings)
        {
            Random randomJitter = new Random();
            //Policy with exponential back-off and added random jitter to not hammer the external api when it comes back online
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(httpSettings.MaxRetryCount,  
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(httpSettings.RetryDelaySeconds, retryAttempt))
                                  + TimeSpan.FromMilliseconds(randomJitter.Next(0, httpSettings.JitterMilliseconds))
                );
        }
    }
}
