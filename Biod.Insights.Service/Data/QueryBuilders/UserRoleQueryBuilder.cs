using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace Biod.Insights.Service.Data.QueryBuilders
{
    public class UserRoleQueryBuilder : IQueryBuilder<AspNetRoles, AspNetRoles>
    {
        [NotNull] private readonly BiodZebraContext _dbContext;
        [NotNull] private readonly UserRoleConfig _config;
        
        private IQueryable<AspNetRoles> _customInitialQueryable;
        
        public UserRoleQueryBuilder([NotNull] BiodZebraContext dbContext) : this(dbContext, new UserRoleConfig.Builder().Build())
        {
            
        }

        public UserRoleQueryBuilder([NotNull] BiodZebraContext dbContext, UserRoleConfig config)
        {
            _customInitialQueryable = null;
            _dbContext = dbContext;
            _config = config;
        }

        public IQueryable<AspNetRoles> GetInitialQueryable()
        {
            return _customInitialQueryable ?? _dbContext.AspNetRoles.AsQueryable();
        }

        public IQueryBuilder<AspNetRoles, AspNetRoles> OverrideInitialQueryable(IQueryable<AspNetRoles> customQueryable)
        {
            _customInitialQueryable = customQueryable;
            return this;
        }

        public async Task<IEnumerable<AspNetRoles>> BuildAndExecute()
        {
            var query = GetInitialQueryable();

            if (!string.IsNullOrWhiteSpace(_config.RoleId))
            {
                query = query.Where(r => r.Id == _config.RoleId);
            }

            if (_config.IncludePublicRolesOnly)
            {
                query = query.Where(r => r.IsPublic);
            }

            if (_config.IncludePrivateRolesOnly)
            {
                query = query.Where(r => !r.IsPublic);
            }

            return await query.ToListAsync();
        }
    }
}