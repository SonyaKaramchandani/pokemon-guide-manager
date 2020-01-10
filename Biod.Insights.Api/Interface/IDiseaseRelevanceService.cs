using System.Threading.Tasks;
using Biod.Insights.Api.Models;

namespace Biod.Insights.Api.Interface
{
    public interface IDiseaseRelevanceService
    {
        Task<DiseaseRelevanceSettingsModel> GetUserDiseaseRelevanceSettings(string userId);
    }
}