CREATE TABLE [surveillance].[Xtbl_RelatedArticlesTransLog] (
  [LogId]              INT IDENTITY(1,1) NOT NULL,
  [ModifiedDate]       DATETIMEOFFSET  NOT NULL,
  [Action]             NVARCHAR(128)   NOT NULL,
  [MainArticleId]      NVARCHAR (128)  NOT NULL,
  [RelatedArticleId]   NVARCHAR (128)  NOT NULL,
  PRIMARY KEY CLUSTERED ([LogId])
);
GO
