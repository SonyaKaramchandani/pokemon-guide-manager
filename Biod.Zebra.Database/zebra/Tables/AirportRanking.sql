CREATE TABLE [zebra].[AirportRanking] (
    [StationId]        INT  NOT NULL,
    [WorldRank]        INT  NOT NULL,
    [NumWorldAirports] INT  NOT NULL,
    [CtryRank]         INT  NOT NULL,
    [NumCtryAirports]  INT  NOT NULL,
    [CtryGeonameId]    INT  NOT NULL,
    [StartDate]        DATE NOT NULL,
    [EndDate]          DATE NOT NULL,
    [ValidTo]          DATE NULL,
    [InboundVolume]    INT  NOT NULL,
    [OutboundVolume]   INT  NOT NULL,
    [LastModified]     DATE NOT NULL,
    CONSTRAINT [PK_AirportRanking] PRIMARY KEY CLUSTERED ([StationId] ASC, [StartDate] ASC, [EndDate] ASC),
    CONSTRAINT [FK_AirportRanking_CtryGeonameId] FOREIGN KEY ([CtryGeonameId]) REFERENCES [place].[Geonames] ([GeonameId]),
    CONSTRAINT [FK_AirportRanking_StationId] FOREIGN KEY ([StationId]) REFERENCES [zebra].[Stations] ([StationId])
);

