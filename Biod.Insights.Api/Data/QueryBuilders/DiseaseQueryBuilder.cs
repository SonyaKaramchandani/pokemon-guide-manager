using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Api.Data.CustomModels;
using Biod.Insights.Api.Data.EntityModels;
using Biod.Insights.Api.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Biod.Insights.Api.Data.QueryBuilders
{
    public class DiseaseQueryBuilder : IQueryBuilder<Diseases, DiseaseJoinResult>
    {
        [NotNull] private readonly BiodZebraContext _dbContext;

        private readonly HashSet<int> _diseaseIds = new HashSet<int>();

        private bool _includeAgents;
        private bool _includeTransmissionModes;
        private bool _includeInterventions;
        private bool _includeIncubationPeriods;
        private bool _includeBiosecurityRisks;
        private bool _includeOutbreakPotentialCategories;

        public DiseaseQueryBuilder([NotNull] BiodZebraContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Diseases> GetInitialQueryable()
        {
            return _dbContext.Diseases.AsQueryable();
        }

        public DiseaseQueryBuilder AddDiseaseId(int diseaseId)
        {
            _diseaseIds.Add(diseaseId);
            return this;
        }

        public DiseaseQueryBuilder AddDiseaseIds(IEnumerable<int> diseaseIds)
        {
            _diseaseIds.UnionWith(diseaseIds);
            return this;
        }

        public DiseaseQueryBuilder IncludeAll()
        {
            return this
                .IncludeAgents()
                .IncludeTransmissionModes()
                .IncludeInterventions()
                .IncludeIncubationPeriods()
                .IncludeBiosecurityRisks()
                .IncludeOutbreakPotentialCategories();
        }

        public DiseaseQueryBuilder IncludeAgents()
        {
            _includeAgents = true;
            return this;
        }

        public DiseaseQueryBuilder IncludeTransmissionModes()
        {
            _includeTransmissionModes = true;
            return this;
        }

        public DiseaseQueryBuilder IncludeInterventions()
        {
            _includeInterventions = true;
            return this;
        }

        public DiseaseQueryBuilder IncludeIncubationPeriods()
        {
            _includeIncubationPeriods = true;
            return this;
        }

        public DiseaseQueryBuilder IncludeBiosecurityRisks()
        {
            _includeBiosecurityRisks = true;
            return this;
        }

        public DiseaseQueryBuilder IncludeOutbreakPotentialCategories()
        {
            _includeOutbreakPotentialCategories = true;
            return this;
        }

        public async Task<IEnumerable<DiseaseJoinResult>> BuildAndExecute()
        {
            var query = GetInitialQueryable();

            if (_diseaseIds.Any())
            {
                query = query.Where(d => _diseaseIds.Contains(d.DiseaseId));
            }

            if (_includeAgents)
            {
                query = query
                    .Include(d => d.XtblDiseaseAgents)
                    .ThenInclude(x => x.Agent)
                    .ThenInclude(a => a.AgentType);
            }

            if (_includeTransmissionModes)
            {
                query = query
                    .Include(d => d.XtblDiseaseTransmissionMode)
                    .ThenInclude(x => x.TransmissionMode);
            }

            if (_includeInterventions)
            {
                query = query
                    .Include(d => d.XtblDiseaseInterventions)
                    .ThenInclude(x => x.Intervention);
            }

            if (_includeIncubationPeriods)
            {
                query = query.Include(d => d.DiseaseSpeciesIncubation);
            }

            // Queries involving joining
            var joinQuery = query.Select(d => new DiseaseJoinResult {Disease = d});

            if (_includeBiosecurityRisks)
            {
                joinQuery =
                    from d in joinQuery
                    join b in _dbContext.BiosecurityRisk on d.Disease.BiosecurityRisk equals b.BiosecurityRiskCode into br
                    from b in br.DefaultIfEmpty()
                    select new DiseaseJoinResult {Disease = d.Disease, BiosecurityRisk = b, OutbreakPotentialCategory = d.OutbreakPotentialCategory};
            }

            if (_includeOutbreakPotentialCategories)
            {
                // Outbreak Potential Categories will execute the query to handle the single to multiple relationship
                var intermediateQuery =
                    from d in joinQuery
                    join o in _dbContext.OutbreakPotentialCategory on d.Disease.OutbreakPotentialAttributeId equals o.AttributeId into opc
                    from o in opc.DefaultIfEmpty()
                    select new {d.Disease, d.BiosecurityRisk, OutbreakPotentialCategory = o};

                return (await intermediateQuery.ToListAsync())
                    .GroupBy(d => d.Disease.DiseaseId)
                    .Select(g => new DiseaseJoinResult
                    {
                        Disease = g.First().Disease,
                        BiosecurityRisk = g.First().BiosecurityRisk,
                        OutbreakPotentialCategory = g.Select(d => d.OutbreakPotentialCategory)
                    })
                    .AsEnumerable();
            }

            return await joinQuery.ToListAsync();
        }
    }
}