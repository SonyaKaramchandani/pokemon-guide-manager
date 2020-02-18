using System.Threading.Tasks;
using Biod.Insights.Api.Models;
using Biod.Insights.Api.Models.User;

namespace Biod.Insights.Api.Interface
{
    public interface IDiseaseRelevanceService
    {
        Task<DiseaseRelevanceSettingsModel> GetUserDiseaseRelevanceSettings(UserModel user);
    }
}