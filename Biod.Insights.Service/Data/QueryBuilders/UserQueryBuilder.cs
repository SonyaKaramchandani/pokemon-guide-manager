using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Service.Data.CustomModels;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace Biod.Insights.Service.Data.QueryBuilders
{
    public class UserQueryBuilder : IQueryBuilder<AspNetUsers, UserJoinResult>
    {
        [NotNull] private readonly BiodZebraContext _dbContext;

        private IQueryable<AspNetUsers> _customInitialQueryable;
        private readonly bool _trackChanges;
        private string _userId;
        
        public UserQueryBuilder([NotNull] BiodZebraContext dbContext, bool trackChanges = false)
        {
            _customInitialQueryable = null;
            _dbContext = dbContext;
            _trackChanges = trackChanges;
        }

        public UserQueryBuilder SetUserId(string userId)
        {
            _userId = userId;
            return this;
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

            if (!_trackChanges)
            {
                query = query.AsNoTracking();
            }

            if (_userId != null)
            {
                query = query.Where(u => u.Id == _userId);
            }

            return await query.Select(u => new UserJoinResult
            {
                User = u,
                Roles = u.AspNetUserRoles.Select(ur => ur.Role)
            }).ToListAsync();
        }
    }
}