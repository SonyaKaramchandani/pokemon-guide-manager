CREATE TABLE [surveillance].[Xtbl_Article_LocationTransLog] (
	[ModifiedDate]       DATETIMEOFFSET  NOT NULL,
	[Action]             NVARCHAR(128)   NOT NULL,
  [ArticleId]          NVARCHAR (128)  NOT NULL,
  LocationGeoNameId    INT             NOT NULL,
  CONSTRAINT [PK_surveillance.Xtbl_Article_LocationTransLog] PRIMARY KEY CLUSTERED (
    [ArticleId],
    [LocationGeoNameId],
    [ModifiedDate]
  )
);
GO
