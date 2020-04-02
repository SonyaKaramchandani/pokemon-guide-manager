using System.Collections.Generic;
using Biod.Insights.Service.Models.Airport;
using Biod.Insights.Service.Models.Disease;
using Biod.Insights.Service.Models.Event;

namespace Biod.Insights.Service.Models.Risk
{
    public class CalculationBreakdownModel
    {
        public const int TopAirportCount = 10;

        public EventCalculationCasesModel CalculationCases { get; set; }

        public DiseaseInformationModel DiseaseInformation { get; set; }

        public IEnumerable<GetAirportModel> TopSourceAirports { get; set; }

        public int TotalSourceAirports { get; set; }

        public int TotalSourceVolume { get; set; }

        public IEnumerable<GetAirportModel> TopDestinationAirports { get; set; }

        public int TotalDestinationAirports { get; set; }

        public int TotalDestinationVolume { get; set; }
    }
}