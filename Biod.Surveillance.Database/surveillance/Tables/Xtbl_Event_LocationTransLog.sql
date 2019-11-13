CREATE TABLE [surveillance].[Xtbl_Event_LocationTransLog] (
	[ModifiedDate] DATETIMEOFFSET  NOT NULL,
	[Action]       NVARCHAR(128)   NOT NULL,
  [EventId]      INT             NOT NULL,
	[GeonameId]    INT             NOT NULL,
	[EventDate]    DATE            NOT NULL,
	[SuspCases]    INT             NULL,
	[ConfCases]    INT             NULL,
	[RepCases]     INT             NULL,
	[Deaths]       INT             NULL,
  CONSTRAINT [PK_surveillance.Xtbl_Event_LocationTransLog] PRIMARY KEY CLUSTERED (
    [EventId],
    [GeonameId],
    [EventDate],
    [ModifiedDate]
  )
);
GO
