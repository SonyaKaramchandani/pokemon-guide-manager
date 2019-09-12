CREATE TABLE [bd].[DiseaseIncubation] (
    [diseaseId]   INT           NOT NULL,
    [minimumDays] FLOAT (53)    DEFAULT ((0.0)) NOT NULL,
    [maximumDays] FLOAT (53)    NOT NULL,
    [averageDays] FLOAT (53)    NOT NULL,
    [notes]       VARCHAR (512) NULL,
    [source]      VARCHAR (MAX) NULL,
    FOREIGN KEY ([diseaseId]) REFERENCES [bd].[Disease] ([diseaseId]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [diseaseIncubation_diseaseId]
    ON [bd].[DiseaseIncubation]([diseaseId] ASC);

