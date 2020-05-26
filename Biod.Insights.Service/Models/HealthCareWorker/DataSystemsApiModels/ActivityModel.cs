namespace Biod.Insights.Service.Models.HealthCareWorker.DataSystemsApiModels
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.11.0 (Newtonsoft.Json v12.0.0.2)")]
    public class ActivityModel
    {
        /// <summary>Unique BlueDot Id of an activity, (int)</summary>
        [Newtonsoft.Json.JsonProperty("activityId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? ActivityId { get; set; }

        /// <summary>The label to be displayed for each activity-related refinement question</summary>
        [Newtonsoft.Json.JsonProperty("activityLabel", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ActivityLabel { get; set; }

        /// <summary>The refinement question related to exposure risk factors or vaccines, varchar(max)</summary>
        [Newtonsoft.Json.JsonProperty("question", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Question { get; set; }
    }
}