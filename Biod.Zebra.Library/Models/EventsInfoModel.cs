using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biod.Zebra.Library.Models
{
    public class EventsInfoModel
    {
        public EventsInfoModel() 
        {
            OutbreakPotentialCategory = new List<EventLocationsOutbreakPotentialModel>();
        }
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public string ExportationPriorityTitle { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public string Summary { get; set; }
        public string Notes { get; set; }
        public Nullable<bool> HasOutlookReport { get; set; }
        public bool IsLocalOnly { get; set; }
        public EventCountryModel EventCountry { get; set; }
        public string DiseaseName { get; set; }
        public string BiosecurityRisk { get; set; }
        public string Transmissions { get; set; }
        public string Prevensions { get; set; }
        public int RepCases { get; set; }
        public int Deaths { get; set; }
        public string Group { get; set; }
        public decimal ExportationProbabilityMin { get; set; }
        public decimal ExportationProbabilityMax { get; set; }
        public decimal ExportationInfectedTravellersMin { get; set; }
        public decimal ExportationInfectedTravellersMax { get; set; }
        public string ExportationProbabilityName { get; set; }
        public decimal ImportationProbabilityMin { get; set; }
        public decimal ImportationProbabilityMax { get; set; }
        public string ImportationProbabilityName { get; set; }
        public decimal ImportationInfectedTravellersMin { get; set; }
        public decimal ImportationInfectedTravellersMax { get; set; }
        public bool LocalSpread { get; set; }
        public List<SourceNameModel> SourceNameList { get; set; }
        public List<EventLocationsOutbreakPotentialModel> OutbreakPotentialCategory { get; set; }
        public EventsInfoModel DeepCopy()
        {
            EventsInfoModel clone = (EventsInfoModel)this.MemberwiseClone();
            clone.EventCountry = new EventCountryModel { CountryName = this.EventCountry.CountryName, CountryCentroidAsText = this.EventCountry.CountryCentroidAsText };
            //TODO: add code for OutbreakPotentialCategory
            return clone;
        }
    }

    public class EventCountryModel
    {
        public string CountryName { get; set; }
        public string CountryCentroidAsText { get; set; }
    }
}