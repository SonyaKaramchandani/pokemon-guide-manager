CREATE TABLE [disease].[Xtbl_Disease_AlternateName] (
    [DiseaseId]     INT            NOT NULL,
    [AlternateName] NVARCHAR (200) NOT NULL,
    [IsColloquial]  BIT            NULL,
    CONSTRAINT [PK_Xtbl_Disease_AlternateName] PRIMARY KEY CLUSTERED ([DiseaseId] ASC, [AlternateName] ASC),
    CONSTRAINT [FK_Xtbl_Disease_AlternateName_DiseaseId] FOREIGN KEY ([DiseaseId]) REFERENCES [disease].[Diseases] ([DiseaseId]) ON DELETE CASCADE
);

