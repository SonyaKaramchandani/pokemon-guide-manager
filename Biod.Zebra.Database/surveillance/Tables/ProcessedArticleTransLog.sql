CREATE TABLE [surveillance].[ProcessedArticleTransLog] (
  [ModifiedDate]           DATETIMEOFFSET   NOT NULL,
  [Action]                 NVARCHAR(128)    NOT NULL,
  [ArticleId]              VARCHAR (128)    NOT NULL,
  [ArticleTitle]           NVARCHAR (500)   NULL,
  [SystemLastModifiedDate] DATETIME         NULL,
  [CertaintyScore]         DECIMAL (18, 16) NULL,
  [ArticleFeedId]          INT              NULL,
  [FeedURL]                NVARCHAR (2000)  NULL,
  [FeedSourceId]           VARCHAR (100)    NULL,
  [FeedPublishedDate]      DATETIME         NULL,
  [HamTypeId]              INT              NULL,
  [OriginalSourceURL]      NVARCHAR (2000)  NULL,
  [IsCompleted]            BIT              NULL,
  [SimilarClusterId]       DECIMAL(20, 2)   NULL,
  [OriginalLanguage]       VARCHAR (50)     NULL,
  [UserLastModifiedDate]   DATETIME         NULL,
  [LastUpdatedByUserName]  VARCHAR (64)     NULL,
  [Notes]                  NVARCHAR (MAX)   NULL,
  [ArticleBody]            NVARCHAR (MAX)   NULL,
  [IsRead]                 BIT              NULL,
  CONSTRAINT [PK_surveillance.ProcessedArticleTransLog] PRIMARY KEY CLUSTERED (
    [ArticleId],
    [ModifiedDate]
  )
);
GO
