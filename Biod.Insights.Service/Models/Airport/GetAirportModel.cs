using Newtonsoft.Json;

namespace Biod.Insights.Service.Models.Airport
{
    public class GetAirportModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Code { get; set; }
        
        public float Latitude { get; set; }
        
        public float Longitude { get; set; }
        
        public int Volume { get; set; }
        
        public string City { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public RiskModel ExportationRisk { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public RiskModel ImportationRisk { get; set; }
    }
}