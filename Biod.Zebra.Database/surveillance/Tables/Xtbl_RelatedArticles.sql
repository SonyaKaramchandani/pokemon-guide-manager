CREATE TABLE [surveillance].[Xtbl_RelatedArticles] (
    [MainArticleId]    VARCHAR (128) NOT NULL,
    [RelatedArticleId] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_Xtbl_RelatedArticles] PRIMARY KEY CLUSTERED ([MainArticleId] ASC, [RelatedArticleId] ASC),
    CONSTRAINT [FK_Xtbl_RelatedArticles_MainArticleId] FOREIGN KEY ([MainArticleId]) REFERENCES [surveillance].[ProcessedArticle] ([ArticleId]) ON DELETE CASCADE
);

GO

CREATE TRIGGER surveillance.utr_Xtbl_RelatedArticlesTransLog_inserted
ON surveillance.Xtbl_RelatedArticles
AFTER INSERT 
AS
	INSERT INTO surveillance.Xtbl_RelatedArticlesTransLog
	SELECT SYSDATETIMEOFFSET(), 'Inserted', inserted.* FROM inserted
GO

CREATE TRIGGER surveillance.utr_Xtbl_RelatedArticleTransLog_updated
ON surveillance.Xtbl_RelatedArticles
AFTER UPDATE 
AS
	INSERT INTO surveillance.Xtbl_RelatedArticlesTransLog
	SELECT SYSDATETIMEOFFSET(), 'Updated', inserted.* FROM inserted
GO

CREATE TRIGGER surveillance.utr_Xtbl_RelatedArticlesTransLog_deleted
ON surveillance.Xtbl_RelatedArticles
AFTER DELETE 
AS
	INSERT INTO surveillance.Xtbl_RelatedArticlesTransLog (ModifiedDate, Action, [MainArticleId], [RelatedArticleId])
	SELECT SYSDATETIMEOFFSET(), 'Deleted', deleted.[MainArticleId], deleted.[RelatedArticleId]
  FROM deleted
GO
