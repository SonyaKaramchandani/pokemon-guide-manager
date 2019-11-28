CREATE TABLE [disease].[DiseaseSpeciesIncubation] (
    [DiseaseId]                       INT             NOT NULL,
    [SpeciesId]						  INT             NOT NULL DEFAULT 1,
    [IncubationAverageSeconds]        BIGINT		  NULL,
    [IncubationMinimumSeconds]        BIGINT		  NULL,
    [IncubationMaximumSeconds]        BIGINT		  NULL,
    CONSTRAINT [PK_DiseaseSpeciesIncubation] PRIMARY KEY CLUSTERED ([DiseaseId], SpeciesId),
    CONSTRAINT FK_DiseaseSpeciesIncubation_DiseaseId FOREIGN KEY (DiseaseId) REFERENCES disease.Diseases(DiseaseId) ON DELETE CASCADE,
    CONSTRAINT FK_DiseaseSpeciesIncubation_SpeciesId FOREIGN KEY (SpeciesId) REFERENCES disease.Species(SpeciesId) ON DELETE CASCADE
);

