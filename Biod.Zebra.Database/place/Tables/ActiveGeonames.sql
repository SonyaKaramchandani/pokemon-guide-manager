CREATE TABLE [place].[ActiveGeonames] (
    [GeonameId]        INT              NOT NULL,
    [Name]             NVARCHAR (200)   NULL,
    [LocationType]     INT              NULL,
    [Admin1GeonameId]  INT              NULL,
    [CountryGeonameId] INT              NULL,
    [DisplayName]      VARCHAR(135)   NULL,
    [Alternatenames]   NVARCHAR (MAX)   NULL,
    [ModificationDate] DATE             NOT NULL,
    [FeatureCode]      VARCHAR (10)     NULL,
    [CountryName]      NVARCHAR (64)    NULL,
    [Latitude]         DECIMAL (10, 5)  NULL,
    [Longitude]        DECIMAL (10, 5)  NULL,
    [Population]       BIGINT           NULL,
    [SearchSeq2]       INT              NULL,
    [Shape]            [sys].[geography] NULL,
	LatPopWeighted     decimal(10,5)    NULL,
	LongPopWeighted     decimal(10,5)    NULL,
    CONSTRAINT [PK_ActiveGeonames] PRIMARY KEY CLUSTERED ([GeonameId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [idx_ActiveGeonames_Admin1GeonameId]
    ON [place].[ActiveGeonames]([Admin1GeonameId] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_ActiveGeonames_CountryGeonameId]
    ON [place].[ActiveGeonames]([CountryGeonameId] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_ActiveGeonames_DisplayName]
    ON [place].[ActiveGeonames]([DisplayName] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_ActiveGeonames_LocationType]
    ON [place].[ActiveGeonames]([LocationType] ASC);

GO

CREATE NONCLUSTERED INDEX [idx_ActiveGeonames_SearchSeq2]
    ON [place].[ActiveGeonames]([SearchSeq2] ASC);

GO
CREATE NONCLUSTERED INDEX [idx_ActiveGeonames_Name]
    ON [place].[ActiveGeonames]([Name] ASC);

GO
CREATE NONCLUSTERED INDEX [idx_ActiveGeonames_Population]
    ON [place].[ActiveGeonames]([Population] ASC);
