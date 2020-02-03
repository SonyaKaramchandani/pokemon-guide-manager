//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Biod.Zebra.Library.EntityModels.Zebra
{
    using System;
    
    public partial class usp_ZebraEmailGetEventByEventId_Result
    {
        public int DiseaseId { get; set; }
        public string DiseaseName { get; set; }
        public string MicrobeType { get; set; }
        public string TransmittedBy { get; set; }
        public string IncubationPeriod { get; set; }
        public string Vaccination { get; set; }
        public string Brief { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string Reasons { get; set; }
        public string ExportationPriorityTitle { get; set; }
        public string Email { get; set; }
        public string EventTitle { get; set; }
        public Nullable<int> EventId { get; set; }
        public Nullable<int> OutbreakPotentialAttributeId { get; set; }
        public string ExportationProbabilityName { get; set; }
        public Nullable<bool> IsPaidUser { get; set; }
        public Nullable<bool> DoNotTrackEnabled { get; set; }
        public Nullable<bool> EmailConfirmed { get; set; }
        public string UserAoiLocationNames { get; set; }
        public string UserId { get; set; }
        public string AoiGeonameIds { get; set; }
        public int IsLocal { get; set; }
        public int RelevanceId { get; set; }
        public bool IsLocalOnly { get; set; }
    }
}
