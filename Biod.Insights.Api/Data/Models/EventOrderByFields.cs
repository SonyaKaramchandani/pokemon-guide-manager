using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class EventOrderByFields
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string ColumnName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsDefault { get; set; }
        public bool IsHidden { get; set; }
    }
}
