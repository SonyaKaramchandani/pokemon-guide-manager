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
    
    public partial class usp_ZebraAnalyticsGetUserEmailNotification_Result
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string AoiGeonameIds { get; set; }
        public string UserEmail { get; set; }
        public int EmailType { get; set; }
        public Nullable<int> EventId { get; set; }
        public System.DateTimeOffset SentDate { get; set; }
    }
}