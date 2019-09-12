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
    [SimilarClusterId]       DECIMAL (20, 2)  NULL,
    [OriginalLanguage]       VARCHAR (50)     NULL,
    [UserLastModifiedDate]   DATETIME         NULL,
    [LastUpdatedByUserName]  VARCHAR (64)     NULL,
    [Notes]                  NVARCHAR (MAX)   NULL,
    [ArticleBody]            NVARCHAR (MAX)   NULL,
    [IsRead]                 BIT              DEFAULT ((0)) NULL,
    CONSTRAINT [PK_ProcessedArticle] PRIMARY KEY CLUSTERED ([ArticleId] ASC),
    CONSTRAINT [FK_ProcessedArticle_ArticleFeed] FOREIGN KEY ([ArticleFeedId]) REFERENCES [surveillance].[ArticleFeed] ([ArticleFeedId]) ON DELETE SET NULL,
    CONSTRAINT [FK_ProcessedArticle_HamType] FOREIGN KEY ([HamTypeId]) REFERENCES [surveillance].[HamType] ([HamTypeId]) ON DELETE SET NULL
);

