CREATE TABLE [disease].[InterventionSpecies] (
    [InterventionId]   INT            NOT NULL,
    SpeciesId		   INT			  NOT NULL DEFAULT 1,
    [RiskReduction]  DECIMAL (4, 2)   NULL,
    [Duration]       VARCHAR (100)    NULL,
    CONSTRAINT [PK_InterventionSpecies] PRIMARY KEY CLUSTERED ([InterventionId], SpeciesId),
    CONSTRAINT [FK_InterventionSpecies_InterventionId] FOREIGN KEY ([InterventionId]) REFERENCES [disease].[Interventions] ([InterventionId]) ON DELETE CASCADE,
    CONSTRAINT [FK_InterventionSpecies_SpeciesId] FOREIGN KEY ([SpeciesId]) REFERENCES [disease].[Species] ([SpeciesId]) ON DELETE CASCADE
);

