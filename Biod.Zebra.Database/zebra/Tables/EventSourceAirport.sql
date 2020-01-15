CREATE TABLE [zebra].[EventSourceAirport] (
    [EventId]         INT            NOT NULL,
    [SourceStationId] INT            NOT NULL,
    [StationName]     NVARCHAR (64)  NULL,
    [StationCode]     CHAR (3)       NULL,
    [CityDisplayName] NVARCHAR (200) NULL,
    [CountryName]     NVARCHAR (100) NULL,
    [NumCtryAirports] INT            NULL,
    [Volume]          INT            NULL,
    [CtryRank]        INT            NULL,
    [WorldRank]       INT            NULL,
	[Probability]     DECIMAL (10,6) NULL,
    CONSTRAINT [PK_EventSourceAirport] PRIMARY KEY CLUSTERED ([EventId] ASC, [SourceStationId] ASC),
    CONSTRAINT [FK_EventSourceAirport_EventId] FOREIGN KEY ([EventId]) REFERENCES [surveillance].[Event] ([EventId]),
    CONSTRAINT [FK_EventSourceAirport_StationId] FOREIGN KEY ([SourceStationId]) REFERENCES [zebra].[Stations] ([StationId]) ON DELETE CASCADE
);
GO

CREATE INDEX idx_EventSourceAirport_SourceStationId ON zebra.EventSourceAirport(SourceStationId ASC);
GO

