CREATE TABLE zebra.EventImportationRisksByGeonameSpreadMd (
	EventId int NOT NULL,
	GeonameId int NOT NULL,
	LocalSpread int NOT NULL,
	MinProb decimal(5,4),
	MaxProb decimal(5,4),
	MinVolume decimal(10,3),
	MaxVolume decimal(10,3)
CONSTRAINT PK_EventImportationRisksByGeonameSpreadMd PRIMARY KEY CLUSTERED (EventId, GeonameId),
CONSTRAINT [FK_EventImportationRisksByGeonamerSpreadMd_GeonameId] FOREIGN KEY (GeonameId) REFERENCES place.Geonames(GeonameId) ON DELETE cascade,
CONSTRAINT [FK_EventImportationRisksByGeonameSpreadMd_EventId] FOREIGN KEY (EventId) REFERENCES [surveillance].[Event](EventId) ON DELETE cascade
);

GO

CREATE INDEX idx_EventImportationRisksByGeonameSpreadMd_GeonameId ON zebra.EventImportationRisksByGeonameSpreadMd(GeonameId ASC);
GO
