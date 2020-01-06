using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class XtblDiseaseAgents
    {
        public int DiseaseId { get; set; }
        public int AgentId { get; set; }

        public virtual Agents Agent { get; set; }
        public virtual Diseases Disease { get; set; }
    }
}
