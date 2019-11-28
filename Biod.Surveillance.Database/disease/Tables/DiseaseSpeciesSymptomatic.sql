CREATE TABLE [disease].[DiseaseSpeciesSymptomatic] (
    [DiseaseId]                       INT             NOT NULL,
    [SpeciesId]						  INT             NOT NULL DEFAULT 1,
    [SymptomaticAverageSeconds]       BIGINT		  NULL,
    [SymptomaticMinimumSeconds]       BIGINT		  NULL,
    [SymptomaticMaximumSeconds]       BIGINT		  NULL,
    CONSTRAINT [PK_DiseaseSpeciesSymptomatic] PRIMARY KEY CLUSTERED ([DiseaseId], SpeciesId),
    CONSTRAINT FK_DiseaseSpeciesSymptomatic_DiseaseId FOREIGN KEY (DiseaseId) REFERENCES disease.Diseases(DiseaseId) ON DELETE CASCADE,
    CONSTRAINT FK_DiseaseSpeciesSymptomatic_SpeciesId FOREIGN KEY (SpeciesId) REFERENCES disease.Species(SpeciesId) ON DELETE CASCADE
);

