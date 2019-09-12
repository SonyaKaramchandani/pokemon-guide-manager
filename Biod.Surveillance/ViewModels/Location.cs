using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biod.Surveillance.ViewModels
{
    public class Location
    {
        public int GeoID { get; set; }
        public string GeoName { get; set; }
        public int? NewDeathCount { get; set; }
        public int? NewConfirmedCount { get; set; }
        public int? NewSuspectedCount { get; set; }
        public int? NewReportedCount { get; set; }
        public int? TotalDeathCount { get; set; }
        public int? TotalConfirmedCount { get; set; }
        public int? TotalSuspectedCount { get; set; }
        public int? TotalReportedCount { get; set; }


    }
}