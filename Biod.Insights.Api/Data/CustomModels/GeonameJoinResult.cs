using Biod.Insights.Api.Data.EntityModels;
using NetTopologySuite.Geometries;

namespace Biod.Insights.Api.Data.CustomModels
{
    public class GeonameJoinResult
    {
        public Geonames Geoname { get; set; }
        
        public Geometry Shape { get; set; }
    }
}