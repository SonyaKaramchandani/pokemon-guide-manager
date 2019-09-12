Create TABLE zebra.EventDestinationAirport_history(
	EventId int NOT NULL,
	DestinationStationId int NOT NULL, --1 means total volume, so can't have fkey
	Volume int,
	MinProb decimal(5, 4), 
	MaxProb decimal(5, 4),
	MinExpVolume decimal(10, 3), 
	MaxExpVolume decimal(10, 3)
CONSTRAINT PK_EventDestinationAirport_history PRIMARY KEY CLUSTERED (EventId, DestinationStationId),
CONSTRAINT [FK_EventDestinationAirport_history_EventId] FOREIGN KEY (EventId) REFERENCES [surveillance].[Event](EventId)
);
GO
CREATE NONCLUSTERED INDEX [idx_DestinationStationId]
    ON [zebra].EventDestinationAirport_history([DestinationStationId] ASC);


