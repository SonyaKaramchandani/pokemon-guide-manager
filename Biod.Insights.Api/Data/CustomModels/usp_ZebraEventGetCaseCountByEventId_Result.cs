namespace Biod.Insights.Api.Data.CustomModels
{
    public class usp_ZebraEventGetCaseCountByEventId_Result
    {
        public int GeonameId { get; set; }
        public int? ConfCases { get; set; }
        public int? SuspCases { get; set; }
        public int? RepCases { get; set; }
        public int? Deaths { get; set; }
        public string LocationName { get; set; }
        public string LocationType { get; set; }
        public bool? RepCasesIsRaw { get; set; }
        public bool? ConfCasesIsRaw { get; set; }
        public bool? DeathsIsRaw { get; set; }
        public bool? SuspCasesIsRaw { get; set; }
        public string ProvinceName { get; set; }
    }
}