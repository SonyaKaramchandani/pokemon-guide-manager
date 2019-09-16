//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Biod.Zebra.Library.EntityModels
{
    using System;
    
    public partial class usp_ZebraEventGetEventSummary_Result
    {
        public Nullable<int> EventId { get; set; }
        public string EventTitle { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public string CountryName { get; set; }
        public string CountryCentroidAsText { get; set; }
        public string ExportationPriorityTitle { get; set; }
        public string Summary { get; set; }
        public string Notes { get; set; }
        public Nullable<bool> HasOutlookReport { get; set; }
        public bool IsLocalOnly { get; set; }
        public Nullable<int> DiseaseId { get; set; }
        public string DiseaseName { get; set; }
        public Nullable<int> OutbreakPotentialAttributeId { get; set; }
        public string BiosecurityRisk { get; set; }
        public string Transmissions { get; set; }
        public Nullable<int> RepCases { get; set; }
        public Nullable<int> Deaths { get; set; }
        public string ExportationProbabilityName { get; set; }
        public Nullable<decimal> ExportationProbabilityMin { get; set; }
        public Nullable<decimal> ExportationProbabilityMax { get; set; }
        public Nullable<decimal> ExportationInfectedTravellersMin { get; set; }
        public Nullable<decimal> ExportationInfectedTravellersMax { get; set; }
        public Nullable<decimal> ImportationMaxProbability { get; set; }
        public Nullable<decimal> ImportationMinProbability { get; set; }
        public Nullable<decimal> ImportationInfectedTravellersMax { get; set; }
        public Nullable<decimal> ImportationInfectedTravellersMin { get; set; }
        public Nullable<int> LocalSpread { get; set; }
        public string Interventions { get; set; }
    }
}
