using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Biod.Zebra.Library.Models.DiseaseRelevance
{
    public class DiseaseGroupViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("diseaseIds")]
        public List<int> DiseaseIds { get; set; }
        
        public IEnumerable<SelectListItem> DiseaseList { get; set; }
    }
}