CREATE TABLE [surveillance].[Event] (
  [EventId]                     INT IDENTITY (1, 1) NOT NULL,
  [EventTitle]                  VARCHAR (200)       NOT NULL,
  [StartDate]                   DATE                NULL,
  [EndDate]                     DATE                NULL,
  [LastUpdatedDate]             DATETIME            NULL,
  [PriorityId]                  INT                 NULL,
  [IsPublished]                 BIT                 NULL,
  [IsPublishedChangesToApi]     BIT                 NULL,
  [Summary]                     VARCHAR (MAX)       NULL,
  [Notes]                       VARCHAR (MAX)       NULL,
  [DiseaseId]                   INT                 NULL,
  [CreatedDate]                 DATETIME            NULL,
  EventMongoId                  VARCHAR(128)        NULL,
  LastUpdatedByUserName         VARCHAR(64)         NULL,
  IsLocalOnly                   BIT DEFAULT (0)     NOT NULL,
  SpeciesId                     INT DEFAULT (1)     NOT NULL,
  PRIMARY KEY CLUSTERED ([EventId] ASC),
  CONSTRAINT FK_Event_PriorityId FOREIGN KEY (PriorityId) REFERENCES [surveillance].[EventPriorities](PriorityId), 
  CONSTRAINT FK_Event_SpeciesId FOREIGN KEY (SpeciesId) REFERENCES disease.Species(SpeciesId)
);
GO

CREATE TRIGGER surveillance.utr_EventTransLog_inserted
ON surveillance.Event
AFTER INSERT 
AS
  INSERT INTO surveillance.EventTransLog
  SELECT SYSDATETIMEOFFSET(), 'Inserted', inserted.* FROM inserted
GO

CREATE TRIGGER surveillance.utr_EventTransLog_updated
ON surveillance.Event
AFTER UPDATE 
AS
  INSERT INTO surveillance.EventTransLog
  SELECT SYSDATETIMEOFFSET(), 'Updated', inserted.* FROM inserted
GO

CREATE TRIGGER surveillance.utr_EventTransLog_deleted
ON surveillance.Event
AFTER DELETE 
AS
  INSERT INTO surveillance.EventTransLog (ModifiedDate, Action, EventId)
  SELECT SYSDATETIMEOFFSET(), 'Deleted', deleted.EventId
  FROM deleted
GO
