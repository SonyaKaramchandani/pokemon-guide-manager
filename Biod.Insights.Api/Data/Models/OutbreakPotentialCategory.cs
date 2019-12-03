using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class OutbreakPotentialCategory
    {
        public int Id { get; set; }
        public int AttributeId { get; set; }
        public string Rule { get; set; }
        public bool NeedsMap { get; set; }
        public string MapThreshold { get; set; }
        public string EffectiveMessageDescription { get; set; }
        public string EffectiveMessage { get; set; }
        public bool IsLocalTransmissionPossible { get; set; }
    }
}
