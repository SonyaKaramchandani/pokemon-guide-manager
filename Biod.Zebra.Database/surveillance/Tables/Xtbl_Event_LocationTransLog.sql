CREATE TABLE [surveillance].[Xtbl_Event_LocationTransLog] (
  [LogId]        INT IDENTITY(1,1) NOT NULL,
  [ModifiedDate] DATETIMEOFFSET  NOT NULL,
  [Action]       NVARCHAR(128)   NOT NULL,
  [EventId]      INT             NOT NULL,
  [GeonameId]    INT             NOT NULL,
  [EventDate]    DATE            NULL,
  [SuspCases]    INT             NULL,
  [ConfCases]    INT             NULL,
  [RepCases]     INT             NULL,
  [Deaths]       INT             NULL,
  PRIMARY KEY CLUSTERED ([LogId])
);
GO
