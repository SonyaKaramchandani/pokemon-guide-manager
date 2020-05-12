using Biod.SyncConsole.Infrastructures;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;
using Biod.Zebra.Notification;
using Biod.Zebra.Library.EntityModels.Surveillance;
using Newtonsoft.Json;
using Biod.SyncConsole.Models;

namespace Biod.SyncConsole
{
    /// <summary>
    /// Main program
    /// </summary>
    class Program
    {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            try
            {
                // update databases
                BiodSurveillanceDataEntities surveillanceDbContext = new BiodSurveillanceDataEntities();
                surveillanceDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("commandTimeout"));

                var ResultMessage = new List<string>();
                string message;

                var serviceDomainName = ConfigurationManager.AppSettings.Get("serviceDomainName");


                //in surveillance for articles and events
                ResultMessage = surveillanceDbContext.usp_UpdateSurveillanceApi_main(serviceDomainName).ToList();
                message = "Biod Surveillance Sync Console Successfully Executed!";
                foreach (var r in ResultMessage)
                {
                    message += "<br>" + Environment.NewLine + r;
                }

                Console.WriteLine(message.Replace("<br>", ""));
                var subject = ConfigurationManager.AppSettings.Get("emailSubjectUponSuccess");
                SendMail(subject, message).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                var message = string.Format("{0}: {1}\n{2}\n", DateTime.Now, ex.Message, ex.StackTrace);
                var subject = ConfigurationManager.AppSettings.Get("emailSubjectUponError");
                SendMail(subject, message).GetAwaiter().GetResult();
            }

            // Automatic case count updates into the surveillance tool
            try
            {
                AutomaticCaseCountUpdates();
            }
            catch (Exception ex)
            {
                var message = string.Format("{0}: {1}\n{2}\n", DateTime.Now, ex.Message, ex.StackTrace);
                var subject = ConfigurationManager.AppSettings.Get("emailSubjectUponError");
                SendMail(subject, message).GetAwaiter().GetResult();
            }
        }

        private static void AutomaticCaseCountUpdates()
        {
            var dbContext = new BiodSurveillanceDataEntities();
            var config = AutoSurveillanceConfig.GetConfig();
            int pageSize = 1000;

            bool change = false;
            foreach (Site site in config.Sites)
            {
                string requestUrl = $"DiseaseOccurrences?pageSize={pageSize}&diseaseId={site.DiseaseId}&geonameId={site.LocationId}" +
                                    $"&includeGeonameIdChildren=true&startDate={DateTime.UtcNow.ToShortDateString()}";
                if (!string.IsNullOrEmpty(site.IncludeChildren) && site.IncludeChildren.ToLower().Equals("true"))
                {
                    requestUrl += $"&includeGeonameIdChildren=true";
                }
                if (!string.IsNullOrEmpty(site.Source))
                {
                    requestUrl += $"&dataSourceId={site.Source}";
                }

                var @event = dbContext.SurveillanceEvents.SingleOrDefault(e => e.DiseaseId.ToString() == site.DiseaseId && e.Xtbl_Event_Location.Any(l => l.GeonameId.ToString() == site.LocationId));
                if (@event == null)
                {
                    var message = $"Missing event for disease {site.DiseaseId}, location {site.LocationId}";
                    var subject = ConfigurationManager.AppSettings.Get("emailSubjectUponError");
                    SendMail(subject, message).GetAwaiter().GetResult();
                    continue;
                }

                // First query for cumulative number of reported cases (metricId 1)
                var diseaseOccurrences = new List<DiseaseOccurrence>();
                int pageNum = 1;
                while (true)
                {
                    var result = GetJsonStringResultAsync(config.BaseUrl, config.UserName, config.Password, requestUrl + $"&pageNum={pageNum}&metricId=1").Result;
                    var records = JsonConvert.DeserializeObject<IList<DiseaseOccurrence>>(result);
                    diseaseOccurrences.AddRange(records);
                    if (records.Count < pageSize)
                    {
                        break;
                    }
                    pageNum++;
                } 

                var reportedCases = diseaseOccurrences
                    .GroupBy(o => o.Places.GeonameId)
                    .Select(g => g.OrderByDescending(o => o.LastModified).OrderByDescending(o => o.EndDate).First())
                    .ToList();

                // Second query for cumulative number of deaths (metricId 34)
                diseaseOccurrences.Clear();
                pageNum = 1;
                while (true)
                {
                    var result = GetJsonStringResultAsync(config.BaseUrl, config.UserName, config.Password, requestUrl + $"&pageNum={pageNum}&metricId=34").Result;
                    var records = JsonConvert.DeserializeObject<IList<DiseaseOccurrence>>(result);
                    diseaseOccurrences.AddRange(records);
                    if (records.Count < pageSize)
                    {
                        break;
                    }
                    pageNum++;
                }

                var reportedDeaths = diseaseOccurrences
                    .GroupBy(o => o.Places.GeonameId)
                    .Select(g => g.OrderByDescending(o => o.LastModified).OrderByDescending(o => o.EndDate).First())
                    .ToList();

                bool eventChanged = false;
                foreach (var reportedCase in reportedCases)
                {
                    // get the matching deaths
                    int? deaths = (int)reportedDeaths.SingleOrDefault(d => d.Places.GeonameId == reportedCase.Places.GeonameId
                                                                           && d.EndDate == reportedCase.EndDate)?.Value;
                    var eventLocation = @event.Xtbl_Event_Location.SingleOrDefault(l => l.EventDate == reportedCase.EndDate && l.GeonameId == reportedCase.Places.GeonameId);
                    if (eventLocation == null)
                    {
                        eventLocation = new SurveillanceXtbl_Event_Location()
                        {
                            EventId = @event.EventId,
                            GeonameId = reportedCase.Places.GeonameId,
                            EventDate = reportedCase.EndDate,
                            SuspCases = 0,
                            ConfCases = Math.Max((int)reportedCase.Value, deaths ?? 0),
                            RepCases = Math.Max((int)reportedCase.Value, deaths ?? 0),
                            Deaths = deaths
                        };

                        dbContext.SurveillanceXtbl_Event_Location.Add(eventLocation);
                        eventChanged = true;
                    }
                    else
                    {
                        if (eventLocation.RepCases != (int)reportedCase.Value ||
                            eventLocation.Deaths != deaths)
                        {
                            eventLocation.ConfCases = Math.Max((int)reportedCase.Value, (deaths ?? eventLocation.Deaths) ?? 0);
                            eventLocation.RepCases = Math.Max((int)reportedCase.Value, (deaths ?? eventLocation.Deaths) ?? 0);
                            eventLocation.Deaths = deaths ?? eventLocation.Deaths;
                            eventChanged = true;
                        }
                    }
                }

                if (eventChanged)
                {
                    @event.IsPublished = false;
                    change = true;
                }
            }

            if (change)
            {
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the json string result asynchronous.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        /// <param name="requestUrl">The request URL.</param>
        /// <returns></returns>
        private static async Task<string> GetJsonStringResultAsync(string baseUrl, string userName, string password, string requestUrl)
        {
            string result = string.Empty;
            try
            {
                var credentials = new NetworkCredential(userName, password);
                var handler = new HttpClientHandler { Credentials = credentials };
                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(requestUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Logging.Log("Error: " + ex.Message + "\n" + ex.InnerException);
                return ex.Message;
            }
        }

        /// <summary>
        /// Send email to sender.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        private static async Task SendMail(string subject, string message)
        {
            var mailRecipientList = ConfigurationManager.AppSettings.Get("emailRecipientList");

            try
            {
                var mail = new EmailMessage();
                var emailClient = new EmailClient();

                foreach (string recipient in mailRecipientList.Split(','))
                {
                    mail.To.Add(recipient);
                }
                mail.Subject = subject;
                mail.Body = message;
                await emailClient.SendEmailAsync(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
        }
    }
}
