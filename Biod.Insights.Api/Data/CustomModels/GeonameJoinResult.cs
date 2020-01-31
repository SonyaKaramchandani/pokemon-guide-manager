using Biod.Insights.Api.Data.EntityModels;
using NetTopologySuite.Geometries;

namespace Biod.Insights.Api.Data.CustomModels
{
    public class GeonameJoinResult
    {
        public Geonames Geoname { get; set; }
        
        // NOTE: Geometry object cannot be casted from the Column due to discrepancy in orientation (CCW vs CW) 
        //       between NetTopologySuite and MSSQL. Any query to Geographical shapes should do .AsText() to prevent
        //       "shell is empty but holes are not" exception.
        public string ShapeAsText { get; set; }
    }
}