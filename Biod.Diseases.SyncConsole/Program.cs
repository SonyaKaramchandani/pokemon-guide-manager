﻿using Biod.Diseases.SyncConsole.EntityModels;
using Biod.Diseases.SyncConsole.Infrastructures;
using Biod.Diseases.SyncConsole.Models;
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

namespace Biod.Diseases.SyncConsole
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
                var apiUserName = ConfigurationManager.AppSettings.Get("userName");
                var apiPassword = ConfigurationManager.AppSettings.Get("password");
                var serviceBaseUrlProd = ConfigurationManager.AppSettings.Get("serviceBaseUrlProd");
                Console.WriteLine("Get Diseases json string Async...");
                string diseasesJsonString = GetJsonStringResultAsync(serviceBaseUrlProd, "Diseases/Diseases",
                apiUserName, apiPassword).Result;
                var diseasesJson = JsonConvert.DeserializeObject<IList<DiseaseClass>>(diseasesJsonString);

                string symptomsJsonString = GetJsonStringResultAsync(serviceBaseUrlProd, "Diseases/Symptoms",
                apiUserName, apiPassword).Result;
                var symptomsJson = JsonConvert.DeserializeObject<IList<SymptomClass>>(symptomsJsonString);

                string systemsJsonString = GetJsonStringResultAsync(serviceBaseUrlProd, "Diseases/Systems",
                apiUserName, apiPassword).Result;
                var systemsJson = JsonConvert.DeserializeObject<IList<SystemClass>>(systemsJsonString);

                //George disease json
                string georgeModifierJsonString = GetJsonStringResultAsync(serviceBaseUrlProd, "Diseases/GeorgeModifiers",
                apiUserName, apiPassword).Result;
                var georgeModifierJson = JsonConvert.DeserializeObject<IList<GeorgeModifierClass>>(georgeModifierJsonString);

                string georgeMessagingJsonString = GetJsonStringResultAsync(serviceBaseUrlProd, "Diseases/GeorgeMessaging",
                apiUserName, apiPassword).Result;
                var georgeMessagingJson = JsonConvert.DeserializeObject<IList<GeorgeMessagingClass>>(georgeMessagingJsonString);

                // update databases
                BiodSurveillanceModelEntities surveillanceDbContext = new BiodSurveillanceModelEntities();
                BiodZebraModelEntities zebraDbContext = new BiodZebraModelEntities();
                BiodGeorgeModelEntities georgeDbContext = new BiodGeorgeModelEntities();
                zebraDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("commandTimeout"));
                surveillanceDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("commandTimeout"));
                georgeDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("commandTimeout"));


                var ResultMessage = new List<string>();
                string message;

                ////in zebra for disease
                ResultMessage = zebraDbContext.usp_UpdateDiseaseApi_main(diseasesJsonString, symptomsJsonString, systemsJsonString).ToList();
                message = "Biod Zebra Diseases Sync Console Successfully Executed!";
                foreach (var r in ResultMessage)
                {
                    message += "<br>" + Environment.NewLine + r;
                }

                //in surveillance for disease
                ResultMessage = surveillanceDbContext.usp_UpdateDiseaseApi_main(diseasesJsonString, symptomsJsonString, systemsJsonString).ToList();
                message += "<br>" + Environment.NewLine + "Biod Surveillance Diseases Sync Console Successfully Executed!";
                foreach (var r in ResultMessage)
                {
                    message += "<br>" + Environment.NewLine + r;
                }

                //in george for disease extension
                ResultMessage = georgeDbContext.usp_PullRegularTables(symptomsJsonString, diseasesJsonString, georgeMessagingJsonString, georgeModifierJsonString).ToList();
                message += "<br>" + Environment.NewLine + "George Diseases Sync Console Successfully Executed!!";
                foreach (var r in ResultMessage)
                {
                    message += "<br>" + Environment.NewLine + r;
                }


                //disease maps: pulling data from ds api takes a while, so put it at last
                var mapUserName = ConfigurationManager.AppSettings.Get("userNameMap");
                var mapPassword = ConfigurationManager.AppSettings.Get("passwordMap");

                var productSourceDbContext = new BiodProductSourceModelEntities();
                string productSourceReturnMessage = "";
                string tblSurfix = "";
                string syncToProductSource = ConfigurationManager.AppSettings.Get("syncToProductSource");
                if (syncToProductSource == "dev" || syncToProductSource == "prod")
                {
                    //2.1 map111
                    //find out if data updated
                    var map111NeedUpate = productSourceDbContext.usp_GeorgeGetDiseaseMapUpdateInfo(serviceBaseUrlProd, 111).FirstOrDefault();
                    if (map111NeedUpate == null ? false : Convert.ToBoolean(map111NeedUpate))
                    {
                        //1. clean storage in dev/staging
                        if (syncToProductSource == "dev")
                        {
                            productSourceDbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE bd.DiseaseMapMaxValue_111_dev");
                            productSourceDbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE bd.DiseaseMapMaxValue_Country_dev");
                            tblSurfix = "_dev";
                        }
                        else if (syncToProductSource == "prod") //clean storage in uat/prod
                        {
                            productSourceDbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE bd.DiseaseMapMaxValue_111");
                            productSourceDbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE bd.DiseaseMapMaxValue_Country");
                        }
                        
                        //2. pull data from api to storage
                        productSourceReturnMessage = GetJsonStringResultAsync(
                            serviceBaseUrlProd,
                            "Models/ModelOutput/Geometry?modelOutputId=111&outFormat=SQL%20Server&emailTo=vivian%40bluedot.global&projection=EPSG%3A4326&outTable=bd.diseaseMapMaxValue_111"
                            + tblSurfix + "&writeDB=%20db1dev.BiodProductSource%20as%20bd&createSpatialIndex=No",
                            mapUserName, mapPassword).Result;
                    }

                    //2.2 map107 GCS
                    //find out if data updated
                    var map107NeedUpate = productSourceDbContext.usp_GeorgeGetDiseaseMapUpdateInfo(serviceBaseUrlProd, 107).FirstOrDefault();
                    if (map107NeedUpate == null ? false : Convert.ToBoolean(map107NeedUpate))
                    {
                        //1. clean storage in dev/staging
                        if (syncToProductSource == "dev")
                        {
                            productSourceDbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE bd.DiseaseMap_107_dev");
                            tblSurfix = "_dev";
                        }
                        else if (syncToProductSource == "prod") //clean storage in uat/prod
                        {
                            productSourceDbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE bd.DiseaseMap_107");
                        }
                        productSourceReturnMessage += "<br>" + Environment.NewLine
                            + GetJsonStringResultAsync(
                                    serviceBaseUrlProd,
                                    "Models/ModelOutput/Geometry?modelOutputId=107&outFormat=SQL%20Server&emailTo=vivian%40bluedot.global&projection=EPSG%3A4326&outTable=bd.diseaseMap_107"
                                    + tblSurfix + "&writeDB=db1dev.BiodProductSource%20as%20bd&createSpatialIndex=No",
                                    mapUserName, mapPassword).Result;
                    }
                    message += "<br>" + Environment.NewLine + productSourceReturnMessage;
                }

                //send email
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
        private static async Task<string> GetJsonStringResultAsync(string baseUrl, string requestUrl, string userName, string password)
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


        //private static async Task<string> GetJsonStringResultAsync(string baseUrl, string requestUrl)
        //{
        //    string result = string.Empty;
        //    try
        //    {
        //        //http://dw1-ubuntu.ad.bluedot.global:81/api/v1/Diseases/Diseases
        //        var userName = ConfigVariables.userName;
        //        var password = ConfigVariables.password;
        //        var credentials = new NetworkCredential(userName, password);
        //        var handler = new HttpClientHandler { Credentials = credentials };
        //        using (var client = new HttpClient(handler))
        //        {
        //            client.BaseAddress = new Uri(baseUrl);
        //            client.DefaultRequestHeaders.Accept.Clear();
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            HttpResponseMessage response = await client.GetAsync(requestUrl);
        //            var jsonObject = new Geonames();
        //            if (response.IsSuccessStatusCode)
        //            {
        //                //var serializer = new JavaScriptSerializer();
        //                //var jsonString = await response.Content.ReadAsStringAsync();
        //                //IList<RootObject> geoNames = serializer.Deserialize<IList<RootObject>>(jsonString);

        //                string jsonStr = await response.Content.ReadAsStringAsync();
        //                var objResponse1 = JsonConvert.DeserializeObject<IList<GeoNamesClass>>(jsonStr);



        //                //jsonObject = await response.Content.ReadAsAsync<Geonames>();
        //                //result = await response.Content.ReadAsStringAsync();
        //            }
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        //Logging.Log(ex, "Error on record #" + recordNumber + ":" + sqlStatement);
        //        return ex.Message;
        //    }
        //}

        //private static async Task<IEnumerable<Diseases>> GetAsync()
        //{
        //    string baseUrl = "http://dw1-ubuntu.ad.bluedot.global:81/api/v1/Diseases/Diseases";
        //    var bluedotInApi = new BlueDotIncAPI();
        //    bluedotInApi.BaseUri = new Uri(baseUrl);
        //    using (var client = bluedotInApi)
        //    {
        //        var results = await client.Get<Diseases>(); //.ApiCartoonGetAsync();
        //        IEnumerable<CartoonCharacter> comic = results.Select(m => new CartoonCharacter
        //        {
        //            Name = m.Name,
        //            PictureUrl = m.PictureUrl
        //        });
        //        return comic;
        //    }
        //}

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