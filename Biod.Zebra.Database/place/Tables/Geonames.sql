CREATE TABLE [place].[Geonames] (
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

GO

CREATE NONCLUSTERED INDEX [idx_Geonames_SearchSeq2]
    ON [place].[Geonames]([SearchSeq2] ASC);

GO
CREATE NONCLUSTERED INDEX [idx_Geonames_Name]
    ON [place].[Geonames]([Name] ASC);

GO
CREATE NONCLUSTERED INDEX [idx_Geonames_Population]
    ON [place].[Geonames]([Population] ASC);
