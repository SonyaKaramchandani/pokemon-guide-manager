﻿namespace Biod.Insights.Service.Models.HealthCareWorker.DataSystemsApiModels
{
    // Class generated using Unleash OpenSpec Api VStudio extension
    // https://medium.com/@unchase/how-to-generate-c-or-typescript-client-code-for-openapi-swagger-specification-d882d59e3b77
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.11.0 (Newtonsoft.Json v12.0.0.2)")]
    public class RefinementQuestionModel
    {
        [Newtonsoft.Json.JsonProperty("diseaseIdList", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<int> DiseaseIdList { get; set; }

        [Newtonsoft.Json.JsonProperty("activityList", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<ActivityModel> ActivityList { get; set; }

        [Newtonsoft.Json.JsonProperty("diseasesWithVaccines", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<DiseaseModel> DiseasesWithVaccines { get; set; }
    }
}