CREATE TABLE [disease].[Xtbl_Disease_Interventions] (
    [DiseaseId]    INT NOT NULL,
    [SpeciesId]	   INT NOT NULL DEFAULT 1,
    [InterventionId] INT NOT NULL,
    CONSTRAINT [PK_Xtbl_Disease_Interventions] PRIMARY KEY CLUSTERED ([DiseaseId] ASC, [SpeciesId] ASC, [InterventionId] ASC),
    CONSTRAINT [FK_Xtbl_Disease_Interventions_DiseaseId] FOREIGN KEY ([DiseaseId]) REFERENCES [disease].[Diseases] ([DiseaseId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Xtbl_Disease_Interventions_InterventionId] FOREIGN KEY ([InterventionId]) REFERENCES [disease].[Interventions] ([InterventionId]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [idx_Xtbl_Disease_Interventions_InterventionId] ON [disease].[Xtbl_Disease_Interventions](InterventionId ASC);

GO

