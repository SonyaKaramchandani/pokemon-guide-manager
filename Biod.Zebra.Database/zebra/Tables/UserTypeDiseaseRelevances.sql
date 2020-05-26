CREATE TABLE [zebra].[UserTypeDiseaseRelevances] (
  [UserTypeId]   UNIQUEIDENTIFIER NOT NULL,
  [DiseaseId]    [int] NOT NULL,
  [RelevanceId]  [int] NOT NULL,
  CONSTRAINT [PK_UserTypeDiseaseRelevances] PRIMARY KEY CLUSTERED ([UserTypeId] ASC, [DiseaseId] ASC),
  CONSTRAINT [FK_UserTypeDiseaseRelevances_UserTypes] FOREIGN KEY ([UserTypeId]) REFERENCES [dbo].[UserTypes] ([Id]),
  CONSTRAINT [FK_UserTypeDiseaseRelevances_Disease] FOREIGN KEY ([DiseaseId]) REFERENCES [disease].[Diseases] ([DiseaseId]),
  CONSTRAINT [FK_UserTypeDiseaseRelevances_Relevance] FOREIGN KEY ([RelevanceId]) REFERENCES [zebra].[RelevanceType] ([RelevanceId])
)
GO