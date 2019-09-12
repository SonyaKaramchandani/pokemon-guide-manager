using System;
using System.Collections.Generic;

namespace Biod.Zebra.Api.Core.Models
{
    public partial class XtblRelatedArticles
    {
        public string MainArticleId { get; set; }
        public string RelatedArticleId { get; set; }

        public ProcessedArticle MainArticle { get; set; }
    }
}
