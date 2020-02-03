using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class ZebraPrevalence
    {
        public int EventDur { get; set; }
        public double Lambda { get; set; }
        public int DurInf { get; set; }
        public int MinTrav { get; set; }
    }
}
