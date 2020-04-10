namespace Biod.Insights.Service.Models.HealthCareWorker.DataSystemsApiModels
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.11.0 (Newtonsoft.Json v12.0.0.2)")]
    public class DiseaseModel
    {
        /// <summary>Unique BlueDot Id of a disease, (int)</summary>
        [Newtonsoft.Json.JsonProperty("diseaseId", Required = Newtonsoft.Json.Required.Always)]
        public int DiseaseId { get; set; }

        /// <summary>Name of the disease, (varchar(64))</summary>
        [Newtonsoft.Json.JsonProperty("disease", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string Disease { get; set; }
    }
}