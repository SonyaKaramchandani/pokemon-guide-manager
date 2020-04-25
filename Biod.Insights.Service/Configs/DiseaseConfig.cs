using System;
using System.Collections.Generic;
using System.Linq;

namespace Biod.Insights.Service.Configs
{
    public class DiseaseConfig
    {
        public readonly HashSet<int> DiseaseIds;
        public readonly bool IncludeAcquisitionModes;
        public readonly bool IncludeAgents;
        public readonly bool IncludeBiosecurityRisks;
        public readonly bool IncludeIncubationPeriods;
        public readonly bool IncludeInterventions;
        public readonly bool IncludeSymptomaticPeriods;
        public readonly bool IncludeTransmissionModes;

        private DiseaseConfig(Builder builder)
        {
            DiseaseIds = builder.DiseaseIds;
            IncludeAcquisitionModes = builder.IncludeAcquisitionModes;
            IncludeAgents = builder.IncludeAgents;
            IncludeBiosecurityRisks = builder.IncludeBiosecurityRisks;
            IncludeIncubationPeriods = builder.IncludeIncubationPeriods;
            IncludeInterventions = builder.IncludeInterventions;
            IncludeSymptomaticPeriods = builder.IncludeSymptomaticPeriods;
            IncludeTransmissionModes = builder.IncludeTransmissionModes;
        }

        public int GetDiseaseId()
        {
            if (DiseaseIds.Count != 1)
            {
                throw new InvalidOperationException("There are more than 1 disease ids in this configuration. This is for retrieving a single disease id");
            }

            return DiseaseIds.Single();
        }

        public class Builder
        {
            protected internal readonly HashSet<int> DiseaseIds = new HashSet<int>();
            protected internal bool IncludeAcquisitionModes;
            protected internal bool IncludeAgents;
            protected internal bool IncludeBiosecurityRisks;
            protected internal bool IncludeIncubationPeriods;
            protected internal bool IncludeInterventions;
            protected internal bool IncludeSymptomaticPeriods;
            protected internal bool IncludeTransmissionModes;

            public Builder()
            {
            }

            public Builder ShouldIncludeAllProperties()
            {
                return this
                    .ShouldIncludeAcquisitionModes()
                    .ShouldIncludeAgents()
                    .ShouldIncludeBiosecurityRisks()
                    .ShouldIncludeIncubationPeriods()
                    .ShouldIncludeInterventions()
                    .ShouldIncludeSymptomaticPeriods()
                    .ShouldIncludeTransmissionModes();
            }

            public Builder AddDiseaseId(int diseaseId)
            {
                DiseaseIds.Add(diseaseId);
                return this;
            }

            public Builder AddDiseaseIds(IEnumerable<int> diseaseIds)
            {
                DiseaseIds.UnionWith(diseaseIds);
                return this;
            }

            public Builder ShouldIncludeAcquisitionModes()
            {
                IncludeAcquisitionModes = true;
                return this;
            }

            public Builder ShouldIncludeAgents()
            {
                IncludeAgents = true;
                return this;
            }

            public Builder ShouldIncludeBiosecurityRisks()
            {
                IncludeBiosecurityRisks = true;
                return this;
            }

            public Builder ShouldIncludeIncubationPeriods()
            {
                IncludeIncubationPeriods = true;
                return this;
            }

            public Builder ShouldIncludeInterventions()
            {
                IncludeInterventions = true;
                return this;
            }

            public Builder ShouldIncludeSymptomaticPeriods()
            {
                IncludeSymptomaticPeriods = true;
                return this;
            }

            public Builder ShouldIncludeTransmissionModes()
            {
                IncludeTransmissionModes = true;
                return this;
            }

            public DiseaseConfig Build()
            {
                return new DiseaseConfig(this);
            }
        }
    }
}