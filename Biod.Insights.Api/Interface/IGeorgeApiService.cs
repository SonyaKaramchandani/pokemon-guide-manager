using Biod.Insights.Api.Models.Risk;
using System.Threading.Tasks;

namespace Biod.Insights.Api.Interface
{
    public interface IGeorgeApiService
    {
        Task<GeorgeRiskClass> GetLocationRisk(float latitude, float longitude);
    }
}
