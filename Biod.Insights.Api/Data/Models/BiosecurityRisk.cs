using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class BiosecurityRisk
    {
        public BiosecurityRisk()
        {
            Diseases = new HashSet<Diseases>();
        }

        public string BiosecurityRiskCode { get; set; }
        public string BiosecurityRiskDesc { get; set; }

        public virtual ICollection<Diseases> Diseases { get; set; }
    }
}
