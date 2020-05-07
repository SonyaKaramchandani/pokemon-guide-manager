using System.Collections.Generic;
using Newtonsoft.Json;

namespace Biod.Insights.Service.Models.Disease
{
    public class DiseaseGroupModel
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<DiseaseGroupModel> SubGroups { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<int> DiseaseIds { get; set; }
    }
}