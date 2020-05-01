using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Biod.Products.Common;
using Biod.Insights.Service.Data.CustomModels;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.Disease;
using Biod.Products.Common.Constants;
using Biod.Insights.Service.Configs;
using Microsoft.EntityFrameworkCore;
using OutbreakPotentialCategory = Biod.Products.Common.Constants.OutbreakPotentialCategory;
using Species = Biod.Products.Common.Constants.Species;

namespace Biod.Insights.Service.Data.QueryBuilders
{
    public class DiseaseQueryBuilder : IQueryBuilder<Diseases, DiseaseJoinResult>
    {
        [NotNull] private readonly BiodZebraContext _dbContext;
        [NotNull] private readonly DiseaseConfig _diseaseConfig;

        private IQueryable<Diseases> _customInitialQueryable;

        public DiseaseQueryBuilder([NotNull] BiodZebraContext dbContext) : this(dbContext, new DiseaseConfig.Builder().Build())
        {
        }

        public DiseaseQueryBuilder([NotNull] BiodZebraContext dbContext, [NotNull] DiseaseConfig diseaseConfig)
        {
            _dbContext = dbContext;
            _diseaseConfig = diseaseConfig;
        }

        public IQueryable<Diseases> GetInitialQueryable()
        {
            return _customInitialQueryable ?? _dbContext.Diseases.AsQueryable().AsNoTracking();
        }

        public IQueryBuilder<Diseases, DiseaseJoinResult> OverrideInitialQueryable(IQueryable<Diseases> customQueryable)
        {
            _customInitialQueryable = customQueryable;
            return this;
        }

        public async Task<IEnumerable<DiseaseJoinResult>> BuildAndExecute()
        {
            var query = GetInitialQueryable();

            if (_diseaseConfig.DiseaseIds.Any())
            {
                query = query.Where(d => _diseaseConfig.DiseaseIds.Contains(d.DiseaseId));
            }

            return await query.Select(d => new DiseaseJoinResult
            {
                DiseaseId = d.DiseaseId,
                DiseaseName = d.DiseaseName,
                OutbreakPotentialAttributeId = d.OutbreakPotentialAttributeId ?? (int) OutbreakPotentialCategory.Unknown,
                Agents = _diseaseConfig.IncludeAgents ? d.XtblDiseaseAgents.Select(x => x.Agent.Agent) : null,
                AgentTypes = _diseaseConfig.IncludeAgents
                    ? d.XtblDiseaseAgents
                        .Select(x => x.Agent.AgentType.AgentType)
                        .Distinct()
                        .OrderBy(a => a)
                    : null,
                AcquisitionModes = _diseaseConfig.IncludeAcquisitionModes
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
                TransmissionModes = _diseaseConfig.IncludeTransmissionModes
                    ? d.XtblDiseaseTransmissionMode
                        .Where(x => x.SpeciesId == (int) Species.Human)
                        .Select(x => x.TransmissionMode.DisplayName)
                        .Distinct()
                        .OrderBy(a => a)
                    : null,
                PreventionMeasures = _diseaseConfig.IncludeInterventions
                    ? d.XtblDiseaseInterventions
                        .Where(x => x.SpeciesId == (int) Species.Human)
                        .Select(x => x.Intervention)
                        .Where(i => i.InterventionType == InterventionType.Prevention)
                        .Select(i => i.DisplayName)
                        .Distinct()
                        .OrderBy(a => a)
                    : null,
                IncubationPeriod = _diseaseConfig.IncludeIncubationPeriods
                    ? d.DiseaseSpeciesIncubation.FirstOrDefault(i => i.SpeciesId == (int) Species.Human)
                    : null,
                SymptomaticPeriod = _diseaseConfig.IncludeSymptomaticPeriods
                    ? d.DiseaseSpeciesSymptomatic.FirstOrDefault(s => s.SpeciesId == (int) Species.Human)
                    : null,
                BiosecurityRisk = _diseaseConfig.IncludeBiosecurityRisks
                    ? d.BiosecurityRiskNavigation.BiosecurityRiskDesc.DefaultIfWhiteSpace(null)
                    : null
            }).ToListAsync();
        }
    }
}