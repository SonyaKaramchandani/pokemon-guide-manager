using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Westwind.Web.Mvc;
using RestSharp;
using Newtonsoft.Json;

namespace Biod.Zebra.Api.Hcw
{
    /// <summary>
    /// This API call gives next disease symptom and updated association scores.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class HcwGetNextQueryController : ApiController
    {
        // GET api/values
        /// <summary>
        /// Find next disease symptom and updated association scores.
        /// </summary>
        /// <param name="association_score">List of disease identifier, symptomId and association score.</param>
        /// <param name="symptoms_queried">List of symptom identifier, and ansower (yes (1), no (0), I don't know (-1)).</param> 
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        public string Get(string association_score, string symptoms_queried = "")
        {
            var symptomToQuery = new SymptomToQuery()
            {
                association_score = association_score,
                symptoms_queried = symptoms_queried
            };

            var jsonParameter = new JsonParameter("symptomToQuery", symptomToQuery);

            var jsonStringResultAsyncString = JsonStringResultClass.PostGetAzureFunctionResult(
                ConfigurationManager.AppSettings.Get("HcwFunctionBaseUrl"),
                jsonParameter,
                JsonConvert.SerializeObject(symptomToQuery),
                ConfigurationManager.AppSettings.Get(@"HcwFunctionCodeToken"));

            return jsonStringResultAsyncString;
        }
         
        // POST api/values
        /// <summary></summary>
        /// <param name="symptomToQuery">Symptom to query.</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        public string Post([FromBody] SymptomToQuery symptomToQuery)
        {
            var jsonParameter = new JsonParameter("symptomToQuery", symptomToQuery);
            
            var jsonStringResultAsyncString = JsonStringResultClass.PostGetAzureFunctionResult(
                ConfigurationManager.AppSettings.Get("HcwFunctionBaseUrl"),
                jsonParameter,
                JsonConvert.SerializeObject(symptomToQuery),
                ConfigurationManager.AppSettings.Get(@"HcwFunctionCodeToken"));

            return jsonStringResultAsyncString;
        }

        /// <summary>SymptomToQuery</summary>
        public class SymptomToQuery
        {
            public string association_score { get; set; }
            public string symptoms_queried { get; set; }
        }
    }
}