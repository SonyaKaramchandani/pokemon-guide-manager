﻿CREATE TABLE [disease].[Xtbl_Disease_CustomGroup](
	[DiseaseId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_Xtbl_Disease_CustomGroup] PRIMARY KEY CLUSTERED 
(
	[DiseaseId] ASC,
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [idx_Xtbl_CustomGroup_AgentId] ON [disease].[Xtbl_Disease_CustomGroup](GroupId ASC);

GO

ALTER TABLE [disease].[Xtbl_Disease_CustomGroup]  ADD  CONSTRAINT [FK_Xtbl_Disease_CustomGroup_CustomGroups] FOREIGN KEY([GroupId])
REFERENCES [disease].[CustomGroups] ([GroupId]) ON DELETE CASCADE
GO

ALTER TABLE [disease].[Xtbl_Disease_CustomGroup] CHECK CONSTRAINT [FK_Xtbl_Disease_CustomGroup_CustomGroups] 
GO

ALTER TABLE [disease].[Xtbl_Disease_CustomGroup]  ADD  CONSTRAINT [FK_Xtbl_Disease_CustomGroup_Diseases] FOREIGN KEY([DiseaseId])
REFERENCES [disease].[Diseases] ([DiseaseId]) ON DELETE CASCADE
GO

ALTER TABLE [disease].[Xtbl_Disease_CustomGroup] CHECK CONSTRAINT [FK_Xtbl_Disease_CustomGroup_Diseases]
GO
