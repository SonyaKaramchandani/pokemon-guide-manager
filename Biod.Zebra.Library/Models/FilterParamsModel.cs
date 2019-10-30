using System.Collections.Generic;

namespace Biod.Zebra.Library.Models
{
    public class FilterParamsModel
    {
        public List<GeonameModel> geonames { get; set; }
        public string geonameIds { get; }
        public string diseasesIds { get; }
        public string transmissionModesIds { get; }
        public string InterventionMethods { get; }
        public bool locationOnly { get; set; }
        public string severityRisks { get; }
        public string biosecurityRisks { get; }
        
        /// <summary>
        /// Whether the requested page has an Event ID in the query params
        /// </summary>
        public bool hasEventId { get; set; }
        
        /// <summary>
        /// Whether the filter is from custom settings (instead of global)
        /// </summary>
        public bool customEvents { get; set; }
        
        /// <summary>
        /// The total number of events
        /// </summary>
        public int totalEvents { get; set; }

        public FilterParamsModel(string geonameIds, string diseasesIds, string transmissionModesIds, string interventionMethods, bool locationOnly, string severityRisks, string biosecurityRisks)
        {
            this.geonameIds = geonameIds;
            this.diseasesIds = diseasesIds;
            this.transmissionModesIds = transmissionModesIds;
            InterventionMethods = interventionMethods;
            this.locationOnly = locationOnly;
            this.severityRisks = severityRisks;
            this.biosecurityRisks = biosecurityRisks;
            hasEventId = false;
            customEvents = true;
            totalEvents = 0;
        }
    }
}
