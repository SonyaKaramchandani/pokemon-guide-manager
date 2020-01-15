CREATE TABLE zebra.GridCountry (
  [GridId]   NVARCHAR (12)    NOT NULL,
  [CountryGeonameId] INT NOT NULL,
  CONSTRAINT [PK_GridCountry] PRIMARY KEY CLUSTERED ([GridId], CountryGeonameId ASC),
  CONSTRAINT [FK_GridCountry_Geoname] FOREIGN KEY ([CountryGeonameId]) REFERENCES [place].[Geonames]([GeonameId])
);
GO

CREATE INDEX idx_CountryGeonameId ON zebra.GridCountry(CountryGeonameId ASC);
GO
