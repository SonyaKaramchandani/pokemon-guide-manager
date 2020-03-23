using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Service.Data.CustomModels;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.Disease;
using Biod.Insights.Common.Constants;
using Microsoft.EntityFrameworkCore;
using OutbreakPotentialCategory = Biod.Insights.Common.Constants.OutbreakPotentialCategory;
using Species = Biod.Insights.Common.Constants.Species;

namespace Biod.Insights.Service.Data.QueryBuilders
{
    public class DiseaseQueryBuilder : IQueryBuilder<Diseases, DiseaseJoinResult>
    {
        [NotNull] private readonly BiodZebraContext _dbContext;

        private IQueryable<Diseases> _customInitialQueryable;
        private readonly HashSet<int> _diseaseIds = new HashSet<int>();

        private bool _includeAgents;
        private bool _includeAcquisitionModes;
        private bool _includeTransmissionModes;
        private bool _includeInterventions;
        private bool _includeIncubationPeriods;
        private bool _includeSymptomaticPeriods;
        private bool _includeBiosecurityRisks;

        public DiseaseQueryBuilder([NotNull] BiodZebraContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Diseases> GetInitialQueryable()
        {
            return _customInitialQueryable ?? _dbContext.Diseases.AsQueryable();
        }

        public IQueryBuilder<Diseases, DiseaseJoinResult> OverrideInitialQueryable(IQueryable<Diseases> customQueryable)
        {
            _customInitialQueryable = customQueryable;
            return this;
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
                .IncludeAcquisitionModes()
                .IncludeTransmissionModes()
                .IncludeInterventions()
                .IncludeIncubationPeriods()
                .IncludeSymptomaticPeriods()
                .IncludeBiosecurityRisks();
        }

        public DiseaseQueryBuilder IncludeAgents()
        {
            _includeAgents = true;
            return this;
        }

        public DiseaseQueryBuilder IncludeAcquisitionModes()
        {
            _includeAcquisitionModes = true;
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
        
        public DiseaseQueryBuilder IncludeSymptomaticPeriods()
        {
            _includeSymptomaticPeriods = true;
            return this;
        }

        public DiseaseQueryBuilder IncludeBiosecurityRisks()
        {
            _includeBiosecurityRisks = true;
            return this;
        }

        public async Task<IEnumerable<DiseaseJoinResult>> BuildAndExecute()
        {
            var query = GetInitialQueryable();

            if (_diseaseIds.Any())
            {
                query = query.Where(d => _diseaseIds.Contains(d.DiseaseId));
            }

            return await query.Select(d => new DiseaseJoinResult
            {
                DiseaseId = d.DiseaseId,
                DiseaseName = d.DiseaseName,
                OutbreakPotentialAttributeId = d.OutbreakPotentialAttributeId ?? (int) OutbreakPotentialCategory.Unknown,
                Agents = _includeAgents ? d.XtblDiseaseAgents.Select(x => x.Agent.Agent) : null,
                AgentTypes = _includeAgents
                    ? d.XtblDiseaseAgents
                        .Select(x => x.Agent.AgentType.AgentType)
                        .Distinct()
                        .OrderBy(a => a)
                    : null,
                AcquisitionModes = _includeAcquisitionModes
                    ? d.XtblDiseaseAcquisitionMode
                        .Where(x => x.SpeciesId == (int) Species.Human)
                        .Select(x => new AcquisitionModeModel
                        {
                            Id = x.AcquisitionMode.AcquisitionModeId,
                            RankId = x.AcquisitionModeRank,
                            Label = x.AcquisitionMode.AcquisitionModeLabel,
                            Description = x.AcquisitionMode.AcquisitionModeDefinitionLabel
                        })
                        .OrderBy(a => a.RankId)
                        .ThenBy(a => a.Label)
                    : null,
                TransmissionModes = _includeTransmissionModes
                    ? d.XtblDiseaseTransmissionMode
                        .Where(x => x.SpeciesId == (int) Species.Human)
                        .Select(x => x.TransmissionMode.DisplayName)
                        .Distinct()
                        .OrderBy(a => a)
                    : null,
                PreventionMeasures = _includeInterventions
                    ? d.XtblDiseaseInterventions
                        .Where(x => x.SpeciesId == (int) Species.Human)
                        .Select(x => x.Intervention)
                        .Where(i => i.InterventionType == InterventionType.Prevention)
                        .Select(i => i.DisplayName)
                        .Distinct()
                        .OrderBy(a => a)
                    : null,
                IncubationPeriod = _includeIncubationPeriods
                    ? d.DiseaseSpeciesIncubation.FirstOrDefault(i => i.SpeciesId == (int) Species.Human)
                    : null,
                SymptomaticPeriod = _includeSymptomaticPeriods
                    ? d.DiseaseSpeciesSymptomatic.FirstOrDefault(s => s.DiseaseId == d.DiseaseId && s.SpeciesId == (int) Species.Human)
                    : null,
                BiosecurityRisk = _includeBiosecurityRisks
                    ? d.BiosecurityRiskNavigation.BiosecurityRiskDesc
                    : null
            }).ToListAsync();
        }
    }
}