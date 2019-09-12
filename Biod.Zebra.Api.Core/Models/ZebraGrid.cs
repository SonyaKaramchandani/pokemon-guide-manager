using System;
using System.Collections.Generic;

namespace Biod.Zebra.Api.Core.Models
{
    public partial class ZebraGrid
    {
        public string GridId { get; set; }
        public string ShapeText { get; set; }
        public int StationId { get; set; }
        public decimal? Probability { get; set; }
        public DateTime GsValidFromDate { get; set; }
    }
}
