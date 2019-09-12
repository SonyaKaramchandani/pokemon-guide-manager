CREATE TABLE [disease].[Xtbl_Disease_Preventions] (
    [DiseaseId]    INT NOT NULL,
    [PreventionId] INT NOT NULL,
    CONSTRAINT [PK_Xtbl_Disease_Preventions] PRIMARY KEY CLUSTERED ([DiseaseId] ASC, [PreventionId] ASC),
    CONSTRAINT [FK_Xtbl_Disease_Preventions_DiseaseId] FOREIGN KEY ([DiseaseId]) REFERENCES [disease].[Diseases] ([DiseaseId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Xtbl_Disease_Preventions_PreventionId] FOREIGN KEY ([PreventionId]) REFERENCES [disease].[Preventions] ([PreventionId]) ON DELETE CASCADE
);

