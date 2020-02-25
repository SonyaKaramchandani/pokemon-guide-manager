using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class AgentTypes
    {
        public AgentTypes()
        {
            Agents = new HashSet<Agents>();
        }

        public int AgentTypeId { get; set; }
        public string AgentType { get; set; }

        public virtual ICollection<Agents> Agents { get; set; }
    }
}
