using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Biod.Zebra.Library.Models.DiseaseRelevance
{
    public class DiseaseGroupTypeViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("diseaseGroups")]
        public List<DiseaseGroupViewModel> DiseaseGroups { get; set; }
    }
}