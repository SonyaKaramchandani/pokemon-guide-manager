using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biod.Surveillance.ViewModels
{
    public class SuggestedEventModel
    {
        public string eventID { get; set; }
        public string eventTitle { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int diseaseID { get; set; }
        public String[] reasonIDs { get; set; }
        public bool alertRadius { get; set; }
        public int priorityID { get; set; }
        public bool isPublished { get; set; }
        public string summary { get; set; }
        public string notes { get; set; }
        public string locationObject { get; set; }
        public string eventMongoId { get; set; }
        public string associatedArticles { get; set; }
        public string LastUpdatedByUserName { get; set; }
    }
}