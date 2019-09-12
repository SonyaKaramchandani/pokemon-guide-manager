using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Biod.Zebra.Library.Models.DiseaseRelevance
{
    public class RelevanceViewModel
    {
        [Required]
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [Required]
        [JsonProperty("diseaseSetting")]
        public Dictionary<int, DiseaseRelevanceSettingViewModel> DiseaseSetting { get; set; }
        
        public class DiseaseRelevanceSettingViewModel
        {
            [Required]
            [JsonProperty("id")]
            public int DiseaseId { get; set; }
        
            [JsonProperty("name")]
            public string DiseaseName { get; set; }
        
            [Required]
            [JsonProperty("relevanceType")]
            public int RelevanceType { get; set; }
        
            [JsonProperty("state")]
            public int? State { get; set; }
        }
    }
}