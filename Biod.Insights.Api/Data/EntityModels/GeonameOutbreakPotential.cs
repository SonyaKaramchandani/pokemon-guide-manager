using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class GeonameOutbreakPotential
    {
        public int GeonameId { get; set; }
        public int DiseaseId { get; set; }
        public int OutbreakPotentialId { get; set; }
        public int OutbreakPotentialAttributeId { get; set; }
        public string EffectiveMessage { get; set; }

        public virtual Diseases Disease { get; set; }
        public virtual Geonames Geoname { get; set; }
    }
}
