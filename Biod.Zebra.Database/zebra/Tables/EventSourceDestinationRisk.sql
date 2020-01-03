CREATE TABLE [zebra].[EventSourceDestinationRisk] (
    [EventId]				INT  NOT NULL,
    [SourceAirportId]		INT  NOT NULL,
    [DestinationAirportId]	INT  NOT NULL,
    Volume					INT  NULL,
	MinProb					DECIMAL(5, 4)	NULL, 
	MaxProb					DECIMAL(5, 4)	NULL,
	MinExpVolume			DECIMAL(10, 3)	NULL, 
	MaxExpVolume			DECIMAL(10, 3)	NULL,
    CONSTRAINT [PK_EventSourceDestinationRisk] PRIMARY KEY CLUSTERED ([EventId] ASC, [SourceAirportId] ASC, [DestinationAirportId] ASC),
    CONSTRAINT [FK_EventSourceDestinationRisk_EventId] FOREIGN KEY ([EventId]) REFERENCES [surveillance].[Event] ([EventId]) ON DELETE CASCADE,
    CONSTRAINT [FK_EventSourceDestinationRisk_DestinationAirportId] FOREIGN KEY ([DestinationAirportId]) REFERENCES [zebra].[Stations] ([StationId]),
    CONSTRAINT [FK_EventSourceDestinationRisk_SourceAirportId] FOREIGN KEY ([SourceAirportId]) REFERENCES [zebra].[Stations] ([StationId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [idx_EventSourceDestinationRisk_SourceAirportId]
    ON [zebra].[EventSourceDestinationRisk]([SourceAirportId] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_EventSourceDestinationRisk_DestinationAirport]
    ON [zebra].[EventSourceDestinationRisk]([DestinationAirportId] ASC);

