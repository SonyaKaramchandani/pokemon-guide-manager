CREATE TABLE [surveillance].[Xtbl_RelatedArticles] (
    [MainArticleId]    VARCHAR (128) NOT NULL,
    [RelatedArticleId] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_Xtbl_RelatedArticles] PRIMARY KEY CLUSTERED ([MainArticleId] ASC, [RelatedArticleId] ASC),
    CONSTRAINT [FK_Xtbl_RelatedArticles_MainArticleId] FOREIGN KEY ([MainArticleId]) REFERENCES [surveillance].[ProcessedArticle] ([ArticleId]) ON DELETE CASCADE
);

