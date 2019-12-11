CREATE TABLE [surveillance].[Xtbl_Event_ReasonTransLog] (
  [LogId]        INT IDENTITY(1,1) NOT NULL,
  [ModifiedDate] DATETIMEOFFSET  NOT NULL,
  [Action]       NVARCHAR(128)   NOT NULL,
  [EventId]      INT             NOT NULL,
  [ReasonId]     INT             NOT NULL,
  PRIMARY KEY CLUSTERED ([LogId])
);
GO
