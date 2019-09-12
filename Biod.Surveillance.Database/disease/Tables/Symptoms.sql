CREATE TABLE [disease].[Symptoms] (
    [SymptomId]         INT           NOT NULL,
    [Symptom]           VARCHAR (100) NOT NULL,
    [SystemId]          INT           NULL,
    [SymptomDefinition] VARCHAR (500) NULL,
    [DefinitionSource]  VARCHAR (500) NULL,
 	LastModified datetime,
   CONSTRAINT [PK_Symptoms] PRIMARY KEY CLUSTERED ([SymptomId] ASC),
    CONSTRAINT [FK_Symptoms_SystemId] FOREIGN KEY ([SystemId]) REFERENCES [disease].[Systems] ([SystemId]) ON DELETE SET NULL
);

