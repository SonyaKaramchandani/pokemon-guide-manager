using Biod.Surveillance.Models.Surveillance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biod.Surveillance.ViewModels
{
    public class EventUpdateModel
    {
        public int eventID { get; set; }
        public string eventTitle { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string diseaseID { get; set; }
        public string SpeciesID { get; set; }
        public string[] reasonIDs { get; set; }
        public string alertRadius { get; set; }
        public string priorityID { get; set; }
        public string isPublished { get; set; }
        public string isPublishedChangesToApi { get; set; }
        public string summary { get; set; }
        public string notes { get; set; }
        public string locationObject { get; set; }
        public string eventMongoId { get; set; }
        public string associatedArticles { get; set; }
        public string LastUpdatedByUserName { get; set; }

    }
}