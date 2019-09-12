CREATE TABLE [place].[GeoNameFeatureCodes] (
    [PlaceTypeId] INT           NOT NULL,
    [PlaceType]   VARCHAR (100) NOT NULL,
    [FeatureCode] VARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_GeoNameFeatureCodes] PRIMARY KEY CLUSTERED ([FeatureCode] ASC)
);

