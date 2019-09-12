CREATE TABLE [bd].[DiseaseSeverity] (
    [diseaseId]          INT        NOT NULL,
    [level]              INT        NOT NULL,
    [treatmentAvailable] FLOAT (53) NOT NULL,
    [chronic]            BIT        DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([diseaseId] ASC),
    FOREIGN KEY ([diseaseId]) REFERENCES [bd].[Disease] ([diseaseId]) ON DELETE CASCADE ON UPDATE CASCADE
);

