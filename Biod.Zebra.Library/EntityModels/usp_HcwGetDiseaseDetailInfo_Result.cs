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
    
    public partial class usp_HcwGetDiseaseDetailInfo_Result
    {
        public int DiseaseId { get; set; }
        public string DiseaseName { get; set; }
        public string TransmissionMode { get; set; }
        public string Incubation { get; set; }
        public string Vaccination { get; set; }
        public Nullable<int> SymptomId { get; set; }
        public string Symptom { get; set; }
        public Nullable<int> AssociationScore { get; set; }
        public string Agents { get; set; }
        public string AgentTypes { get; set; }
    }
}
