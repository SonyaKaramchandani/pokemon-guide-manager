using System.ComponentModel.DataAnnotations.Schema;

namespace Biod.Insights.Data.CustomModels
{
    public class usp_ZebraEventGetEventSummary_Result
    {
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime? EndDate { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public string CountryName { get; set; }
        public string CountryCentroidAsText { get; set; }
        public string ExportationPriorityTitle { get; set; }
        public string Summary { get; set; }
        public string Notes { get; set; }
        public bool IsLocalOnly { get; set; }
        public int? DiseaseId { get; set; }
        public string DiseaseName { get; set; }
        public int? OutbreakPotentialAttributeId { get; set; }
        public string BiosecurityRisk { get; set; }
        public string Transmissions { get; set; }
        public int? RepCases { get; set; }
        public int? Deaths { get; set; }
        public string ExportationProbabilityName { get; set; }
        [Column(TypeName = "decimal(5,4)")]
        public decimal? ExportationProbabilityMin { get; set; }
        [Column(TypeName = "decimal(5,4)")]
        public decimal? ExportationProbabilityMax { get; set; }
        [Column(TypeName = "decimal(10,3)")]
        public decimal? ExportationInfectedTravellersMin { get; set; }
        [Column(TypeName = "decimal(10,3)")]
        public decimal? ExportationInfectedTravellersMax { get; set; }
        [Column(TypeName = "decimal(5,4)")]
        public decimal? ImportationMaxProbability { get; set; }
        [Column(TypeName = "decimal(5,4)")]
        public decimal? ImportationMinProbability { get; set; }
        [Column(TypeName = "decimal(10,3)")]
        public decimal? ImportationInfectedTravellersMax { get; set; }
        [Column(TypeName = "decimal(10,3)")]
        public decimal? ImportationInfectedTravellersMin { get; set; }
        public int? LocalSpread { get; set; }
        public string Interventions { get; set; }
    }
}