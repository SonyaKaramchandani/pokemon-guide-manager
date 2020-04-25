using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Models;
using Biod.Insights.Service.Models.Account;
using Biod.Insights.Service.Models.User;

namespace Biod.Insights.Service.Interface
{
    public interface IUserService
    {
        Task<UserModel> GetUser([NotNull] UserConfig config);

        Task<IEnumerable<UserModel>> GetUsers(IQueryable<AspNetUsers> customQueryable = null);

        Task<UserModel> UpdatePersonalDetails([NotNull] UserConfig config, [NotNull] UserPersonalDetailsModel personalDetailsModel);

        Task<UserModel> UpdateCustomSettings([NotNull] UserConfig config, [NotNull] UserCustomSettingsModel customSettingsModel);

        Task<UserModel> UpdateNotificationSettings([NotNull] UserConfig config, [NotNull] UserNotificationsModel notificationsModel);

        Task UpdatePassword([NotNull] UserConfig config, [NotNull] ChangePasswordModel changePasswordModel);
    }
}