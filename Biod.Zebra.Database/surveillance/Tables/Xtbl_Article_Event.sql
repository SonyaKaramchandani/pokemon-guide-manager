CREATE TABLE [surveillance].[Xtbl_Article_Event] (
    [ArticleId] VARCHAR (128) NOT NULL,
    [EventId]   INT           NOT NULL,
    CONSTRAINT [PK_Article_Event] PRIMARY KEY CLUSTERED ([ArticleId] ASC, [EventId] ASC),
    CONSTRAINT [FK_Xtbl_Article_Event_Event] FOREIGN KEY ([EventId]) REFERENCES [surveillance].[Event] ([EventId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Xtbl_Article_Event_ProcessedArticle] FOREIGN KEY ([ArticleId]) REFERENCES [surveillance].[ProcessedArticle] ([ArticleId]) ON DELETE CASCADE
);

