CREATE TABLE zebra.GridProvince (
    [GridId]   NVARCHAR (12)    NOT NULL,
	Adm1GeonameId int NOT NULL,
    CONSTRAINT [PK_GridProvince] PRIMARY KEY CLUSTERED ([GridId], Adm1GeonameId ASC)
);

GO

CREATE INDEX idx_Adm1GeonameId ON zebra.GridProvince(Adm1GeonameId ASC);

GO
