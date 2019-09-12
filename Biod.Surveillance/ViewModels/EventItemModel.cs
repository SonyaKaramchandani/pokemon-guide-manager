using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biod.Surveillance.ViewModels
{
    public class EventItemModel
    {
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public bool? IsPublished { get; set; }
        public int? PriorityId { get; set; }
        public int ArticleCount { get; set; }
        public bool IsIncubationExceeded { get; set; }
        public bool Has30DaysElapsed { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public bool IsSuggested { get; set; }
    }
}