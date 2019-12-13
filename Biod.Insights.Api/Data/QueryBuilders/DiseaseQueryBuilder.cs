using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Biod.Insights.Api.Data.CustomModels;
using Biod.Insights.Api.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Biod.Insights.Api.Data.QueryBuilders
{
    public class DiseaseQueryBuilder
    {
        [NotNull] private readonly BiodZebraContext _dbContext;

        private int? _diseaseId;

        private bool _includeAgents;
        private bool _includeTransmissionModes;
        private bool _includeInterventions;
        private bool _includeIncubationPeriods;
        private bool _includeBiosecurityRisks;
        private bool _includeOutbreakPotentialCategories;

        public DiseaseQueryBuilder(BiodZebraContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DiseaseQueryBuilder SetDiseaseId(int diseaseId)
        {
            _diseaseId = diseaseId;
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

        public IQueryable<DiseaseJoinResult> Build()
        {
            var query = _dbContext.Diseases.AsQueryable();

            if (_diseaseId != null)
            {
                query = query.Where(d => d.DiseaseId == _diseaseId);
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
                joinQuery =
                    from d in joinQuery
                    join o in _dbContext.OutbreakPotentialCategory on d.Disease.OutbreakPotentialAttributeId equals o.AttributeId into opc
                    from o in opc.DefaultIfEmpty()
                    select new DiseaseJoinResult {Disease = d.Disease, BiosecurityRisk = d.BiosecurityRisk, OutbreakPotentialCategory = o};
            }

            return joinQuery;
        }
    }
}