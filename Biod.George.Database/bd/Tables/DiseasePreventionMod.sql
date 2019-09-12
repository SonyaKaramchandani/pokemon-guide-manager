CREATE TABLE [bd].[DiseasePreventionMod] (
    [prevention]  INT NOT NULL,
    [conditionId] INT NOT NULL,
    [messageId]   INT NOT NULL,
    PRIMARY KEY CLUSTERED ([prevention] ASC, [conditionId] ASC),
    FOREIGN KEY ([conditionId]) REFERENCES [bd].[Condition] ([conditionId]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([prevention]) REFERENCES [bd].[DiseasePrevention] ([id])
);

