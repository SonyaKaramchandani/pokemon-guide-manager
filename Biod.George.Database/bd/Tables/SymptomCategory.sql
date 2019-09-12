CREATE TABLE [bd].[SymptomCategory] (
    [symptomCategoryId] INT           IDENTITY (1, 1) NOT NULL,
    [name]              VARCHAR (256) NOT NULL,
    [notes]             VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([symptomCategoryId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [symptomCategory_name]
    ON [bd].[SymptomCategory]([name] ASC);

