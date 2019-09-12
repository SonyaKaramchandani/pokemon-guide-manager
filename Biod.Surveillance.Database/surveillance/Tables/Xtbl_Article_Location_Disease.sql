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
    CONSTRAINT [FK_Xtbl_Article_Location_Disease_Geonames] FOREIGN KEY ([LocationGeoNameId]) REFERENCES [place].[Geonames] ([GeonameId]) ON DELETE CASCADE,
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
