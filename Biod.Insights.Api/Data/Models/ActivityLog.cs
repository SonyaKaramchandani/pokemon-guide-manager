using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class ActivityLog
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Thread { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string UserName { get; set; }
        public string HostAddress { get; set; }
        public string Browser { get; set; }
        public string ServerName { get; set; }
        public string Url { get; set; }
        public string ApplicationName { get; set; }
    }
}
