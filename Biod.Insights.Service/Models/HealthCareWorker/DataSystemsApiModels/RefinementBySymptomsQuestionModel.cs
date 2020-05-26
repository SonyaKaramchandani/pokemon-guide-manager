using System;
using System.Collections.Generic;
using System.Text;

namespace Biod.Insights.Service.Models.HealthCareWorker.DataSystemsApiModels
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.11.0 (Newtonsoft.Json v12.0.0.2)")]
    public class RefinementBySymptomsQuestionModel
    {
        [Newtonsoft.Json.JsonProperty("diseaseIdList", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<int> DiseaseIdList { get; set; }

        [Newtonsoft.Json.JsonProperty("activities", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<SymptomModel> Symptoms { get; set; }
    }
}