CREATE TABLE [place].[Geonames] (
    [GeonameId]        INT            NOT NULL,
    [Name]             NVARCHAR (200) NULL,
    [LocationType]     INT            NULL,
    [Admin1GeonameId]  INT            NULL,
    [CountryGeonameId] INT            NULL,
    [DisplayName]      NVARCHAR (500) NULL,
    [Alternatenames]   NVARCHAR (MAX) NULL,
    [ModificationDate] DATE           NOT NULL,
    [FeatureCode]      VARCHAR (10)   NULL,
    [CountryName]      NVARCHAR (64)  NULL,
	Latitude		   DECIMAL(10,5),
	Longitude		   DECIMAL(10,5),
	[Population]	   BIGINT,
    CONSTRAINT [PK_Geonames] PRIMARY KEY CLUSTERED ([GeonameId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [idx_Geonames_Admin1GeonameId]
    ON [place].[Geonames]([Admin1GeonameId] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_Geonames_CountryGeonameId]
    ON [place].[Geonames]([CountryGeonameId] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_Geonames_DisplayName]
    ON [place].[Geonames]([DisplayName] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_Geonames_LocationType]
    ON [place].[Geonames]([LocationType] ASC);

