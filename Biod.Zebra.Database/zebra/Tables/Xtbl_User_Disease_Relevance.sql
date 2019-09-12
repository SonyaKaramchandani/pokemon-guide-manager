CREATE TABLE [zebra].[Xtbl_User_Disease_Relevance](
	[UserId] [nvarchar](128) NOT NULL,
	[DiseaseId] [int] NOT NULL,
	[RelevanceId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
 CONSTRAINT [PK_Xtbl_User_Disease_Relevance] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[DiseaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [zebra].[Xtbl_User_Disease_Relevance]  ADD  CONSTRAINT [FK_Xtbl_User_Disease_Relevance_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [zebra].[Xtbl_User_Disease_Relevance] CHECK CONSTRAINT [FK_Xtbl_User_Disease_Relevance_AspNetUsers]
GO

ALTER TABLE [zebra].[Xtbl_User_Disease_Relevance]  ADD  CONSTRAINT [FK_Xtbl_User_Disease_Relevance_Diseases] FOREIGN KEY([DiseaseId])
REFERENCES [disease].[Diseases] ([DiseaseId]) ON DELETE CASCADE
GO

ALTER TABLE [zebra].[Xtbl_User_Disease_Relevance] CHECK CONSTRAINT [FK_Xtbl_User_Disease_Relevance_Diseases]
GO

ALTER TABLE [zebra].[Xtbl_User_Disease_Relevance]  ADD  CONSTRAINT [FK_Xtbl_User_Disease_Relevance_RelevanceState] FOREIGN KEY([StateId])
REFERENCES [zebra].[RelevanceState] ([StateId])
GO

ALTER TABLE [zebra].[Xtbl_User_Disease_Relevance] CHECK CONSTRAINT [FK_Xtbl_User_Disease_Relevance_RelevanceState]
GO

ALTER TABLE [zebra].[Xtbl_User_Disease_Relevance]  ADD  CONSTRAINT [FK_Xtbl_User_Disease_Relevance_RelevanceType] FOREIGN KEY([RelevanceId])
REFERENCES [zebra].[RelevanceType] ([RelevanceId])
GO

ALTER TABLE [zebra].[Xtbl_User_Disease_Relevance] CHECK CONSTRAINT [FK_Xtbl_User_Disease_Relevance_RelevanceType]
GO