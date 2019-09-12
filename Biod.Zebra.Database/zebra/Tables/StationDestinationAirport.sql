CREATE TABLE [zebra].[StationDestinationAirport] (
    [StationId]            INT  NOT NULL,
    [DestinationAirportId] INT  NOT NULL,
    [ValidFromDate]        DATE NOT NULL,
    [Volume]               INT  NULL,
    CONSTRAINT [PK_StationDestinationAirport] PRIMARY KEY CLUSTERED ([StationId] ASC, [DestinationAirportId] ASC, [ValidFromDate] ASC),
    CONSTRAINT [FK_StationDestinationAirport_DestinationAirportId] FOREIGN KEY ([DestinationAirportId]) REFERENCES [zebra].[Stations] ([StationId]),
    CONSTRAINT [FK_StationDestinationAirport_StationId] FOREIGN KEY ([StationId]) REFERENCES [zebra].[Stations] ([StationId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [idx_StationDestinationAirport_ValidFromDate]
    ON [zebra].[StationDestinationAirport]([ValidFromDate] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_StationDestinationAirport_DestinationAirport]
    ON [zebra].[StationDestinationAirport]([DestinationAirportId] ASC);

