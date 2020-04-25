using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Service.Data.CustomModels;
using Biod.Insights.Service.Models.Risk;

namespace Biod.Insights.Service.Interface
{
    public interface IRiskCalculationService
    {
        Task<CalculationBreakdownModel> GetCalculationBreakdown(int eventId, int? geonameId);
        
        Task PreCalculateImportationRisk(int geonameId);
        
        Task PreCalculateImportationRisk(ICollection<int> geonameIds);
    }
}