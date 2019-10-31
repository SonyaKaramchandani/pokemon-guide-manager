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

