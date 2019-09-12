using Newtonsoft.Json;

namespace Biod.Zebra.Library.Models.DiseaseRelevance
{
    public class DiseaseViewModel
    {
        [JsonProperty("id")]
        public int DiseaseId { get; set; }
        
        [JsonProperty("name")]
        public string DiseaseName { get; set; }
        
        [JsonProperty("type")]
        public string DiseaseType { get; set; }
    }
}