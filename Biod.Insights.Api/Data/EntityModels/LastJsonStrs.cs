using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class LastJsonStrs
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string JsonStr { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
