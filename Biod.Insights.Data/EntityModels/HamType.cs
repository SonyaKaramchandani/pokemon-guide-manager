using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class HamType
    {
        public HamType()
        {
            ProcessedArticle = new HashSet<ProcessedArticle>();
        }

        public int HamTypeId { get; set; }
        public string HamTypeName { get; set; }

        public virtual ICollection<ProcessedArticle> ProcessedArticle { get; set; }
    }
}
