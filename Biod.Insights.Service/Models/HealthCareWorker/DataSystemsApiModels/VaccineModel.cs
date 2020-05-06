namespace Biod.Insights.Service.Models.HealthCareWorker.DataSystemsApiModels
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.11.0 (Newtonsoft.Json v12.0.0.2)")]
    public partial class VaccineModel
    {
        /// <summary>Unique BlueDot Id of a disease, (int)</summary>
        [Newtonsoft.Json.JsonProperty("diseaseId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? DiseaseId { get; set; }

        /// <summary>Name of the disease, (varchar(64))</summary>
        [Newtonsoft.Json.JsonProperty("disease", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Disease { get; set; }

        /// <summary>The label to be displayed for each vaccine-related refinement question</summary>
        [Newtonsoft.Json.JsonProperty("vaccineLabel", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string VaccineLabel { get; set; }

        /// <summary>The refinement question related to exposure risk factors or vaccines, varchar(max)</summary>
        [Newtonsoft.Json.JsonProperty("question", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Question { get; set; }
    }
}