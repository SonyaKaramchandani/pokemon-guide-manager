using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class XtblArticleDisease
    {
        public string ArticleId { get; set; }
        public int DiseaseId { get; set; }

        public ProcessedArticle Article { get; set; }
        public Diseases Disease { get; set; }
    }
}
