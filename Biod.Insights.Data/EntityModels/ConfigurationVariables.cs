using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class ConfigurationVariables
    {
        public Guid ConfigurationVariableId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
        public string Description { get; set; }
        public string ApplicationName { get; set; }
    }
}
