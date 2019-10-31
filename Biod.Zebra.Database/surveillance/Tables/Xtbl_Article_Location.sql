CREATE TABLE [surveillance].[Xtbl_Article_Location] (
    [ArticleId]         VARCHAR (128) NOT NULL,
    [LocationGeoNameId] INT           NOT NULL,
    CONSTRAINT [PK_Article_Location] PRIMARY KEY CLUSTERED ([ArticleId] ASC, [LocationGeoNameId] ASC),
    --CONSTRAINT [FK_Xtbl_Article_Location_Location] FOREIGN KEY ([LocationGeoNameId]) REFERENCES [place].[ActiveGeonames] ([GeonameId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Xtbl_Article_Location_ProcessedArticle] FOREIGN KEY ([ArticleId]) REFERENCES [surveillance].[ProcessedArticle] ([ArticleId]) ON DELETE CASCADE
);

