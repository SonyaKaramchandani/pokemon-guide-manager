CREATE TABLE [bd].[DiseaseSymptom] (
    [diseaseId]        INT           NOT NULL,
    [symptomId]        INT           NOT NULL,
    [associationScore] FLOAT (53)    DEFAULT ((0.0)) NOT NULL,
    [notes]            VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([diseaseId] ASC, [symptomId] ASC),
    FOREIGN KEY ([diseaseId]) REFERENCES [bd].[Disease] ([diseaseId]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([symptomId]) REFERENCES [bd].[Symptom] ([symptomId]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [diseaseSymptom_diseaseId]
    ON [bd].[DiseaseSymptom]([diseaseId] ASC);

