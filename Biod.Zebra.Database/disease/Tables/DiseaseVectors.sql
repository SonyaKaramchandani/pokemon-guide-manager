CREATE TABLE [disease].[DiseaseVectors](
	[DiseaseVectorId] int NOT NULL,
	[DiseaseVector] nvarchar(100) NOT NULL,
	[DiseaseVectorCategory] nvarchar(200) NULL,
    CONSTRAINT [PK_DiseaseVectors] PRIMARY KEY CLUSTERED ([DiseaseVectorId] ASC)
	)
GO

