CREATE TABLE [surveillance].[Xtbl_Article_Location] (
    [ArticleId]         VARCHAR (128) NOT NULL,
    [LocationGeoNameId] INT           NOT NULL,
    CONSTRAINT [PK_Article_Location] PRIMARY KEY CLUSTERED ([ArticleId] ASC, [LocationGeoNameId] ASC),
    CONSTRAINT [FK_Xtbl_Article_Location_Location] FOREIGN KEY ([LocationGeoNameId]) REFERENCES [place].[Geonames] ([GeonameId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Xtbl_Article_Location_ProcessedArticle] FOREIGN KEY ([ArticleId]) REFERENCES [surveillance].[ProcessedArticle] ([ArticleId]) ON DELETE CASCADE
);
GO

CREATE TRIGGER surveillance.utr_Xtbl_Article_LocationTransLog_inserted
ON surveillance.Xtbl_Article_Location
AFTER INSERT 
AS
	INSERT INTO surveillance.Xtbl_Article_LocationTransLog
	SELECT SYSDATETIMEOFFSET(), 'Inserted', inserted.* FROM inserted
GO

CREATE TRIGGER surveillance.utr_Xtbl_Article_LocationTransLog_updated
ON surveillance.Xtbl_Article_Location
AFTER UPDATE 
AS
	INSERT INTO surveillance.Xtbl_Article_LocationTransLog
	SELECT SYSDATETIMEOFFSET(), 'Updated', inserted.* FROM inserted
GO

CREATE TRIGGER surveillance.utr_Xtbl_Article_LocationTransLog_deleted
ON surveillance.Xtbl_Article_Location
AFTER DELETE 
AS
	INSERT INTO surveillance.Xtbl_Article_LocationTransLog (ModifiedDate, Action, ArticleId, LocationGeoNameId)
	SELECT SYSDATETIMEOFFSET(), 'Deleted', deleted.ArticleId, deleted.LocationGeoNameId
  FROM deleted
GO
