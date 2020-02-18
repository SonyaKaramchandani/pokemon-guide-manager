using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Biod.Insights.Api.Models;
using Biod.Insights.Api.Models.Account;
using Biod.Insights.Api.Models.User;

namespace Biod.Insights.Api.Interface
{
    public interface IUserService
    {
        Task<UserModel> GetUser([NotNull] string userId);
        
        Task<UserModel> UpdatePersonalDetails([NotNull] string userId, [NotNull] UserPersonalDetailsModel personalDetailsModel);
        
        Task<UserModel> UpdateCustomSettings([NotNull] string userId, [NotNull] UserCustomSettingsModel customSettingsModel);
        
        Task<UserModel> UpdateNotificationSettings([NotNull] string userId, [NotNull] UserNotificationsModel notificationsModel);
        
        Task UpdatePassword([NotNull] string userId, [NotNull] ChangePasswordModel changePasswordModel);
    }
}