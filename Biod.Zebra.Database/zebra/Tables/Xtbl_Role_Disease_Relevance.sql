CREATE TABLE [zebra].[Xtbl_Role_Disease_Relevance](
	[RoleId] [nvarchar](128) NOT NULL,
	[DiseaseId] [int] NOT NULL,
	[RelevanceId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
 CONSTRAINT [PK_Xtbl_Role_Disease_Relevance] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[DiseaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [zebra].[Xtbl_Role_Disease_Relevance]  ADD  CONSTRAINT [FK_Xtbl_Role_Disease_Relevance_AspNetRoles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [zebra].[Xtbl_Role_Disease_Relevance] CHECK CONSTRAINT [FK_Xtbl_Role_Disease_Relevance_AspNetRoles]
GO

ALTER TABLE [zebra].[Xtbl_Role_Disease_Relevance]  ADD  CONSTRAINT [FK_Xtbl_Role_Disease_Relevance_Diseases] FOREIGN KEY([DiseaseId])
REFERENCES [disease].[Diseases] ([DiseaseId]) ON DELETE CASCADE
GO

ALTER TABLE [zebra].[Xtbl_Role_Disease_Relevance] CHECK CONSTRAINT [FK_Xtbl_Role_Disease_Relevance_Diseases]
GO

ALTER TABLE [zebra].[Xtbl_Role_Disease_Relevance]  ADD  CONSTRAINT [FK_Xtbl_Role_Disease_Relevance_RelevanceState] FOREIGN KEY([StateId])
REFERENCES [zebra].[RelevanceState] ([StateId])
GO

ALTER TABLE [zebra].[Xtbl_Role_Disease_Relevance] CHECK CONSTRAINT [FK_Xtbl_Role_Disease_Relevance_RelevanceState]
GO

ALTER TABLE [zebra].[Xtbl_Role_Disease_Relevance]  ADD  CONSTRAINT [FK_Xtbl_Role_Disease_Relevance_RelevanceType] FOREIGN KEY([RelevanceId])
REFERENCES [zebra].[RelevanceType] ([RelevanceId])
GO

ALTER TABLE [zebra].[Xtbl_Role_Disease_Relevance] CHECK CONSTRAINT [FK_Xtbl_Role_Disease_Relevance_RelevanceType]
GO

