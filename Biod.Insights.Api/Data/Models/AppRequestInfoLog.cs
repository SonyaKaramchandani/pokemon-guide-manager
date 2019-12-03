using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class AppRequestInfoLog
    {
        public int LogTransId { get; set; }
        public string RequestIpaddress { get; set; }
        public bool IsPrivateIpAddress { get; set; }
        public DateTime LogDateTime { get; set; }
    }
}
