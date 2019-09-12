CREATE TABLE [bd].[DiseaseTransmission] (
    [diseaseId] INT          NOT NULL,
    [mode]      INT          NOT NULL,
    [rank]      INT          DEFAULT ((1)) NOT NULL,
    [agents]    VARCHAR (64) NULL,
    [contact]   VARCHAR (64) NULL,
    [actions]   VARCHAR (64) NULL,
    PRIMARY KEY CLUSTERED ([diseaseId] ASC, [mode] ASC),
    FOREIGN KEY ([diseaseId]) REFERENCES [bd].[Disease] ([diseaseId]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([mode]) REFERENCES [bd].[TransmissionMode] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [diseaseTransmission_diseaseId]
    ON [bd].[DiseaseTransmission]([diseaseId] ASC);

