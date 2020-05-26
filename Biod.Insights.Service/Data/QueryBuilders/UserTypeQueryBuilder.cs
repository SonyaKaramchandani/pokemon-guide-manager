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
    public class UserTypeQueryBuilder : IQueryBuilder<UserTypes, UserTypes>
    {
        [NotNull] private readonly BiodZebraContext _dbContext;
        [NotNull] private readonly UserTypeConfig _config;

        private IQueryable<UserTypes> _customInitialQueryable;

        public UserTypeQueryBuilder([NotNull] BiodZebraContext dbContext) : this(dbContext, new UserTypeConfig.Builder().Build())
        {
        }

        public UserTypeQueryBuilder([NotNull] BiodZebraContext dbContext, UserTypeConfig config)
        {
            _customInitialQueryable = null;
            _dbContext = dbContext;
            _config = config;
        }

        public IQueryable<UserTypes> GetInitialQueryable()
        {
            return _customInitialQueryable ?? _dbContext.UserTypes.AsQueryable();
        }

        public IQueryBuilder<UserTypes, UserTypes> OverrideInitialQueryable(IQueryable<UserTypes> customQueryable)
        {
            _customInitialQueryable = customQueryable;
            return this;
        }

        public async Task<IEnumerable<UserTypes>> BuildAndExecute()
        {
            var query = GetInitialQueryable();

            if (_config.UserTypeId.HasValue)
            {
                query = query.Where(r => r.Id == _config.UserTypeId.Value);
            }

            return await query.ToListAsync();
        }
    }
}