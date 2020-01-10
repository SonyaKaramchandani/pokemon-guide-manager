using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Biod.Insights.Api.Models.User;

namespace Biod.Insights.Api.Interface
{
    public interface IUserService
    {
        Task<GetUserModel> GetUser([NotNull] string userId);
    }
}