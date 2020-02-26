namespace Biod.Insights.Service.Data.CustomModels
{
    public class GeonameJoinResult
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string DisplayName { get; set; }
        
        public int LocationType { get; set; }
        
        public string CountryName { get; set; }
        
        public string ProvinceName { get; set; }
        
        public float Latitude { get; set; }
        
        public float Longitude { get; set; }
        
        // NOTE: Geometry object cannot be casted from the Column due to discrepancy in orientation (CCW vs CW) 
        //       between NetTopologySuite and MSSQL. Any query to Geographical shapes should do .AsText() to prevent
        //       "shell is empty but holes are not" exception.
        public string ShapeAsText { get; set; }
    }
}