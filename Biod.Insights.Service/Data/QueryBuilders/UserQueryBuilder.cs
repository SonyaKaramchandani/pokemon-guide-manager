using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Service.Data.CustomModels;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace Biod.Insights.Service.Data.QueryBuilders
{
    public class UserQueryBuilder : IQueryBuilder<AspNetUsers, UserJoinResult>
    {
        [NotNull] private readonly BiodZebraContext _dbContext;
        [NotNull] private readonly UserConfig _config;

        private IQueryable<AspNetUsers> _customInitialQueryable;

        public UserQueryBuilder([NotNull] BiodZebraContext dbContext) : this(dbContext,
            new UserConfig.Builder().Build())
        {
            
        }
        
        public UserQueryBuilder([NotNull] BiodZebraContext dbContext, UserConfig config)
        {
            _customInitialQueryable = null;
            _dbContext = dbContext;
            _config = config;
        }

        public IQueryable<AspNetUsers> GetInitialQueryable()
        {
            return _customInitialQueryable ?? _dbContext.AspNetUsers.AsQueryable();
        }

        public IQueryBuilder<AspNetUsers, UserJoinResult> OverrideInitialQueryable(IQueryable<AspNetUsers> customQueryable)
        {
            _customInitialQueryable = customQueryable;
            return this;
        }

        public async Task<IEnumerable<UserJoinResult>> BuildAndExecute()
        {
            var query = GetInitialQueryable();

            if (!_config.TrackChanges)
            {
                query = query.AsNoTracking();
            }

            if (_config.UserId != null)
            {
                query = query.Where(u => u.Id == _config.UserId);
            }

            return await query.Select(u => new UserJoinResult
            {
                User = u,
                Roles = u.AspNetUserRoles.Select(ur => ur.Role)
            }).ToListAsync();
        }
    }
}