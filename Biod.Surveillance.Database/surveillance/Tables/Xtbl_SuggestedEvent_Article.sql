﻿Create TABLE [surveillance].[Xtbl_SuggestedEvent_Article](
	SuggestedEventId varchar(30) NOT NULL,
	ArticleId varchar(128) NOT NULL,
	CONSTRAINT [PK_Xtbl_SuggestedEvent_Article] PRIMARY KEY CLUSTERED (SuggestedEventId, ArticleId),
	CONSTRAINT [FK_Xtbl_SuggestedEvent_Article_ArticleId] FOREIGN KEY (ArticleId) 
		REFERENCES [surveillance].[ProcessedArticle] (ArticleId) ON DELETE CASCADE,
	CONSTRAINT FK_Xtbl_Xtbl_SuggestedEvent_Article_SuggestedEventId FOREIGN KEY (SuggestedEventId) 
		REFERENCES [surveillance].[SuggestedEvent] (SuggestedEventId) ON DELETE CASCADE
);
GO

CREATE NONCLUSTERED INDEX [idx_Xtbl_SuggestedEvent_Article_ArticleId] ON [surveillance].Xtbl_SuggestedEvent_Article([ArticleId] ASC)
GO
