using Biod.Surveillance.Models.Surveillance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biod.Surveillance.ViewModels
{
    public class SuggestedEventViewModel
    {
        public string EventId { get; set; }
        public string EventTitle { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public int DiseaseId { get; set; }
        public bool IsLocalOnly { get; set; }
        public bool IsPublished { get; set; }
        public int PriorityId { get; set; }
        public string Summary { get; set; }
        public string Notes { get; set; }
        public int ArticleCount { get; set; }
        public List<ProcessedArticle> AssociatedArticle { get; set; }

    }
}