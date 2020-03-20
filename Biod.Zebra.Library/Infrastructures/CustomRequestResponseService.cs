// This code requires the Nuget package Microsoft.AspNet.WebApi.Client to be installed.
// Instructions for doing this in Visual Studio:
// Tools -> Nuget Package Manager -> Package Manager Console
// Install-Package Microsoft.AspNet.WebApi.Client

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Biod.Zebra.Library.Infrastructures
{
    public class CustomRequestResponseService : IZebraUpdateService
    {
        //ZebraPrevalence: this service is for testing the R service invocation. It is not used in the Insights application
        public async Task GetPrevalanceService()
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"ismin", "cases", "pop.size", "event.end", "event.start", "disease.incubation", "disease.symptomatic"},
                                //Values = new string[,] {  { "0", "0", "0", "value", "value", "0", "0" },  { "0", "0", "0", "value", "value", "0", "0" },  }
                                Values = new string[,] {  { "0", "50", "5000", "2018-12-10", "2018-12-10", "5", "5" } }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                const string apiKey = "+HbAy3OVIpunRCsU4fMGzjeKvITuD/f4NI9cRjq0yhl1oQ5OHzg2R/9bZpJqZdzTwzmaIpmd37IaHk7u/Ry4YQ=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                //Workspace ID =  9c7aaf870f5c4af5948e59c27edce85c
                //Subscription ID =   fe195812-fa9a-4372-81fa-f9c917526864
                //Primary Authorization Token =   YS5/+4YHjwT3Yvb+lveOXiLa7CB7d0b9StbjhrrXDxpRjGqNX2wUPZUEH//Z4gsV3dA3muKLdWyHzC1m/Enslw==

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/9c7aaf870f5c4af5948e59c27edce85c/services/fe657dce7b7a4d8eb78f323bc1ea0aa9/execute?api-version=2.0&details=false");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)


                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    AzureMLServiceResult azureMLServiceResult = JsonConvert.DeserializeObject<AzureMLServiceResult>(result); //result shows error because it needs string as parameter.
                    var value = azureMLServiceResult.Results.output1.value.Values[0][0];

                    Console.WriteLine("Result: {0}", result);
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
            }
        }

        //ZebraMinMaxCases
        public async Task<string> GetMinMaxCasesService(string gridId, string cases)
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"gridid", "cases"},
                                Values = new string[,] {  { gridId, cases }  }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                string apiKey = ConfigurationManager.AppSettings.Get("ZebraMinMaxCasesApiKey"); // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                //client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("AzureMLBaseAddress"));
                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/" + 
                    ConfigurationManager.AppSettings.Get("ZebraMinMaxCasesWorkspaceId") + "/services/" +
                    ConfigurationManager.AppSettings.Get("ZebraMinMaxCasesServiceId") + "/execute?api-version=2.0&details=true");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)


                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    AzureMLServiceResult azureMLServiceResult = JsonConvert.DeserializeObject<AzureMLServiceResult>(result); //result shows error because it needs string as parameter.
                    var resultCsv = string.Join(",", azureMLServiceResult.Results.output1.value.Values[0]);
                    return resultCsv;
                    //Console.WriteLine("Result: {0}", result);
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                    //Console.WriteLine(responseContent);
                }
            }
        }

        //ZebraMinMaxPrevalence
        public async Task<string> GetMinMaxPrevalenceService(string mineventcasesoverpopsize, string maxeventcasesoverpopsize,
            string diseaseincubation, string diseasesymptomatic, string eventstartdate, string eventenddate)
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"mineventcasesoverpopsize", "maxeventcasesoverpopsize", "diseaseincubation", "diseasesymptomatic", "eventstartdate", "eventenddate"},
                                //Values = new string[,] {  { "0", "0", "0", "0", "value", "value" },  { "0", "0", "0", "0", "value", "value" },  }
                                Values = new string[,] {  { mineventcasesoverpopsize, maxeventcasesoverpopsize, diseaseincubation, diseasesymptomatic, eventstartdate, eventenddate } }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                string apiKey = ConfigurationManager.AppSettings.Get("ZebraMinMaxPrevalenceApiKey"); // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                //client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("AzureMLBaseAddress"));
                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/" +
                    ConfigurationManager.AppSettings.Get("ZebraMinMaxPrevalenceWorkspaceId") + "/services/" +
                    ConfigurationManager.AppSettings.Get("ZebraMinMaxPrevalenceServiceId") + "/execute?api-version=2.0&details=true");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)


                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    AzureMLServiceResult azureMLServiceResult = JsonConvert.DeserializeObject<AzureMLServiceResult>(result); //result shows error because it needs string as parameter.
                    var resultCsv = string.Join(",", azureMLServiceResult.Results.output1.value.Values[0]);
                    return resultCsv;
                    //Console.WriteLine("Result: {0}", result);
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                    //Console.WriteLine(responseContent);
                }
            }
        }

        //InsightsMinMaxPrevalence for spreadMd
        public async Task<string> GetInsightsMinMaxPrevalenceService(string mineventcasesoverpopsize, string maxeventcasesoverpopsize,
            string diseaseincubation, string diseasesymptomatic, string eventstartdate, string eventenddate)
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"mineventcasesoverpopsize", "maxeventcasesoverpopsize", "diseaseincubation", "diseasesymptomatic", "eventstartdate", "eventenddate"},
                                //Values = new string[,] {  { "0", "0", "0", "0", "value", "value" },  { "0", "0", "0", "0", "value", "value" },  }
                                Values = new string[,] {  { mineventcasesoverpopsize, maxeventcasesoverpopsize, diseaseincubation, diseasesymptomatic, eventstartdate, eventenddate } }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                string apiKey = ConfigurationManager.AppSettings.Get("InsightsMinMaxPrevalenceApiKey"); // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                //client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("AzureMLBaseAddress"));
                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/" +
                    ConfigurationManager.AppSettings.Get("InsightsMinMaxPrevalenceWorkspaceId") + "/services/" +
                    ConfigurationManager.AppSettings.Get("InsightsMinMaxPrevalenceServiceId") + "/execute?api-version=2.0&details=true");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)


                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    AzureMLServiceResult azureMLServiceResult = JsonConvert.DeserializeObject<AzureMLServiceResult>(result); //result shows error because it needs string as parameter.
                    var resultCsv = string.Join(",", azureMLServiceResult.Results.output1.value.Values[0]);
                    return resultCsv;
                    //Console.WriteLine("Result: {0}", result);
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                    //Console.WriteLine(responseContent);
                }
            }
        }
    }

    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }

    public interface IZebraUpdateService
    {
        /// <returns>The following string format: "[GridId],[Cases],[MinCases],[MaxCases]"</returns>
        Task<string> GetMinMaxCasesService(string gridId, string cases);

        /// <returns>The following string format: "[MinPrevalence],[MaxPrevalence]"</returns>
        Task<string> GetMinMaxPrevalenceService(string mineventcasesoverpopsize, string maxeventcasesoverpopsize,
            string diseaseincubation, string diseasesymptomatic, string eventstartdate, string eventenddate);

        /// <returns>The following string format: "[MinPrevalence],[MaxPrevalence]"</returns>
        Task<string> GetInsightsMinMaxPrevalenceService(string mineventcasesoverpopsize, string maxeventcasesoverpopsize,
            string diseaseincubation, string diseasesymptomatic, string eventstartdate, string eventenddate);
    }

}

