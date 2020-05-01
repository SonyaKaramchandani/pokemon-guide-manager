CREATE TABLE [disease].[AcquisitionModes](
	[AcquisitionModeId] [int] NOT NULL,
	[DiseaseVectorId] [int] NOT NULL,
	[TransferModalityId] [int] NOT NULL,
	[Multiplier] [int] NULL,
	[AcquisitionModeLabel] [nvarchar](100) NULL,
	[AcquisitionModeDefinitionLabel] [nvarchar](500) NULL,
    CONSTRAINT [PK_AcquisitionModes] PRIMARY KEY CLUSTERED ([AcquisitionModeId] ASC),
    CONSTRAINT [FK_AcquisitionModes_DiseaseVectorId] FOREIGN KEY ([DiseaseVectorId]) 
		REFERENCES [disease].[DiseaseVectors] ([DiseaseVectorId]) ON DELETE CASCADE,
    CONSTRAINT [FK_AcquisitionModes_TransferModalityId] FOREIGN KEY ([TransferModalityId]) 
		REFERENCES [disease].[TransferModality] ([TransferModalityId]) ON DELETE CASCADE
)

GO

