using System;
using System.Collections.Generic;
using Biod.Insights.Service.Models.Airport;
using Biod.Insights.Service.Models.Article;
using Biod.Insights.Service.Models.Disease;
using Newtonsoft.Json;

namespace Biod.Insights.Service.Models.Event
{
    public class GetEventModel
    {
        /// <summary>
        /// General information of the event
        /// </summary>
        public EventInformationModel EventInformation { get; set; }

        /// <summary>
        /// Given a location, the volume and probability risk of importation for that location 
        /// </summary>
        public RiskModel ImportationRisk { get; set; }

        /// <summary>
        /// The volume and probability risk of exportation for the event 
        /// </summary>
        public RiskModel ExportationRisk { get; set; }

        /// <summary>
        /// Articles associated to the event
        /// </summary>
        public IEnumerable<ArticleModel> Articles { get; set; }

        /// <summary>
        /// List of airports that are expected to export the event
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<GetAirportModel> SourceAirports { get; set; }

        /// <summary>
        /// List of airports that are expected to import the event
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<GetAirportModel> DestinationAirports { get; set; }

        /// <summary>
        /// Disease information for the disease associated to the event
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DiseaseInformationModel DiseaseInformation { get; set; }

        /// <summary>
        /// Given a location, the outbreak potential category of this disease to that location
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public OutbreakPotentialCategoryModel OutbreakPotentialCategory { get; set; }

        /// <summary>
        /// Metadata on any properties used to calculate the risk (e.g. cases used)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public EventCalculationCasesModel CalculationMetadata { get; set; }

        #region Event Locations

        // Fields in this section depend on the loading of Event Locations

        /// <summary>
        /// Total computed case counts over all locations of this event
        /// </summary>
        public CaseCountModel CaseCounts { get; set; }

        /// <summary>
        /// List of locations that are associated to the event
        /// </summary>
        public IEnumerable<EventLocationModel> EventLocations { get; set; }

        #region Local Case Counts

        // Fields in this section further depend on the loading of Local Case Counts
        
        /// <summary>
        /// Given a location, the number of cases that are considered proximal to that location
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public CaseCountModel LocalCaseCounts { get; set; }

        /// <summary>
        /// Flag whether the event has local cases
        /// </summary>
        public bool IsLocal { get; set; }
        
        #endregion

        #region Event Locations History

        // Fields in this section further depend on the loading of Event Locations History

        /// <summary>
        /// List of locations that have had their case count increased since last updated
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<EventLocationModel> UpdatedLocations { get; set; }

        /// <summary>
        /// Date of the previous update activity for any of the locations that have changed
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? PreviousActivityDate { get; set; }

        #endregion

        #endregion
    }
}