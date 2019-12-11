CREATE TABLE [surveillance].[Xtbl_Article_EventTransLog] (
  [LogId]          INT IDENTITY(1,1) NOT NULL,
  [ModifiedDate]   DATETIMEOFFSET    NOT NULL,
  [Action]         NVARCHAR(128)     NOT NULL,
  [ArticleId]      NVARCHAR(128)     NOT NULL,
  [EventId]        INT               NOT NULL,
  PRIMARY KEY CLUSTERED ([LogId])
);
GO
