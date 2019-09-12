CREATE TABLE [bd].[DiseaseSeverityMod] (
    [diseaseId]          INT        NOT NULL,
    [conditionId]        INT        NOT NULL,
    [addend]             INT        DEFAULT ((1)) NOT NULL,
    [conditionParameter] FLOAT (53) DEFAULT ((-1.0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([diseaseId] ASC, [conditionId] ASC),
    FOREIGN KEY ([conditionId]) REFERENCES [bd].[Condition] ([conditionId]) ON UPDATE CASCADE,
    FOREIGN KEY ([diseaseId]) REFERENCES [bd].[Disease] ([diseaseId]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [diseaseSeverityMod_diseaseId]
    ON [bd].[DiseaseSeverityMod]([diseaseId] ASC);

