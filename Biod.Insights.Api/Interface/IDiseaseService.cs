using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Api.Models;

namespace Biod.Insights.Api.Interface
{
    public interface IDiseaseService
    {
        Task<DiseaseInformationModel> GetDisease(int diseaseId);
        
        Task<IEnumerable<DiseaseInformationModel>> GetDiseases();
    }
}