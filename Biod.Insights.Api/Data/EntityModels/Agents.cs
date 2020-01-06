using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class Agents
    {
        public Agents()
        {
            XtblDiseaseAgents = new HashSet<XtblDiseaseAgents>();
        }

        public int AgentId { get; set; }
        public string Agent { get; set; }
        public int AgentTypeId { get; set; }

        public virtual AgentTypes AgentType { get; set; }
        public virtual ICollection<XtblDiseaseAgents> XtblDiseaseAgents { get; set; }
    }
}
