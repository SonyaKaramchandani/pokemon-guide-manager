using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class HamType
    {
        public HamType()
        {
            ProcessedArticle = new HashSet<ProcessedArticle>();
        }

        public int HamTypeId { get; set; }
        public string HamTypeName { get; set; }

        public ICollection<ProcessedArticle> ProcessedArticle { get; set; }
    }
}
