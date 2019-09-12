CREATE TABLE [disease].[DiseaseSpeciesSymptomatic] (
    [DiseaseId]                       INT             NOT NULL,
    [SpeciesId]						  INT             NOT NULL DEFAULT 1,
    [SymptomaticAverageDays]          DECIMAL (10, 2) NULL,
    [SymptomaticMinimumDays]          DECIMAL (10, 2) NULL,
    [SymptomaticMaximumDays]          DECIMAL (10, 2) NULL,
    CONSTRAINT [PK_DiseaseSpeciesSymptomatic] PRIMARY KEY CLUSTERED ([DiseaseId], SpeciesId),
    CONSTRAINT FK_DiseaseSpeciesSymptomatic_DiseaseId FOREIGN KEY (DiseaseId) REFERENCES disease.Diseases(DiseaseId) ON DELETE CASCADE,
    CONSTRAINT FK_DiseaseSpeciesSymptomatic_SpeciesId FOREIGN KEY (SpeciesId) REFERENCES disease.Species(SpeciesId) ON DELETE CASCADE
);

