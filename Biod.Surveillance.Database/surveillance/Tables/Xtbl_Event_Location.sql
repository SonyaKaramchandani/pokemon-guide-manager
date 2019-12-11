CREATE TABLE [surveillance].[Xtbl_Event_Location] (
    [EventId]   INT  NOT NULL,
    [GeonameId] INT  NOT NULL,
    [EventDate] DATE NOT NULL,
    [SuspCases] INT  NULL,
    [ConfCases] INT  NULL,
    [RepCases]  INT  NULL,
    [Deaths]    INT  NULL,
    CONSTRAINT [PK_Xtbl_Event_Location] PRIMARY KEY CLUSTERED ([EventId] ASC, [GeonameId] ASC, [EventDate] ASC),
    CONSTRAINT [FK_Xtbl_Event_Location_Event] FOREIGN KEY ([EventId]) REFERENCES [surveillance].[Event] ([EventId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Xtbl_Event_Location_Geoname] FOREIGN KEY ([GeonameId]) REFERENCES [place].[Geonames] ([GeonameId]) ON DELETE CASCADE
);

GO

CREATE TRIGGER surveillance.utr_Xtbl_Event_LocationTransLog_inserted
ON surveillance.Xtbl_Event_Location
AFTER INSERT 
AS
	INSERT INTO surveillance.Xtbl_Event_LocationTransLog
	SELECT SYSDATETIMEOFFSET(), 'Inserted', inserted.* FROM inserted
GO

CREATE TRIGGER surveillance.utr_Xtbl_Event_LocationTransLog_updated
ON surveillance.Xtbl_Event_Location
AFTER UPDATE 
AS
	INSERT INTO surveillance.Xtbl_Event_LocationTransLog
	SELECT SYSDATETIMEOFFSET(), 'Updated', inserted.* FROM inserted
GO

CREATE TRIGGER surveillance.utr_Xtbl_Event_LocationTransLog_deleted
ON surveillance.Xtbl_Event_Location
AFTER DELETE 
AS
	INSERT INTO surveillance.Xtbl_Event_LocationTransLog (ModifiedDate, Action, EventId, GeonameId, EventDate)
	SELECT SYSDATETIMEOFFSET(), 'Deleted', deleted.EventId, deleted.GeonameId, deleted.EventDate
  FROM deleted
GO
