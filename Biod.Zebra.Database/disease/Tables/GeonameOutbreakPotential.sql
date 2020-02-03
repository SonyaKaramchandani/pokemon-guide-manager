CREATE TABLE [disease].[GeonameOutbreakPotential] (
    [GeonameId]                    INT            NOT NULL,
    [DiseaseId]                    INT            NOT NULL,
    [OutbreakPotentialId]          INT            NOT NULL,
    [OutbreakPotentialAttributeId] INT            NOT NULL,
    [EffectiveMessage]             NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_GeonameOutbreakPotential] PRIMARY KEY CLUSTERED ([GeonameId] ASC, [DiseaseId] ASC),
    CONSTRAINT [FK_GeonameOutbreakPotential_Disease] FOREIGN KEY ([DiseaseId]) REFERENCES [disease].[Diseases] ([DiseaseId]) ON DELETE CASCADE,
    CONSTRAINT [FK_GeonameOutbreakPotential_Geoname] FOREIGN KEY ([GeonameId]) REFERENCES [place].[Geonames] ([GeonameId]) ON DELETE CASCADE
);
GO
