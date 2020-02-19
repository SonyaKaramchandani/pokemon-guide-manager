CREATE TABLE [disease].[AcquisitionModes](
	[AcquisitionModeId] [int] NOT NULL,
	[DiseaseVectorId] [int] NOT NULL,
	[TransferModalityId] [int] NOT NULL,
	[Multiplier] [int] NULL,
	[IsDirect] [bit] NULL,
	[AcquisitionModeLabel] [nvarchar](100) NULL,
	[AcquisitionModeDefinitionLabel] [nvarchar](300) NULL,
 CONSTRAINT [PK_AcquisitionModes] PRIMARY KEY CLUSTERED 
(
	[AcquisitionModeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

