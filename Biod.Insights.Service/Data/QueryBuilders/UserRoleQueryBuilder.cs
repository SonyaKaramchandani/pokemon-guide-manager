using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace Biod.Insights.Service.Data.QueryBuilders
{
    public class UserRoleQueryBuilder : IQueryBuilder<AspNetRoles, AspNetRoles>
    {
        [NotNull] private readonly BiodZebraContext _dbContext;

        private string _roleId;
        
        private bool _includePublicRolesOnly;
        private bool _includePrivateRolesOnly;
        
        public UserRoleQueryBuilder([NotNull] BiodZebraContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserRoleQueryBuilder SetRoleId(string roleId)
        {
            _roleId = roleId; 
            return this;
        }

        public UserRoleQueryBuilder IncludePublicRolesOnly()
        {
            _includePublicRolesOnly = true;
            _includePrivateRolesOnly = false;
            return this;
        }

        public UserRoleQueryBuilder IncludePrivateRolesOnly()
        {
            _includePublicRolesOnly = false;
            _includePrivateRolesOnly = true;
            return this;
        }
        
        public IQueryable<AspNetRoles> GetInitialQueryable()
        {
            return _dbContext.AspNetRoles.AsQueryable();
        }

        public async Task<IEnumerable<AspNetRoles>> BuildAndExecute()
        {
            var query = GetInitialQueryable();

            if (!string.IsNullOrWhiteSpace(_roleId))
            {
                query = query.Where(r => r.Id == _roleId);
            }

            if (_includePublicRolesOnly)
            {
                query = query.Where(r => r.IsPublic);
            }

            if (_includePrivateRolesOnly)
            {
                query = query.Where(r => !r.IsPublic);
            }

            return await query.ToListAsync();
        }
    }
}