using Newtonsoft.Json;

namespace Biod.Insights.Api.Models.Geoname
{
    public class GetGeonameModel
    {
        public int GeonameId { get; set; }

        public int LocationType { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }
        
        public string Province { get; set; }
        
        public float Latitude { get; set; }
        
        public float Longitude { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Shape { get; set; }
    }
}