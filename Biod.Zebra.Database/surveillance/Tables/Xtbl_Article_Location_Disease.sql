CREATE TABLE [surveillance].[Xtbl_Article_Location_Disease] (
    [ArticleId]           VARCHAR (128) NOT NULL,
    [LocationGeoNameId]   INT           NOT NULL,
    [DiseaseId]           INT           NOT NULL,
    [TotalDeathCount]     INT           NULL,
    [TotalConfirmedCount] INT           NULL,
    [TotalSuspectedCount] INT           NULL,
    [TotalReportedCount]  INT           NULL,
    [NewDeathCount]       INT           NULL,
    [NewConfirmedCount]   INT           NULL,
    [NewSuspectedCount]   INT           NULL,
    [NewReportedCount]    INT           NULL,
    CONSTRAINT [PK_Article_Location_Disease] PRIMARY KEY CLUSTERED ([ArticleId] ASC, [LocationGeoNameId] ASC, [DiseaseId] ASC),
    CONSTRAINT [FK_Xtbl_Article_Location_Disease_Diseases] FOREIGN KEY ([DiseaseId]) REFERENCES [disease].[Diseases] ([DiseaseId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Xtbl_Article_Location_Disease_Geonames] FOREIGN KEY ([LocationGeoNameId]) REFERENCES [place].[ActiveGeonames] ([GeonameId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Xtbl_Article_Location_Disease_ProcessedArticle] FOREIGN KEY ([ArticleId]) REFERENCES [surveillance].[ProcessedArticle] ([ArticleId]) ON DELETE CASCADE
);
GO

--DiseaseId
CREATE NONCLUSTERED INDEX [idx_Xtbl_Article_Location_Disease_DiseaseId]
  ON [surveillance].Xtbl_Article_Location_Disease(DiseaseId ASC);
GO

--LocationGeoNameId
CREATE NONCLUSTERED INDEX [idx_Xtbl_Article_Location_Disease_LocationGeoNameId]
  ON [surveillance].Xtbl_Article_Location_Disease(LocationGeoNameId ASC);
GO

CREATE TRIGGER surveillance.utr_Xtbl_Article_Location_DiseaseTransLog_inserted
ON surveillance.Xtbl_Article_Location_Disease
AFTER INSERT 
AS
	INSERT INTO surveillance.Xtbl_Article_Location_DiseaseTransLog
	SELECT SYSDATETIMEOFFSET(), 'Inserted', inserted.* FROM inserted
GO

CREATE TRIGGER surveillance.utr_Xtbl_Article_Location_DiseaseTransLog_updated
ON surveillance.Xtbl_Article_Location_Disease
AFTER UPDATE 
AS
	INSERT INTO surveillance.Xtbl_Article_Location_DiseaseTransLog
	SELECT SYSDATETIMEOFFSET(), 'Updated', inserted.* FROM inserted
GO

CREATE TRIGGER surveillance.utr_Xtbl_Article_Location_DiseaseTransLog_deleted
ON surveillance.Xtbl_Article_Location_Disease
AFTER DELETE 
AS
	INSERT INTO surveillance.Xtbl_Article_Location_DiseaseTransLog (ModifiedDate, Action, ArticleId, LocationGeoNameId, DiseaseId)
	SELECT SYSDATETIMEOFFSET(), 'Deleted', deleted.ArticleId, deleted.LocationGeoNameId, deleted.DiseaseId
  FROM deleted
GO
