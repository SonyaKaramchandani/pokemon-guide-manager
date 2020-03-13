using Biod.Zebra.Library.Infrastructures.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Biod.Zebra.WeeklyOperationConsole
{
    public class Program
    {
        private static readonly bool shouldLogToFile = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("isLogToFile"));
        private static readonly string logDirectory = ConfigurationManager.AppSettings.Get("logFolder") ?? Path.GetTempPath();
        private static readonly string baseUrl = ConfigurationManager.AppSettings.Get("ZebraApi");
        private static readonly string notificationApiBaseUrl = ConfigurationManager.AppSettings.Get("NotificationApi");
        private static readonly ILogger Logger;

        static Program()
        {
            if (shouldLogToFile)
            {
                Logger = new FileLogger(logDirectory);
            }
            else
            {
                Logger = new MockedLogger();
            }
        }

        public static void Main()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                using (var notificationClient = new HttpClient())
                {
                    // Configure the clients
                    notificationClient.BaseAddress = new Uri(notificationApiBaseUrl);
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var byteArray = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings.Get("ZebraBasicAuthUsernameAndPassword"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    if (bool.Parse(ConfigurationManager.AppSettings.Get("IsSendWeeklyBriefEnabled")))
                    {
                        Task.WaitAll(SendWeeklyEmail(notificationClient));

                    }
                    if (bool.Parse(ConfigurationManager.AppSettings.Get("IsUpdateWeeklyDataEnabled")))
                    {
                        
                        Task.WaitAll(UpdateWeeklyData(client));
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error("Failed to complete weekly operations", ex);
            }
        }

        /// <summary>
        /// Makes the Get API Call for sending the Weekly Brief emails
        /// </summary>
        /// <param name="client">the http client</param>
        /// <returns>The response if the request was successful, otherwise null</returns>
        public async static Task<HttpResponseMessage> SendWeeklyEmail(HttpClient client)
        {
            Logger.Info($"Sending API request to send Weekly Brief emails");
            HttpResponseMessage response;
            try
            {
                response = await client.PostAsync("weekly", null);

                if (response != null && response.IsSuccessStatusCode)
                {
                    Logger.Info($"Successfully triggered API to send Weekly Brief emails");
                    return response;
                }

                Logger.Warning($"Request to send Weekly Brief emails was unsuccessful. Server returned { response?.StatusCode }");
                Logger.Warning(await response?.Content.ReadAsStringAsync() ?? "Response was null");
            }
            catch (Exception ex)
            {
                Logger.Error($"An error occurred when sending API request to trigger weekly brief emails", ex);
            }
            return null;
        }

        /// <summary>
        /// Makes the Post API Call for updating the weekly email data
        /// </summary>
        /// <param name="client">the http client</param>
        /// <returns>The response if the request was successful, otherwise null</returns>
        public async static Task<HttpResponseMessage> UpdateWeeklyData(HttpClient client)
        {
            Logger.Info($"Sending API request to update Weekly Email data");
            HttpResponseMessage response;
            try
            {
                response = await client.PostAsync("api/ZebraEmailUpdateWeeklyData", null);

                if (response != null && response.IsSuccessStatusCode)
                {
                    Logger.Info($"Successfully triggered API to update Weekly Email data");
                    return response;
                }

                Logger.Warning($"Request to update Weekly Email data was unsuccessful. Server returned { response?.StatusCode }");
                Logger.Warning(await response?.Content.ReadAsStringAsync() ?? "Response was null");
            }
            catch (Exception ex)
            {
                Logger.Error($"An error occurred when sending API request to trigger update of Weekly Email data", ex);
            }
            return null;
        }
    }
}
