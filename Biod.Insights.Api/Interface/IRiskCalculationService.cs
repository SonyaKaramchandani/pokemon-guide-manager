using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Api.Data.CustomModels;

namespace Biod.Insights.Api.Interface
{
    public interface IRiskCalculationService
    {
        Task PreCalculateImportationRisk(int geonameId);
        
        Task PreCalculateImportationRisk(ICollection<int> geonameIds);
    }
}