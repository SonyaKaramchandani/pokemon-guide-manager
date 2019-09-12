using Biod.SyncConsole.EntityModels;
using Biod.SyncConsole.Infrastructures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Linq;


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
                BiodSurveillanceModelEntities surveillanceDbContext = new BiodSurveillanceModelEntities();
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


                //Logging.Log("geonamesJsonString: " + geonamesJsonString);
                //Logging.Log(ConfigurationManager.AppSettings.Get("serviceBaseUrl") + "Places/Geonames/?lastModified=" + maxModifiedDate + "&PageNum=1&PageSize=10");
                
                //foreach (var geoname in geonamesJson)
                //{
                //    var existed 
                //    if (geoname.geonameId)
                //}
                //dbContext.Geoname

                Console.WriteLine(message.Replace("<br>", ""));
                var emailTo = ConfigurationManager.AppSettings.Get("emailRecipientList");
                var subject = ConfigurationManager.AppSettings.Get("emailSubjectUponSuccess");
                var emailList = emailTo.Split(',');
                SendMail(emailList, subject, message);
            }
            catch (Exception ex)
            {
                var message = string.Format("{0}: {1}\n{2}\n", DateTime.Now, ex.Message, ex.StackTrace);
                var emailTo = ConfigurationManager.AppSettings.Get("emailRecipientList");
                var subject = ConfigurationManager.AppSettings.Get("emailSubjectUponError");
                var emailList = emailTo.Split(',');
                SendMail(emailList, subject, message);
            }

        }

        /// <summary>
        /// Gets the json string result asynchronous.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="requestUrl">The request URL.</param>
        /// <returns></returns>
        private static async Task<string> GetJsonStringResultAsync(string baseUrl, string requestUrl)
        {
            string result = string.Empty;
            try
            {
                var userName = ConfigurationManager.AppSettings.Get("userName");
                var password = ConfigurationManager.AppSettings.Get("password");
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
        /// <param name="mailRecipientList">The mail recipient list.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        private static bool SendMail(string[] mailRecipientList, string subject, string message)
        {
            try
            {
                var mail = new MailMessage();
                var currier = new SmtpClient();

                foreach (string recipient in mailRecipientList)
                {
                    mail.To.Add(recipient);
                }
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                currier.Send(mail);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
