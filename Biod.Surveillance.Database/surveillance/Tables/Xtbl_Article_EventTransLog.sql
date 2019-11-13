CREATE TABLE [surveillance].[Xtbl_Article_EventTransLog] (
	[ModifiedDate]   DATETIMEOFFSET  NOT NULL,
	[Action]         NVARCHAR(128)   NOT NULL,
  [ArticleId]      NVARCHAR (128)   NOT NULL,
  [EventId]        INT             NOT NULL,
  CONSTRAINT [PK_surveillance.Xtbl_Article_EventTransLog] PRIMARY KEY CLUSTERED (
    [ArticleId],
    [EventId],
    [ModifiedDate]
  )
);
GO