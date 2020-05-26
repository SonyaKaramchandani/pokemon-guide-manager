namespace Biod.Insights.Service.Models.HealthCareWorker.DataSystemsApiModels
{
    public class RefinementSymptomsHealthCareWorkerModel : HealthCareWorkerModel
    {
        /// <summary>Indicates whether a disease was refined based on the symptoms the patient was presented</summary>
        [Newtonsoft.Json.JsonProperty("refinedBySymptom", Required = Newtonsoft.Json.Required.Always)]
        public bool RefinedBySymptom { get; set; }

        /// <summary>The rank change after refinement 1 questions. Negative value means disease moved down (rank increases), positive value means disease moved up (rank decreases), 0 means either no change in rank or not affected by refinement questions, (int)</summary>
        [Newtonsoft.Json.JsonProperty("changeInRankPostSymptoms", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? ChangeInRankPostSymptoms { get; set; }

        /// <summary>The rank or order of a disease in HCW output after refinement questions related to symptoms, (int)</summary>
        [Newtonsoft.Json.JsonProperty("rankPostSymptoms", Required = Newtonsoft.Json.Required.Always)]
        public int RankPostSymptoms { get; set; }
    }
}