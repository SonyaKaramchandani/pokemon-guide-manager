using System;
using System.Collections.Generic;
using Biod.Products.Common.Constants;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Models.Disease;
using Microsoft.EntityFrameworkCore;
using Species = Biod.Products.Common.Constants.Species;

namespace Biod.Insights.Service.UnitTest.Data
{
    public class DiseaseQueryBuilderDatabaseFixture : IDisposable
    {
        public static readonly AgentTypes AGENT_TYPE_A = new AgentTypes {AgentTypeId = 1, AgentType = "Agent Type 123124"};
        public static readonly AgentTypes AGENT_TYPE_B = new AgentTypes {AgentTypeId = 2, AgentType = "Agent Type 559832"};
        public static readonly AgentTypes AGENT_TYPE_C = new AgentTypes {AgentTypeId = 3, AgentType = "Agent Type 123124"};

        public static readonly TransmissionModes TRANSMISSION_MODE_A = new TransmissionModes {TransmissionModeId = 1, DisplayName = "Transmission Mode 964836"};
        public static readonly TransmissionModes TRANSMISSION_MODE_B = new TransmissionModes {TransmissionModeId = 2, DisplayName = "Transmission Mode 348729"};
        public static readonly TransmissionModes TRANSMISSION_MODE_C = new TransmissionModes {TransmissionModeId = 3, DisplayName = "Transmission Mode 212452"};
        public static readonly TransmissionModes TRANSMISSION_MODE_D = new TransmissionModes {TransmissionModeId = 4, DisplayName = "Transmission Mode 964836"};

        public static readonly AcquisitionModes ACQUISITION_MODE_A = new AcquisitionModes
        {
            AcquisitionModeId = 1,
            AcquisitionModeLabel = "Acquisition Mode 231238",
            AcquisitionModeDefinitionLabel = "Description of Acquisition Mode 231238"
        };

        public static readonly AcquisitionModes ACQUISITION_MODE_B = new AcquisitionModes
        {
            AcquisitionModeId = 2,
            AcquisitionModeLabel = "Acquisition Mode 623464",
            AcquisitionModeDefinitionLabel = "Description of Acquisition Mode 623464"
        };

        public static readonly AcquisitionModes ACQUISITION_MODE_C = new AcquisitionModes
        {
            AcquisitionModeId = 3,
            AcquisitionModeLabel = "Acquisition Mode 573634",
            AcquisitionModeDefinitionLabel = "Description of Acquisition Mode 573634"
        };

        public static readonly AcquisitionModes ACQUISITION_MODE_D = new AcquisitionModes
        {
            AcquisitionModeId = 4,
            AcquisitionModeLabel = "Acquisition Mode 250893",
            AcquisitionModeDefinitionLabel = "Description of Acquisition Mode 250893"
        };

        public static readonly AcquisitionModes ACQUISITION_MODE_E = new AcquisitionModes
        {
            AcquisitionModeId = 5,
            AcquisitionModeLabel = "Acquisition Mode 866398",
            AcquisitionModeDefinitionLabel = "Description of Acquisition Mode 866398"
        };

        public static readonly Diseases DISEASE_A = new Diseases
        {
            DiseaseId = 1,
            DiseaseName = "Apple",
            OutbreakPotentialAttributeId = null,
            XtblDiseaseAgents = null,
            XtblDiseaseAcquisitionMode = null,
            XtblDiseaseTransmissionMode = null,
            XtblDiseaseInterventions = null,
            DiseaseSpeciesIncubation = null,
            DiseaseSpeciesSymptomatic = null,
            BiosecurityRiskNavigation = null
        };

        public static readonly Diseases DISEASE_B = new Diseases
        {
            DiseaseId = 2,
            DiseaseName = "Banana",
            OutbreakPotentialAttributeId = 2,
            XtblDiseaseAgents = new List<XtblDiseaseAgents>(),
            XtblDiseaseAcquisitionMode = new List<XtblDiseaseAcquisitionMode>(),
            XtblDiseaseTransmissionMode = new List<XtblDiseaseTransmissionMode>(),
            XtblDiseaseInterventions = new List<XtblDiseaseInterventions>(),
            DiseaseSpeciesIncubation = new List<DiseaseSpeciesIncubation>(),
            DiseaseSpeciesSymptomatic = new List<DiseaseSpeciesSymptomatic>(),
            BiosecurityRiskNavigation = new BiosecurityRisk
            {
                BiosecurityRiskCode = "Biosecurity Risk Code 1298375",
                BiosecurityRiskDesc = null
            }
        };

        public static readonly Diseases DISEASE_C = new Diseases
        {
            DiseaseId = 3,
            DiseaseName = "Carrot",
            OutbreakPotentialAttributeId = 3,
            XtblDiseaseAgents = new List<XtblDiseaseAgents>
            {
                new XtblDiseaseAgents {Agent = new Agents {AgentId = 1, Agent = "Agent 859723", AgentType = AGENT_TYPE_A}}
            },
            XtblDiseaseAcquisitionMode = new List<XtblDiseaseAcquisitionMode>
            {
                new XtblDiseaseAcquisitionMode {SpeciesId = 999},
                new XtblDiseaseAcquisitionMode {SpeciesId = 232}
            },
            XtblDiseaseTransmissionMode = new List<XtblDiseaseTransmissionMode>
            {
                new XtblDiseaseTransmissionMode {SpeciesId = 929},
                new XtblDiseaseTransmissionMode {SpeciesId = 327}
            },
            XtblDiseaseInterventions = new List<XtblDiseaseInterventions>
            {
                new XtblDiseaseInterventions {SpeciesId = 648},
                new XtblDiseaseInterventions {SpeciesId = 578},
                new XtblDiseaseInterventions
                {
                    SpeciesId = (int) Species.Human, Intervention = new Interventions
                    {
                        InterventionId = 2947,
                        InterventionType = "1893hfj9087fa"
                    }
                },
                new XtblDiseaseInterventions
                {
                    SpeciesId = (int) Species.Human, Intervention = new Interventions
                    {
                        InterventionId = 5972,
                        InterventionType = "45893gh2jf8sad6g"
                    }
                }
            },
            DiseaseSpeciesIncubation = new List<DiseaseSpeciesIncubation>
            {
                new DiseaseSpeciesIncubation {SpeciesId = 293},
                new DiseaseSpeciesIncubation {SpeciesId = 248}
            },
            DiseaseSpeciesSymptomatic = new List<DiseaseSpeciesSymptomatic>
            {
                new DiseaseSpeciesSymptomatic {SpeciesId = 568},
                new DiseaseSpeciesSymptomatic {SpeciesId = 496}
            },
            BiosecurityRiskNavigation = new BiosecurityRisk
            {
                BiosecurityRiskCode = "Biosecurity Risk Code 15313513",
                BiosecurityRiskDesc = ""
            }
        };

        public static readonly Diseases DISEASE_D = new Diseases
        {
            DiseaseId = 4,
            DiseaseName = "Donut",
            OutbreakPotentialAttributeId = 4,
            XtblDiseaseAgents = new List<XtblDiseaseAgents>
            {
                new XtblDiseaseAgents {Agent = new Agents {AgentId = 2, Agent = "Agent 423123"}},
                new XtblDiseaseAgents {Agent = new Agents {AgentId = 3, Agent = "Agent 234897", AgentType = AGENT_TYPE_B}},
                new XtblDiseaseAgents {Agent = new Agents {AgentId = 4, Agent = "Agent 239034", AgentType = AGENT_TYPE_B}},
                new XtblDiseaseAgents {Agent = new Agents {AgentId = 5, Agent = "Agent 697208", AgentType = AGENT_TYPE_A}},
                new XtblDiseaseAgents {Agent = new Agents {AgentId = 6, Agent = "Agent 462234", AgentType = AGENT_TYPE_C}}
            },
            XtblDiseaseAcquisitionMode = new List<XtblDiseaseAcquisitionMode>
            {
                new XtblDiseaseAcquisitionMode
                {
                    SpeciesId = (int) Species.Human,
                    AcquisitionModeRank = 3,
                    AcquisitionMode = ACQUISITION_MODE_A
                }
            },
            XtblDiseaseTransmissionMode = new List<XtblDiseaseTransmissionMode>
            {
                new XtblDiseaseTransmissionMode {SpeciesId = (int) Species.Human, TransmissionMode = TRANSMISSION_MODE_A}
            },
            XtblDiseaseInterventions = new List<XtblDiseaseInterventions>
            {
                new XtblDiseaseInterventions
                {
                    SpeciesId = (int) Species.Human, Intervention = new Interventions
                    {
                        InterventionId = 1283975,
                        InterventionType = InterventionType.Prevention,
                        DisplayName = "Prevention Measure 1283975"
                    }
                }
            },
            DiseaseSpeciesIncubation = new List<DiseaseSpeciesIncubation>
            {
                new DiseaseSpeciesIncubation
                {
                    SpeciesId = (int) Species.Human,
                    IncubationMinimumSeconds = 123,
                    IncubationMaximumSeconds = 567,
                    IncubationAverageSeconds = 424
                }
            },
            DiseaseSpeciesSymptomatic = new List<DiseaseSpeciesSymptomatic>
            {
                new DiseaseSpeciesSymptomatic
                {
                    SpeciesId = (int) Species.Human,
                    SymptomaticMinimumSeconds = 123,
                    SymptomaticMaximumSeconds = 567,
                    SymptomaticAverageSeconds = 424
                }
            },
            BiosecurityRiskNavigation = new BiosecurityRisk
            {
                BiosecurityRiskCode = "Biosecurity Risk Code 6938983",
                BiosecurityRiskDesc = "Biosecurity Risk 6938983"
            }
        };

        public static readonly Diseases DISEASE_E = new Diseases
        {
            DiseaseId = 5,
            DiseaseName = "Egg",
            OutbreakPotentialAttributeId = 6,
            XtblDiseaseAcquisitionMode = new List<XtblDiseaseAcquisitionMode>
            {
                new XtblDiseaseAcquisitionMode
                {
                    SpeciesId = (int) Species.Human,
                    AcquisitionModeRank = 3,
                    AcquisitionMode = ACQUISITION_MODE_A
                },
                new XtblDiseaseAcquisitionMode
                {
                    SpeciesId = (int) Species.Human,
                    AcquisitionModeRank = 2,
                    AcquisitionMode = ACQUISITION_MODE_B
                },
                new XtblDiseaseAcquisitionMode
                {
                    SpeciesId = (int) Species.Human,
                    AcquisitionModeRank = 3,
                    AcquisitionMode = ACQUISITION_MODE_E
                },
                new XtblDiseaseAcquisitionMode
                {
                    SpeciesId = (int) Species.Human,
                    AcquisitionModeRank = 1,
                    AcquisitionMode = ACQUISITION_MODE_C
                },
                new XtblDiseaseAcquisitionMode
                {
                    SpeciesId = (int) Species.Human,
                    AcquisitionModeRank = 3,
                    AcquisitionMode = ACQUISITION_MODE_D
                }
            },
            XtblDiseaseTransmissionMode = new List<XtblDiseaseTransmissionMode>
            {
                new XtblDiseaseTransmissionMode {SpeciesId = (int) Species.Human, TransmissionMode = TRANSMISSION_MODE_A},
                new XtblDiseaseTransmissionMode {SpeciesId = (int) Species.Human, TransmissionMode = TRANSMISSION_MODE_B},
                new XtblDiseaseTransmissionMode {SpeciesId = (int) Species.Human, TransmissionMode = TRANSMISSION_MODE_C},
                new XtblDiseaseTransmissionMode {SpeciesId = (int) Species.Human, TransmissionMode = TRANSMISSION_MODE_D}
            },
            XtblDiseaseInterventions = new List<XtblDiseaseInterventions>
            {
                new XtblDiseaseInterventions
                {
                    SpeciesId = (int) Species.Human, Intervention = new Interventions
                    {
                        InterventionId = 3145515,
                        InterventionType = InterventionType.Prevention,
                        DisplayName = "Prevention Measure 3145515"
                    }
                },
                new XtblDiseaseInterventions
                {
                    SpeciesId = (int) Species.Human, Intervention = new Interventions
                    {
                        InterventionId = 4608920,
                        InterventionType = InterventionType.Prevention,
                        DisplayName = "Prevention Measure 4608920"
                    }
                },
                new XtblDiseaseInterventions
                {
                    SpeciesId = (int) Species.Human, Intervention = new Interventions
                    {
                        InterventionId = 2349812,
                        InterventionType = InterventionType.Prevention,
                        DisplayName = "Prevention Measure 2349812"
                    }
                }
            }
        };

        public BiodZebraContext DbContext { get; }

        public DiseaseQueryBuilderDatabaseFixture()
        {
            DbContext = new BiodZebraContext(new DbContextOptionsBuilder<BiodZebraContext>()
                .UseInMemoryDatabase("DiseaseQueryBuilderDatabaseFixture")
                .Options);

            DbContext.Diseases.Add(DISEASE_A);
            DbContext.Diseases.Add(DISEASE_B);
            DbContext.Diseases.Add(DISEASE_C);
            DbContext.Diseases.Add(DISEASE_D);
            DbContext.Diseases.Add(DISEASE_E);
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }

        public static IEnumerable<object[]> AcquisitionModesTestData()
        {
            return new[]
            {
                // Null
                new object[]
                {
                    1,
                    new AcquisitionModeModel[0]
                },

                // Empty list
                new object[]
                {
                    2,
                    new AcquisitionModeModel[0]
                },

                // Non-matching species id
                new object[]
                {
                    3,
                    new AcquisitionModeModel[0]
                },

                // Single Acquisition Mode
                new object[]
                {
                    4,
                    new[]
                    {
                        new AcquisitionModeModel
                        {
                            Id = 1,
                            Label = "Acquisition Mode 231238",
                            Description = "Description of Acquisition Mode 231238",
                            RankId = 3
                        }
                    }
                },

                // Single Acquisition Mode
                new object[]
                {
                    5,
                    new[]
                    {
                        new AcquisitionModeModel
                        {
                            Id = 3,
                            Label = "Acquisition Mode 573634",
                            Description = "Description of Acquisition Mode 573634",
                            RankId = 1
                        },
                        new AcquisitionModeModel
                        {
                            Id = 2,
                            Label = "Acquisition Mode 623464",
                            Description = "Description of Acquisition Mode 623464",
                            RankId = 2
                        },
                        new AcquisitionModeModel
                        {
                            Id = 1,
                            Label = "Acquisition Mode 231238",
                            Description = "Description of Acquisition Mode 231238",
                            RankId = 3
                        },
                        new AcquisitionModeModel
                        {
                            Id = 4,
                            Label = "Acquisition Mode 250893",
                            Description = "Description of Acquisition Mode 250893",
                            RankId = 3
                        },
                        new AcquisitionModeModel
                        {
                            Id = 5,
                            Label = "Acquisition Mode 866398",
                            Description = "Description of Acquisition Mode 866398",
                            RankId = 3
                        }
                    }
                }
            };
        }
    }
}