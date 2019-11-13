CREATE TABLE [surveillance].[Xtbl_Event_ReasonTransLog] (
	[ModifiedDate] DATETIMEOFFSET  NOT NULL,
	[Action]       NVARCHAR(128)   NOT NULL,
  [EventId]      INT             NOT NULL,
	[ReasonId]     INT             NOT NULL,
  CONSTRAINT [PK_surveillance.Xtbl_Event_ReasonTransLog] PRIMARY KEY CLUSTERED (
    [EventId],
    [ReasonId],
    [ModifiedDate]
  )
);
GO
