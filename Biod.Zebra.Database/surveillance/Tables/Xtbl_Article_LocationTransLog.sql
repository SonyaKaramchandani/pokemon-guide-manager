CREATE TABLE [surveillance].[Xtbl_Article_LocationTransLog] (
  [LogId]              INT IDENTITY(1,1) NOT NULL,
  [ModifiedDate]       DATETIMEOFFSET  NOT NULL,
  [Action]             NVARCHAR(128)   NOT NULL,
  [ArticleId]          NVARCHAR (128)  NOT NULL,
  LocationGeoNameId    INT             NOT NULL,
  PRIMARY KEY CLUSTERED ([LogId])
);
GO
