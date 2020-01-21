using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Biod.Zebra.Library.Models.Surveillance;
using System.Net.Http.Headers;
using Biod.Zebra.Library.EntityModels.Surveillance;
using System.IO;
using Biod.Zebra.Library.Infrastructures.Log;
using Biod.Zebra.Library.Models;

namespace Biod.Surveillance.Zebra.SyncConsole
{
    /*
        Synchronizer for updating event metadata for all published events
        from Surveillance Tool to Insights. This also includes sending 
        weekly email briefs, as applicable.
    */
    public class Program
    {
        private static readonly bool shouldLogToFile = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("isLogToFile"));
        private static readonly string loggerdirectory = ConfigurationManager.AppSettings.Get("logFile") ?? Path.GetTempPath();
        private static readonly string baseUrl = ConfigurationManager.AppSettings.Get("ZebraSyncMetadataUpdateApi");
        private static readonly ILogger Logger;

        static Program()
        {
            if (shouldLogToFile)
            {
                Logger = new FileLogger(loggerdirectory);
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
                using (BiodSurveillanceDataEntities dbContext = new BiodSurveillanceDataEntities())
                using (HttpClient client = new HttpClient())
                {
                    // Configure the client
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var byteArray = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings.Get("ZebraBasicAuthUsernameAndPassword"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    Task.WaitAll(Sync(dbContext, client, new DefaultConsole()));

                    if (bool.Parse(ConfigurationManager.AppSettings.Get("IsSendWeeklyBriefEnabled")))
                    {
                        Task.WaitAll(SendWeeklyEmail(client));

                    }
                    if (bool.Parse(ConfigurationManager.AppSettings.Get("IsUpdateWeeklyDataEnabled")))
                    {
                        Task.WaitAll(UpdateWeeklyData(client));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An error has occurred");
                Logger.Error(ex.ToString());

                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Looks up published events from the database, transforms each into the EventUpdateModel
        /// and sends them using the provided HttpClient.
        /// </summary>
        /// <param name="dbContext">the database context</param>
        /// <param name="client">the http client</param>
        /// <param name="console">the console logger</param>
        /// <returns>the number of successful updates</returns>
        public static async Task<int> Sync(BiodSurveillanceDataEntities dbContext, HttpClient client, IConsoleLogger console)
        {
            var publishedEvents = dbContext.SurveillanceEvents.Where(p => p.IsPublished == true).ToList();

            int counter = 0,
            failures = 0;
            foreach (SurveillanceEvent pubEvent in publishedEvents)
            {
                EventUpdateModel eventUpdateModel = ConvertToEventUpdate(dbContext, pubEvent);

                var updateResponse = await SendEventUpdate(client, eventUpdateModel);
                if (updateResponse == null)
                {
                    failures++;
                    Logger.Error($"Failed to get response for EventId ({ eventUpdateModel.eventID })");
                }

                console.UpdateConsole($"EventId ({ eventUpdateModel.eventID }) is done    { counter++ } of { publishedEvents.Count() }");
            }

            int success = counter - failures;
            Logger.Info($"Successfully updated { success } of { counter } events");

            return success;
        }

        /// <summary>
        /// Makes the update request using the provided http client
        /// </summary>
        /// <param name="client">the http client</param>
        /// <param name="eventUpdateModel">the model to send</param>
        /// <returns>The response if the request was successful, otherwise null</returns>
        public async static Task<HttpResponseMessage> SendEventUpdate(HttpClient client, EventUpdateModel eventUpdateModel)
        {
            if (eventUpdateModel == null)
            {
                throw new ArgumentNullException("The provided model cannot be null");
            }

            Logger.Info($"Sending update request for Event ID '{ eventUpdateModel.eventID }'");
            HttpResponseMessage response;
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(eventUpdateModel), Encoding.UTF8, "application/json");
                response = await client.PostAsync("api/ZebraEventUpdate", content);

                if (response != null && response.IsSuccessStatusCode)
                {
                    Logger.Info($"Successfully sent update request for Event ID '{ eventUpdateModel.eventID }'");
                    return response;
                }

                Logger.Warning($"Update request for Event ID '{ eventUpdateModel.eventID }' was unsuccessful. Server returned { response?.StatusCode }");
                Logger.Warning(await response?.Content.ReadAsStringAsync() ?? "Response was null");
            }
            catch (Exception ex)
            {
                Logger.Error($"An error occurred when sending update request for Event ID '{ eventUpdateModel.eventID }'");
                Logger.Error(ex.ToString());
            }
            return null;
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
                response = await client.GetAsync("api/ZebraEmailUsersWeeklyEmail");

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
                Logger.Error($"An error occurred when sending API request to trigger weekly brief emails");
                Logger.Error(ex.ToString());
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

        /// <summary>
        /// Converts the database Event model to the Serializable model used for the API request.
        /// </summary>
        /// <param name="dbContext">the database context</param>
        /// <param name="pubEvent">the event object to update</param>
        /// <returns>the converted model</returns>
        public static EventUpdateModel ConvertToEventUpdate(BiodSurveillanceDataEntities dbContext, SurveillanceEvent pubEvent)
        {
            if (pubEvent == null)
            {
                throw new ArgumentNullException("The provided event cannot be null");
            }

            Logger.Debug($"Mapping Event ID '{ pubEvent.EventId }' started");

            Logger.Debug($"Retrieving and processing event locations");
            List<SurveillanceXtbl_Event_Location> locations = dbContext.SurveillanceXtbl_Event_Location
                .Where(e => e.EventId == pubEvent.EventId)
                .AsEnumerable()
                .Select(el => ConvertToEventLocation(el))
                .ToList();

            Logger.Debug($"Processing associated articles");
            List<ArticleUpdateForZebra> articleList = pubEvent.ProcessedArticles
                .Select(a => ConvertToArticleUpdate(a))
                .ToList();

            Logger.Debug($"Creating the EventUpdateModel");
            EventUpdateModel eventUpdateModel = new EventUpdateModel
            {
                eventID = pubEvent.EventId.ToString(),
                eventTitle = pubEvent.EventTitle,
                startDate = (pubEvent.StartDate != null) ? pubEvent.StartDate.ToString() : "",
                endDate = (pubEvent.EndDate != null) ? pubEvent.EndDate.ToString() : "",
                lastUpdatedDate = (pubEvent.LastUpdatedDate!= null) ? pubEvent.LastUpdatedDate.ToString() : "",
                diseaseID = pubEvent.DiseaseId.ToString(),
                speciesID = pubEvent.SpeciesId,
                reasonIDs = pubEvent.EventCreationReasons.Select(r => r.ReasonId.ToString()).ToArray(),
                alertRadius = pubEvent.IsLocalOnly.ToString(),
                priorityID = pubEvent.PriorityId.ToString(),
                isPublished = pubEvent.IsPublished.ToString(),
                summary = pubEvent.Summary,
                notes = pubEvent.Notes,
                locationObject = JsonConvert.SerializeObject(locations),
                associatedArticles = JsonConvert.SerializeObject(articleList),
                LastUpdatedByUserName = pubEvent.LastUpdatedByUserName
            };

            Logger.Debug($"Mapping Event ID '{ pubEvent.EventId }' completed");
            return eventUpdateModel;
        }

        /// <summary>
        /// Converts a database Xtbl_Event_Location model to a Serializable model used in the API request
        /// </summary>
        /// <param name="location">the database location model</param>
        /// <returns>the converted model</returns>
        public static SurveillanceXtbl_Event_Location ConvertToEventLocation(SurveillanceXtbl_Event_Location location)
        {
            if (location == null)
            {
                throw new ArgumentNullException("The provided location cannot be null");
            }

            return new SurveillanceXtbl_Event_Location
            {
                GeonameId = location.GeonameId,
                EventDate = location.EventDate,
                SuspCases = location.SuspCases ?? 0,
                ConfCases = location.ConfCases ?? 0,
                RepCases = location.RepCases ?? 0,
                Deaths = location.Deaths ?? 0
            };
        }

        /// <summary>
        /// Converts a database ProcessedArticle model to a Serializable model used in the API request
        /// </summary>
        /// <param name="article">the databse article model</param>
        /// <returns>the converted model</returns>
        public static ArticleUpdateForZebra ConvertToArticleUpdate(SurveillanceProcessedArticle article)
        {
            if (article == null)
            {
                throw new ArgumentNullException("The provided article cannot be null");
            }

            return new ArticleUpdateForZebra
            {
                ArticleId = article.ArticleId,
                ArticleTitle = article.ArticleTitle,
                SystemLastModifiedDate = article.SystemLastModifiedDate,
                CertaintyScore = article.CertaintyScore,
                ArticleFeedId = article.ArticleFeedId,
                FeedURL = article.FeedURL,
                FeedSourceId = article.FeedSourceId,
                FeedPublishedDate = article.FeedPublishedDate,
                HamTypeId = article.HamTypeId,
                OriginalSourceURL = article.OriginalSourceURL,
                IsCompleted = article.IsCompleted,
                SimilarClusterId = article.SimilarClusterId,
                OriginalLanguage = article.OriginalLanguage,
                UserLastModifiedDate = article.UserLastModifiedDate,
                LastUpdatedByUserName = article.LastUpdatedByUserName,
                Notes = article.Notes,
                ArticleBody = null, //article body increases payload
                IsRead = article.IsRead,
                DiseaseObject = "",
                SelectedPublishedEventIds = new List<int>()
            };
        }

        /// <summary>
        /// Class using the native Console for outputting information to the console
        /// </summary>
        public class DefaultConsole : IConsoleLogger
        {
            public void UpdateConsole(string message)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write(message);
            }
        }
    }
}