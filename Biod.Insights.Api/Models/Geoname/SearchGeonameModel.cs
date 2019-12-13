using Biod.Insights.Api.Constants;

namespace Biod.Insights.Api.Models.Geoname
{
    public class SearchGeonameModel
    {
        public int GeonameId { get; set; }

        public string Name { get; set; }
        
        public LocationType LocationType { get; set; }
        
        public float Latitude { get; set; }
        
        public float Longitude { get; set; }
    }
}