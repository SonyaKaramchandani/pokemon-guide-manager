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
    
    public partial class usp_ZebraEmailGetWeeklyEmailData_Result
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public Nullable<bool> IsPaidUser { get; set; }
        public Nullable<bool> DoNotTrackEnabled { get; set; }
        public string UserAoiLocationNames { get; set; }
        public Nullable<int> EventId { get; set; }
        public string EventTitle { get; set; }
        public Nullable<bool> IsNewEvent { get; set; }
        public Nullable<int> RepCases { get; set; }
        public int DeltaNewRepCases { get; set; }
        public int DeltaNewDeaths { get; set; }
        public int LocalSpread { get; set; }
        public Nullable<decimal> MaxProb { get; set; }
        public Nullable<decimal> MinProb { get; set; }
        public Nullable<decimal> MaxVolume { get; set; }
        public Nullable<decimal> MinVolume { get; set; }
        public Nullable<decimal> MaxProbOld { get; set; }
        public Nullable<decimal> MinProbOld { get; set; }
        public Nullable<decimal> MaxVolumeOld { get; set; }
        public Nullable<decimal> MinVolumeOld { get; set; }
        public int RelevanceId { get; set; }
    }
}
