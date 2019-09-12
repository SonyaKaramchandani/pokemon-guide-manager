CREATE TABLE [bd].[DiseaseSeasonality] (
    [diseaseId]       INT        NOT NULL,
    [zone]            INT        NOT NULL,
    [fromMonth]       INT        NOT NULL,
    [toMonth]         INT        NOT NULL,
    [offSeasonWeight] FLOAT (53) DEFAULT ((0.0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([diseaseId] ASC, [zone] ASC),
    FOREIGN KEY ([diseaseId]) REFERENCES [bd].[Disease] ([diseaseId]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [diseaseSeasonality_diseaseId]
    ON [bd].[DiseaseSeasonality]([diseaseId] ASC);

