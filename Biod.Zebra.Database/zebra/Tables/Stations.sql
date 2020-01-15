CREATE TABLE [zebra].[Stations] (
  [StationId]       INT            NOT NULL,
  [StationCode]     CHAR (3)       NULL,
  [StationGridName] NVARCHAR (200) NULL,
  [StatioType]      CHAR (1)       NULL,
  [LastModified]    DATE           NULL,
  [CityId]          INT            NULL,
  [GeonameId]       INT            NULL,
  [ValidFromDate]   DATE           NULL,
  [ValidToDate]     DATE           NULL,
  [CityGeonameId]   INT            NULL,
  [Latitude]        DECIMAL(10,5)  NULL,
  [Longitude]       DECIMAL(10,5)  NULL,
  CONSTRAINT [PK_Stations] PRIMARY KEY CLUSTERED ([StationId] ASC),
  CONSTRAINT [FK_Stations_Geoname] FOREIGN KEY ([GeonameId]) REFERENCES [place].[Geonames] ([GeonameId]),
  CONSTRAINT [FK_Stations_CityGeoname] FOREIGN KEY ([CityGeonameId]) REFERENCES [place].[Geonames] ([GeonameId])
);
GO

CREATE NONCLUSTERED INDEX [idx_Stations_CityId]
    ON [zebra].[Stations]([CityId] ASC);
GO

CREATE NONCLUSTERED INDEX [idx_Stations_CityGeonameId]
    ON [zebra].[Stations]([CityGeonameId] ASC);

GO

CREATE NONCLUSTERED INDEX [idx_Stations_ValidToDate]
    ON [zebra].[Stations]([ValidToDate] ASC);
GO
