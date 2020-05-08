using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Service.Models.User;

namespace Biod.Insights.Service.Interface
{
    public interface IUserTypeService
    {
        Task<UserTypeModel> GetUserType(Guid userTypeId);
        
        Task<IEnumerable<UserTypeModel>> GetUserTypes();
    }
}