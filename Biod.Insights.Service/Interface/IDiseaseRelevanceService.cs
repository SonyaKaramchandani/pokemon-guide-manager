using System.Threading.Tasks;
using Biod.Insights.Service.Models;
using Biod.Insights.Service.Models.User;

namespace Biod.Insights.Service.Interface
{
    public interface IDiseaseRelevanceService
    {
        Task<DiseaseRelevanceSettingsModel> GetUserDiseaseRelevanceSettings(UserModel user);
    }
}