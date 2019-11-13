CREATE TABLE [surveillance].[Xtbl_Event_Reason] (
  [EventId]  INT NOT NULL,
  [ReasonId] INT NOT NULL,
  CONSTRAINT [PK_Xtbl_Event_Reason] PRIMARY KEY CLUSTERED ([EventId] ASC, [ReasonId] ASC),
  CONSTRAINT [FK_Xtbl_Event_Reason_Event] FOREIGN KEY ([EventId]) REFERENCES [surveillance].[Event] ([EventId]) ON DELETE CASCADE,
  CONSTRAINT [FK_Xtbl_Event_Reason_Reason] FOREIGN KEY ([ReasonId]) REFERENCES [surveillance].[EventCreationReasons] ([ReasonId]) ON DELETE CASCADE
);

GO

CREATE TRIGGER surveillance.utr_Xtbl_Event_ReasonTransLog_inserted
ON surveillance.Xtbl_Event_Reason
AFTER INSERT 
AS
	INSERT INTO surveillance.Xtbl_Event_ReasonTransLog
	SELECT SYSDATETIMEOFFSET(), 'Inserted', inserted.* FROM inserted
GO

CREATE TRIGGER surveillance.utr_Xtbl_Event_ReasonTransLog_updated
ON surveillance.Xtbl_Event_Reason
AFTER UPDATE 
AS
	INSERT INTO surveillance.Xtbl_Event_ReasonTransLog
	SELECT SYSDATETIMEOFFSET(), 'Updated', inserted.* FROM inserted
GO

CREATE TRIGGER surveillance.utr_Xtbl_Event_ReasonTransLog_deleted
ON surveillance.Xtbl_Event_Reason
AFTER DELETE 
AS
	INSERT INTO surveillance.Xtbl_Event_ReasonTransLog (ModifiedDate, Action, EventId, ReasonId)
	SELECT SYSDATETIMEOFFSET(), 'Deleted', deleted.EventId, deleted.ReasonId
  FROM deleted
GO
