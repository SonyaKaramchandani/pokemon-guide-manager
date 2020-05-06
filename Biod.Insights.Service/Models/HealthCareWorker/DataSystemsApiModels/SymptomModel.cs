using System;
using System.Collections.Generic;
using System.Text;

namespace Biod.Insights.Service.Models.HealthCareWorker.DataSystemsApiModels
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.11.0 (Newtonsoft.Json v12.0.0.2)")]
    public class SymptomModel
    {
        /// <summary>Unique BlueDot Id of an activity, (int)</summary>
        [Newtonsoft.Json.JsonProperty("symptomId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? SymptomId { get; set; }

        /// <summary>The label to be displayed for each activity-related refinement question</summary>
        [Newtonsoft.Json.JsonProperty("symptom", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Symptom { get; set; }

        /// <summary>Unique BlueDot Id of a symptom category, (int)</summary>
        [Newtonsoft.Json.JsonProperty("symptomCategoryId", Required = Newtonsoft.Json.Required.Always)]
        public int SymptomCategoryId { get; set; }

        /// <summary>Symptom category associated with a symptom, (varchar(256))</summary>
        [Newtonsoft.Json.JsonProperty("symptomCategory", Required = Newtonsoft.Json.Required.Always)]
        public string SymptomCategory { get; set; }
    }
}