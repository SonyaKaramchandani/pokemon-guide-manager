using Biod.Insights.Service.Models.Disease;
using Biod.Insights.Service.Models.Event;

namespace Biod.Insights.Service.Models.Risk
{
    public class CalculationBreakdownModel
    {
        public const int TopAirportCount = 10;

        public EventCalculationCasesModel CalculationCases { get; set; }

        public DiseaseInformationModel DiseaseInformation { get; set; }

        public EventAirportModel Airports { get; set; }
    }
}