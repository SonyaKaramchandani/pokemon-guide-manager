CREATE TABLE zebra.GridCountry (
    [GridId]   NVARCHAR (12)    NOT NULL,
	CountryGeonameId int NOT NULL,
    CONSTRAINT [PK_GridCountry] PRIMARY KEY CLUSTERED ([GridId], CountryGeonameId ASC)
);

GO

CREATE INDEX idx_CountryGeonameId ON zebra.GridCountry(CountryGeonameId ASC);

GO
