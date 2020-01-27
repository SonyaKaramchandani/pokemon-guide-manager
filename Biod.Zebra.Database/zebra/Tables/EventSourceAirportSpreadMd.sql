CREATE TABLE [zebra].[EventSourceAirportSpreadMd] (
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
	MinCaseOverPop	  FLOAT			 NULL,
	MaxCaseOverPop	  FLOAT			 NULL,
	MinPrevelance	  FLOAT			 NULL,
	MaxPrevelance	  FLOAT			 NULL,
	MinProb				DECIMAL(5, 4)	NULL, 
	MaxProb				DECIMAL(5, 4)	NULL,
	MinExpVolume		DECIMAL(10, 3)	NULL, 
	MaxExpVolume		DECIMAL(10, 3)	NULL
    CONSTRAINT [PK_EventSourceAirportSpreadMd] PRIMARY KEY CLUSTERED ([EventId] ASC, [SourceStationId] ASC),
    CONSTRAINT [FK_EventSourceAirportSpreadMd_EventId] FOREIGN KEY ([EventId]) REFERENCES [surveillance].[Event] ([EventId]) ON DELETE CASCADE,
    CONSTRAINT [FK_EventSourceAirportSpreadMd_StationId] FOREIGN KEY ([SourceStationId]) REFERENCES [zebra].[Stations] ([StationId]) ON DELETE CASCADE
);

