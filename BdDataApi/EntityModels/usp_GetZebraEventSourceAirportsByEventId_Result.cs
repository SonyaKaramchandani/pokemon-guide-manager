//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BdDataApi.EntityModels
{
    using System;
    
    public partial class usp_GetZebraEventSourceAirportsByEventId_Result
    {
        public string CityDisplayName { get; set; }
        public string StationName { get; set; }
        public string StationCode { get; set; }
        public Nullable<int> Volume { get; set; }
        public Nullable<int> CtryRank { get; set; }
        public string CountryName { get; set; }
        public Nullable<int> NumCtryAirports { get; set; }
        public Nullable<int> WorldRank { get; set; }
        public Nullable<int> TotalAptsWorld { get; set; }
        public Nullable<long> TotalVolume { get; set; }
    }
}