CREATE TABLE [bd].[DiseaseActivityMod] (
    [diseaseId] INT        NOT NULL,
    [activity]  INT        NOT NULL,
    [scale]     FLOAT (53) DEFAULT ((1.0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([diseaseId] ASC, [activity] ASC),
    FOREIGN KEY ([activity]) REFERENCES [bd].[Activity] ([id]) ON UPDATE CASCADE,
    FOREIGN KEY ([diseaseId]) REFERENCES [bd].[Disease] ([diseaseId]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [diseaseActivityMod_diseaseId]
    ON [bd].[DiseaseActivityMod]([diseaseId] ASC);

