using System.Collections.Generic;
using Biod.Insights.Service.Models.Airport;
using Newtonsoft.Json;

namespace Biod.Insights.Service.Models.Event
{
    public class EventAirportModel
    {
        /// <summary>
        /// List of airports that are expected to export the event
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<GetAirportModel> SourceAirports { get; set; }
        
        /// <summary>
        /// Total number of Source Airports
        /// </summary>
        public int TotalSourceAirports { get; set; }

        /// <summary>
        /// Total volume across all source airports
        /// </summary>
        public int TotalSourceVolume { get; set; }
        
        /// <summary>
        /// List of airports that are expected to import the event
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<GetAirportModel> DestinationAirports { get; set; }

        /// <summary>
        /// Total number of Destination Airports
        /// </summary>
        public int TotalDestinationAirports { get; set; }

        /// <summary>
        /// Total volume across all destination airports
        /// </summary>
        public int TotalDestinationVolume { get; set; }
    }
}