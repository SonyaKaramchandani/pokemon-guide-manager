namespace Biod.Insights.Service.Models.HealthCareWorker.DataSystemsApiModels
{
    // Class generated using Unleash OpenSpec Api VStudio extension
    // https://medium.com/@unchase/how-to-generate-c-or-typescript-client-code-for-openapi-swagger-specification-d882d59e3b77
    public class RefinementActivitiesAndVaccinesHealthCareWorkerModel : HealthCareWorkerModel
    {
        /// <summary>Indicates whether a disease was refined based on the activities the patient was involved</summary>
        [Newtonsoft.Json.JsonProperty("refinedByActivity", Required = Newtonsoft.Json.Required.Always)]
        public bool RefinedByActivity { get; set; }

        /// <summary>A activity multiplier for a disease calculated based on involved activities. If multiple activities are related to a disease, the multiplier for each activity are multiplied together, (float)</summary>
        [Newtonsoft.Json.JsonProperty("activityMultiplier", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double? ActivityMultiplier { get; set; }

        /// <summary>Indicates whether a disease was refined based on the vaccines the patient had</summary>
        [Newtonsoft.Json.JsonProperty("refinedByPrevention", Required = Newtonsoft.Json.Required.Always)]
        public bool RefinedByPrevention { get; set; }

        /// <summary>The rank change after refinement 2 questions. Negative value means disease moved down (rank increases), positive value means disease moved up (rank decreases), 0 means either no change in rank or not affected by refinement questions, (int)</summary>
        [Newtonsoft.Json.JsonProperty("changeInRankPostActivityAndVaccine", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? ChangeInRankPostActivityAndVaccine { get; set; }

        /// <summary>A HCW value that shows indicates the risk and probability of getting a disease, calculated by multiplying orignal acquisitionRiskOutput with exposure risk factors (activityMultiplier and vaccine riskReduction) together, (float)</summary>
        [Newtonsoft.Json.JsonProperty("acquisitionRiskOutputRefined", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double? AcquisitionRiskOutputRefined { get; set; }

        /// <summary>The rank or order of a disease in HCW output after refinement questions related to activities and vaccines, (int)</summary>
        [Newtonsoft.Json.JsonProperty("rankPostActivityAndVaccine", Required = Newtonsoft.Json.Required.Always)]
        public int RankPostActivityAndVaccine { get; set; }
    }
}