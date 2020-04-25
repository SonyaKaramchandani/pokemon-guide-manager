﻿CREATE TABLE [zebra].[EventDestinationAirportSpreadMd] (
    [EventId]              INT             NOT NULL,
    [DestinationStationId] INT             NOT NULL,
    [StationName]          NVARCHAR (64)   NULL,
    [StationCode]          CHAR (3)        NULL,
    [CityDisplayName]      NVARCHAR (200)  NULL,
    [Volume]               INT          NULL,
    [Latitude]             DECIMAL (10, 5) NULL,
    [Longitude]            DECIMAL (10, 5) NULL,
	MinProb				DECIMAL(5, 4)	NULL, 
	MaxProb				DECIMAL(5, 4)	NULL,
	MinExpVolume		DECIMAL(10, 3)	NULL, 
	MaxExpVolume		DECIMAL(10, 3)	NULL
    CONSTRAINT [PK_EventDestinationAirportSpreadMd] PRIMARY KEY CLUSTERED ([EventId] ASC, [DestinationStationId] ASC),
    CONSTRAINT [FK_EventDestinationAirportSpreadMd_EventId] FOREIGN KEY ([EventId]) REFERENCES [surveillance].[Event] ([EventId]),
    CONSTRAINT [FK_EventDestinationAirportSpreadMd_DestinationStationId] FOREIGN KEY (DestinationStationId) REFERENCES zebra.Stations(StationId)
);
GO
CREATE NONCLUSTERED INDEX [idx_DestinationStationId]
    ON [zebra].EventDestinationAirportSpreadMd([DestinationStationId] ASC);


