using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Biod.Insights.Service.Models;
using Biod.Insights.Service.Models.Account;
using Biod.Insights.Service.Models.User;

namespace Biod.Insights.Service.Interface
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