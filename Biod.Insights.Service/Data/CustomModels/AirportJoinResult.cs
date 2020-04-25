namespace Biod.Insights.Service.Data.CustomModels
{
    public class AirportJoinResult
    {
        public bool IsModelNotRun { get; set; }

        public int StationId { get; set; }

        public string StationName { get; set; }

        public string StationCode { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public int Volume { get; set; }

        public float MinProb { get; set; }

        public float MaxProb { get; set; }

        public float MinExpVolume { get; set; }

        public float MaxExpVolume { get; set; }

        public int? CityGeonameId { get; set; }

        public string CityName { get; set; }
    }
}