CREATE TABLE zebra.GridProvince (
  [GridId]   NVARCHAR (12)    NOT NULL,
  [Adm1GeonameId] INT NOT NULL,
  CONSTRAINT [PK_GridProvince] PRIMARY KEY CLUSTERED ([GridId], Adm1GeonameId ASC),
  CONSTRAINT [FK_GridProvince_Geoname] FOREIGN KEY ([Adm1GeonameId]) REFERENCES [place].[Geonames]([GeonameId])
);
GO

CREATE INDEX idx_Adm1GeonameId ON zebra.GridProvince(Adm1GeonameId ASC);
GO
