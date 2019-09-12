using RestSharp;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Biod.Zebra.Library.Infrastructures.Log;

namespace Biod.Zebra.Library.Infrastructures
{
    public static class JsonStringResultClass
    {
        private static readonly ILogger Logger = Log.Logger.GetLogger(typeof(JsonStringResultClass).ToString());

        /// <summary>
        /// Gets the json string result asynchronous.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="requestUrl">The request URL.</param>
        /// <returns></returns>
        public static async Task<string> GetJsonStringResultAsync(string baseUrl, string requestUrl, string userName, string password)
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
                    HttpResponseMessage response = client.GetAsync(baseUrl + requestUrl).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error: " + ex.Message + "\n" + ex.InnerException);
                return ex.Message;
            }
        }

        public static async Task<string> PostJsonStringResultAsync(
            string baseUrl, string requestUrl, string userName, string password,
            string jsonStringParameter)
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
                    
                    HttpResponseMessage response = client.PostAsJsonAsync(baseUrl + requestUrl, jsonStringParameter).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error: " + ex.Message + "\n" + ex.InnerException);
                return ex.Message;
            }
        }

        public static string PostGetAzureFunctionResult(string baseUrl, JsonParameter jsonParameter, string jsonStringParameter, string token)
        {
            string result = string.Empty;
            try
            {
                var client = new RestClient(baseUrl + "?code=" + token);
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Content-Type", "application/json");
                //request.AddParameter("undefined", "{\n\t\"association_score\":\"0,0,5,0,1,1,1,0,2\",\n\t\"symptoms_queried\":\"0,1\"\n}", ParameterType.RequestBody);
                request.AddParameter("undefined", jsonStringParameter, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                return response.Content;
            }
            catch (Exception ex)
            {
                Logger.Error("Error: " + ex.Message + "\n" + ex.InnerException);
                return ex.Message;
            }
        }
    }
}