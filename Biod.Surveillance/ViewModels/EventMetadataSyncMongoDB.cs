using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biod.Surveillance.ViewModels
{

    public class EventCaseCountsByDateMongo
    {
        public string date { get; set; }
        public int suspected { get; set; }
        public int confirmed { get; set; }
        public int reported { get; set; }
        public int deaths { get; set; }
    }
    public class EventCaseCountMongo
    {
        public int geonameId { get; set; }
        public List<EventCaseCountsByDateMongo> caseCountsByDate { get; set; }
    }
    public class EventMetadataSyncMongoDB
    {
        public int eventId { get; set; }
        public string eventName { get; set; }
        public string priority { get; set; }
        public string summary { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int? diseaseId { get; set; }
        public int? speciesId { get; set; }
        public bool localOnly { get; set; }
        public bool? approvedForPublishing { get; set; }
        public string publishedDate { get; set; }
        public List<string> reasonForCreation { get; set; }
        public string notes { get; set; }
        public List<EventCaseCountMongo> locations { get; set; }
        public List<string> associatedArticleIds { get; set; }
        //public string userLastModifiedDate { get; set; }

    }
}