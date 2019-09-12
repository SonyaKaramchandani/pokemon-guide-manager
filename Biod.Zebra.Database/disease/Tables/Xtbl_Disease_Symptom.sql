CREATE TABLE [disease].[Xtbl_Disease_Symptom] (
    [DiseaseId]        INT          NOT NULL,
    [SpeciesId]		   INT          NOT NULL DEFAULT 1,
    [SymptomId]        INT          NOT NULL,
    [Frequency]        VARCHAR (50) NULL,
    [AssociationScore] INT          NULL,
    CONSTRAINT [PK_Xtbl_Disease_Symptom] PRIMARY KEY CLUSTERED ([DiseaseId] ASC, SpeciesId ASC, [SymptomId] ASC),
    CONSTRAINT [FK_Xtbl_Disease_Symptom_DiseaseId] FOREIGN KEY ([DiseaseId]) REFERENCES [disease].[Diseases] ([DiseaseId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Xtbl_Disease_Symptom_SpeciesId] FOREIGN KEY ([SpeciesId]) REFERENCES [disease].[Species] ([SpeciesId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Xtbl_Disease_Symptom_SymptomId] FOREIGN KEY ([SymptomId]) REFERENCES [disease].[Symptoms] ([SymptomId]) ON DELETE CASCADE
);

