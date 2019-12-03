namespace Biod.Insights.Api.Models
{
    public class GetGeonameModel
    {
        public int GeonameId { get; set; }

        public int LocationType { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }
    }
}