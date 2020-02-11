CREATE TABLE [zebra].EventExtensionSpreadMd
(
	[EventId]                               INT NOT NULL,
	[AirportsDestinationVolume]             INT NULL,
	[MinExportationProbabilityViaAirports]  DECIMAL(5, 4)	NULL,
	[MaxExportationProbabilityViaAirports]  DECIMAL(5, 4)	NULL,
	[MinExportationVolumeViaAirports]       DECIMAL(10, 3) NULL, 
	[MaxExportationVolumeViaAirports]       DECIMAL(10, 3) NULL
  CONSTRAINT PK_EventExtensionSpreadMd PRIMARY KEY CLUSTERED (EventId),
  CONSTRAINT [FK_EventExtensionSpreadMd_EventId] FOREIGN KEY (EventId) REFERENCES [surveillance].[Event](EventId)
);
GO
