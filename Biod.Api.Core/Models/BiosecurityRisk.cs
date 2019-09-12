using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class BiosecurityRisk
    {
        public BiosecurityRisk()
        {
            Diseases = new HashSet<Diseases>();
        }

        public string BiosecurityRiskCode { get; set; }
        public string BiosecurityRiskDesc { get; set; }

        public ICollection<Diseases> Diseases { get; set; }
    }
}
