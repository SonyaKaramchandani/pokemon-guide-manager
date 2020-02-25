using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace Biod.Insights.Common.HttpClients
{
    public static class HttpClientsBuilder
    {
        /// <summary>
        /// Configure external services that requires httpClient
        /// </summary>
        /// <param name="services">The Service Collection</param>
        /// <param name="httpSettings">The http client configuration</param>
        /// <param name="httpClientBuilder">Function to allow customization of httpClientBuilder</param>
        /// <returns></returns>
        public static IServiceCollection AddHttpClients<T1, T2>(
            this IServiceCollection services,
            HttpSettings httpSettings,
            Action<IHttpClientBuilder> httpClientBuilder = null)
            where T1 : class
            where T2 : class, T1
        {
            var builder = services.AddHttpClient<T1, T2>(client =>
                {
                    client.BaseAddress = new Uri(httpSettings.BaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(httpSettings.TimeoutSeconds);
                })
                .SetHandlerLifetime(TimeSpan.FromMinutes(httpSettings.HandlerLifetimeMinutes))
                .AddPolicyHandler(GetRetryPolicy(httpSettings));

            httpClientBuilder?.Invoke(builder);

            // TODO implement Polly Circuit Breaker Policy
            return services;
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(HttpSettings httpSettings)
        {
            var randomJitter = new Random();
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