CREATE TABLE zebra.EventImportationRisksByGeoname (
	GeonameId int NOT NULL,
	LocalSpread int NOT NULL,
	EventId int NOT NULL,
	MinProb decimal(5,4),
	MaxProb decimal(5,4),
	MinVolume decimal(10,3),
	MaxVolume decimal(10,3)
CONSTRAINT PK_EventImportationRisksByGeoname PRIMARY KEY CLUSTERED (GeonameId, EventId),
CONSTRAINT [FK_EventImportationRisksByGeonamer_GeonameId] FOREIGN KEY (GeonameId) REFERENCES place.Geonames(GeonameId) ON DELETE cascade,
CONSTRAINT [FK_EventImportationRisksByGeoname_EventId] FOREIGN KEY (EventId) REFERENCES [surveillance].[Event](EventId) ON DELETE cascade
);

GO

CREATE INDEX idx_EventImportationRisksByGeoname ON zebra.EventImportationRisksByGeoname(EventId ASC);
GO
