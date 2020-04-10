namespace Biod.Insights.Service.Models.HealthCareWorker.DataSystemsApiModels
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.11.0 (Newtonsoft.Json v12.0.0.2)")]
    public class ActivityModel
    {
        /// <summary>Unique BlueDot Id of an activity, (int)</summary>
        [Newtonsoft.Json.JsonProperty("activityId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? ActivityId { get; set; }

        /// <summary>A travel-related activity that can modify the risk of disease for an individual, (varchar(64))</summary>
        [Newtonsoft.Json.JsonProperty("activity", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Activity { get; set; }
    }
}