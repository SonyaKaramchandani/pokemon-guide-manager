CREATE TABLE [disease].[Xtbl_Disease_CustomGroup](
	[DiseaseId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_Xtbl_Disease_CustomGroup] PRIMARY KEY CLUSTERED 
(
	[DiseaseId] ASC,
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [disease].[Xtbl_Disease_CustomGroup]  ADD  CONSTRAINT [FK_Xtbl_Disease_CustomGroup_CustomGroups] FOREIGN KEY([GroupId])
REFERENCES [disease].[CustomGroups] ([GroupId])
GO

ALTER TABLE [disease].[Xtbl_Disease_CustomGroup] CHECK CONSTRAINT [FK_Xtbl_Disease_CustomGroup_CustomGroups]
GO

ALTER TABLE [disease].[Xtbl_Disease_CustomGroup]  ADD  CONSTRAINT [FK_Xtbl_Disease_CustomGroup_Diseases] FOREIGN KEY([DiseaseId])
REFERENCES [disease].[Diseases] ([DiseaseId])
GO

ALTER TABLE [disease].[Xtbl_Disease_CustomGroup] CHECK CONSTRAINT [FK_Xtbl_Disease_CustomGroup_Diseases]
GO
