CREATE TABLE [surveillance].[ProcessedArticle] (
  [ArticleId]              VARCHAR (128)    NOT NULL,
  [ArticleTitle]           NVARCHAR (500)   NULL,
  [SystemLastModifiedDate] DATETIME         NOT NULL,
  [CertaintyScore]         DECIMAL (18, 16) NULL,
  [ArticleFeedId]          INT              NULL,
  [FeedURL]                NVARCHAR (2000)  NULL,
  [FeedSourceId]           VARCHAR (100)    NULL,
  [FeedPublishedDate]      DATETIME         NOT NULL,
  [HamTypeId]              INT              NULL,
  [OriginalSourceURL]      NVARCHAR (2000)  NULL,
  [IsCompleted]            BIT              NULL,
  [SimilarClusterId]       DECIMAL(20, 2)   NULL,
  [OriginalLanguage]       VARCHAR (50)     NULL,
  [UserLastModifiedDate]   DATETIME         NULL,
  [LastUpdatedByUserName]  VARCHAR (64)     NULL,
  [Notes]                  NVARCHAR (MAX)   NULL,
  [ArticleBody]            NVARCHAR (MAX)   NULL,
  [IsRead]                 BIT DEFAULT (0)  NULL,
  [IsImportant]            BIT DEFAULT (0)  NULL,
  CONSTRAINT [PK_ProcessedArticle] PRIMARY KEY CLUSTERED ([ArticleId] ASC),
  CONSTRAINT [FK_ProcessedArticle_ArticleFeed] FOREIGN KEY ([ArticleFeedId]) REFERENCES [surveillance].[ArticleFeed] ([ArticleFeedId]) ON DELETE SET NULL,
  CONSTRAINT [FK_ProcessedArticle_HamType] FOREIGN KEY ([HamTypeId]) REFERENCES [surveillance].[HamType] ([HamTypeId]) ON DELETE SET NULL
);
GO

--index
CREATE NONCLUSTERED INDEX [idx_ProcessedArticle_HamTypeId]
  ON [surveillance].[ProcessedArticle](HamTypeId ASC);
GO

--SimilarClusterId
CREATE NONCLUSTERED INDEX [idx_ProcessedArticle_SimilarClusterId]
  ON [surveillance].[ProcessedArticle](SimilarClusterId ASC);
GO

--IsCompleted
CREATE NONCLUSTERED INDEX [idx_ProcessedArticle_IsCompleted]
  ON [surveillance].[ProcessedArticle](IsCompleted ASC);
GO

--FeedPublishedDate
CREATE NONCLUSTERED INDEX [idx_ProcessedArticle_FeedPublishedDate]
  ON [surveillance].[ProcessedArticle](FeedPublishedDate ASC);
GO

--UserLastModifiedDate
CREATE NONCLUSTERED INDEX [idx_ProcessedArticle_UserLastModifiedDate]
  ON [surveillance].[ProcessedArticle](UserLastModifiedDate ASC);
GO

--SystemLastModifiedDate
CREATE NONCLUSTERED INDEX [idx_ProcessedArticle_SystemLastModifiedDate]
  ON [surveillance].[ProcessedArticle](SystemLastModifiedDate ASC);
GO

--ArticleFeedId
CREATE NONCLUSTERED INDEX [idx_ProcessedArticle_ArticleFeedId]
  ON [surveillance].[ProcessedArticle](ArticleFeedId ASC);
GO

CREATE TRIGGER surveillance.utr_ProcessedArticleTransLog_inserted
ON surveillance.ProcessedArticle
AFTER INSERT 
AS
  INSERT INTO surveillance.ProcessedArticleTransLog
  SELECT SYSDATETIMEOFFSET(), 'Inserted', inserted.* FROM inserted
GO

CREATE TRIGGER surveillance.utr_ProcessedArticleTransLog_updated
ON surveillance.ProcessedArticle
AFTER UPDATE 
AS
  INSERT INTO surveillance.ProcessedArticleTransLog
  SELECT SYSDATETIMEOFFSET(), 'Updated', inserted.* FROM inserted
GO

CREATE TRIGGER surveillance.utr_ProcessedArticleTransLog_deleted
ON surveillance.ProcessedArticle
AFTER DELETE 
AS
  INSERT INTO surveillance.ProcessedArticleTransLog (ModifiedDate, Action, ArticleId)
  SELECT SYSDATETIMEOFFSET(), 'Deleted', deleted.ArticleId
  FROM deleted
GO
