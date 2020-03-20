using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Service.Data.CustomModels;

namespace Biod.Insights.Service.Interface
{
    public interface IRiskCalculationService
    {
        Task PreCalculateImportationRisk(int geonameId);
        
        Task PreCalculateImportationRisk(ICollection<int> geonameIds);
    }
}