CREATE TABLE [surveillance].[Xtbl_Article_Location_DiseaseTransLog] (
	[ModifiedDate]         DATETIMEOFFSET  NOT NULL,
	[Action]               NVARCHAR(128)   NOT NULL,
  [ArticleId]            NVARCHAR(128)   NOT NULL,
  [LocationGeoNameId]    INT             NOT NULL,
  [DiseaseId]            INT             NOT NULL,
	[TotalDeathCount]      INT             NULL,
	[TotalConfirmedCount]  INT             NULL,
	[TotalSuspectedCount]  INT             NULL,
	[TotalReportedCount]   INT             NULL,
	[NewDeathCount]        INT             NULL,
	[NewConfirmedCount]    INT             NULL,
	[NewSuspectedCount]    INT             NULL,
	[NewReportedCount]     INT             NULL,
  CONSTRAINT [PK_surveillance.Xtbl_Article_Location_DiseaseTransLog] PRIMARY KEY CLUSTERED (
    [ArticleId],
    [LocationGeoNameId],
    [DiseaseId],
    [ModifiedDate]
  )
);
GO
