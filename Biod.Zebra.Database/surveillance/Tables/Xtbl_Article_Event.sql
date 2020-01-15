CREATE TABLE [surveillance].[Xtbl_Article_Event] (
    [ArticleId] VARCHAR (128) NOT NULL,
    [EventId]   INT           NOT NULL,
    CONSTRAINT [PK_Article_Event] PRIMARY KEY CLUSTERED ([ArticleId] ASC, [EventId] ASC),
    CONSTRAINT [FK_Xtbl_Article_Event_Event] FOREIGN KEY ([EventId]) REFERENCES [surveillance].[Event] ([EventId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Xtbl_Article_Event_ProcessedArticle] FOREIGN KEY ([ArticleId]) REFERENCES [surveillance].[ProcessedArticle] ([ArticleId]) ON DELETE CASCADE
);
GO

CREATE NONCLUSTERED INDEX [idx_Xtbl_Article_Event_EventId] ON [surveillance].[Xtbl_Article_Event]([EventId] ASC)
GO

CREATE TRIGGER surveillance.utr_Xtbl_Article_EventTransLog_inserted
ON surveillance.Xtbl_Article_Event
AFTER INSERT 
AS
	INSERT INTO surveillance.Xtbl_Article_EventTransLog
	SELECT SYSDATETIMEOFFSET(), 'Inserted', inserted.* FROM inserted
GO

CREATE TRIGGER surveillance.utr_Xtbl_Article_EventTransLog_updated
ON surveillance.Xtbl_Article_Event
AFTER UPDATE 
AS
	INSERT INTO surveillance.Xtbl_Article_EventTransLog
	SELECT SYSDATETIMEOFFSET(), 'Updated', inserted.* FROM inserted
GO

CREATE TRIGGER surveillance.utr_Xtbl_Article_EventTransLog_deleted
ON surveillance.Xtbl_Article_Event
AFTER DELETE 
AS
	INSERT INTO surveillance.Xtbl_Article_EventTransLog (ModifiedDate, Action, ArticleId, EventId)
	SELECT SYSDATETIMEOFFSET(), 'Deleted', deleted.ArticleId, deleted.EventId
  FROM deleted
GO
