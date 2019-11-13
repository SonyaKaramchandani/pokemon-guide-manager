CREATE TABLE [surveillance].[Xtbl_RelatedArticlesTransLog] (
	[ModifiedDate]       DATETIMEOFFSET  NOT NULL,
	[Action]             NVARCHAR(128)   NOT NULL,
  [MainArticleId]      NVARCHAR (128)  NOT NULL,
  [RelatedArticleId]   NVARCHAR (128)  NOT NULL,
  CONSTRAINT [PK_surveillance.Xtbl_RelatedArticlesTransLog] PRIMARY KEY CLUSTERED (
    [MainArticleId],
    [RelatedArticleId],
    [ModifiedDate]
  )
);
GO
