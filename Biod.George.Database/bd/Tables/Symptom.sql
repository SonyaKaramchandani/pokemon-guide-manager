CREATE TABLE [bd].[Symptom] (
    [symptomId]         INT           IDENTITY (1, 1) NOT NULL,
    [name]              VARCHAR (256) NOT NULL,
    [symptomCategoryId] INT           NOT NULL,
    [altNames]          VARCHAR (256) NULL,
    [definition]        VARCHAR (MAX) NULL,
    [definitionSource]  VARCHAR (256) NULL,
    PRIMARY KEY CLUSTERED ([symptomId] ASC),
    FOREIGN KEY ([symptomCategoryId]) REFERENCES [bd].[SymptomCategory] ([symptomCategoryId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [symptom_name]
    ON [bd].[Symptom]([name] ASC);

