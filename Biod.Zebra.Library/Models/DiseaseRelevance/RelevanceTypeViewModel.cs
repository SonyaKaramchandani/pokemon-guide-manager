using Newtonsoft.Json;

namespace Biod.Zebra.Library.Models.DiseaseRelevance
{
    public class RelevanceTypeViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}