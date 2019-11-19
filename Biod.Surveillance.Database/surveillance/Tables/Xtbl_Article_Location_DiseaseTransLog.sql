CREATE TABLE [surveillance].[Xtbl_Article_Location_DiseaseTransLog] (
  [LogId]                INT IDENTITY(1,1) NOT NULL,
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
  PRIMARY KEY CLUSTERED ([LogId])
);
GO
