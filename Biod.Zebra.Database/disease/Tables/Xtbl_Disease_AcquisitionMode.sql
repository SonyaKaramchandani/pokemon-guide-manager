CREATE TABLE [disease].[Xtbl_Disease_AcquisitionMode](
	[DiseaseId] [int] NOT NULL,
	[SpeciesId] [int] NOT NULL,
	[AcquisitionModeId] [int] NOT NULL,
	[AcquisitionModeRank] [int] NOT NULL,
 CONSTRAINT [PK_Xtbl_Disease_AcquisitionMode] PRIMARY KEY CLUSTERED 
(
	[DiseaseId] ASC,
	[SpeciesId] ASC,
	[AcquisitionModeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [disease].[Xtbl_Disease_AcquisitionMode] ADD  DEFAULT ((1)) FOR [SpeciesId]
GO

ALTER TABLE [disease].[Xtbl_Disease_AcquisitionMode]  WITH CHECK ADD  CONSTRAINT [FK_Xtbl_Disease_AcquisitionMode_AcquisitionModeId] FOREIGN KEY([AcquisitionModeId])
REFERENCES [disease].[AcquisitionModes] ([AcquisitionModeId])
ON DELETE CASCADE
GO

ALTER TABLE [disease].[Xtbl_Disease_AcquisitionMode] CHECK CONSTRAINT [FK_Xtbl_Disease_AcquisitionMode_AcquisitionModeId]
GO

ALTER TABLE [disease].[Xtbl_Disease_AcquisitionMode]  WITH CHECK ADD  CONSTRAINT [FK_Xtbl_Disease_AcquisitionMode_DiseaseId] FOREIGN KEY([DiseaseId])
REFERENCES [disease].[Diseases] ([DiseaseId])
ON DELETE CASCADE
GO

ALTER TABLE [disease].[Xtbl_Disease_AcquisitionMode] CHECK CONSTRAINT [FK_Xtbl_Disease_AcquisitionMode_DiseaseId]
GO

ALTER TABLE [disease].[Xtbl_Disease_AcquisitionMode]  WITH CHECK ADD  CONSTRAINT [FK_Xtbl_Disease_AcquisitionMode_SpeciesId] FOREIGN KEY([SpeciesId])
REFERENCES [disease].[Species] ([SpeciesId])
ON DELETE CASCADE
GO

ALTER TABLE [disease].[Xtbl_Disease_AcquisitionMode] CHECK CONSTRAINT [FK_Xtbl_Disease_AcquisitionMode_SpeciesId]
GO

