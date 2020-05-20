namespace Biod.Insights.Service.Models.Event
{
    public class ProximalCaseCountModel
    {
        public int EventId { get; set; }

        public int ProximalCases { get; set; }

        public int TotalEventCases { get; set; }

        public int LocationId { get; set; }

        public int LocationType { get; set; }

        public string LocationName { get; set; }

        public bool IsWithinLocation { get; set; }
    }
}