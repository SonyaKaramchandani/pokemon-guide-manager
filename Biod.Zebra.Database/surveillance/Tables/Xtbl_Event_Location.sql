CREATE TABLE [surveillance].[Xtbl_Event_Location] (
    [EventId]   INT  NOT NULL,
    [GeonameId] INT  NOT NULL,
    [EventDate] DATE NOT NULL,
    [SuspCases] INT  NULL,
    [ConfCases] INT  NULL,
    [RepCases]  INT  NULL,
    [Deaths]    INT  NULL,
    CONSTRAINT [PK_Xtbl_Event_Location] PRIMARY KEY CLUSTERED ([EventId] ASC, [GeonameId] ASC),
    CONSTRAINT [FK_Xtbl_Event_Location_Event] FOREIGN KEY ([EventId]) REFERENCES [surveillance].[Event] ([EventId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Xtbl_Event_Location_Geoname] FOREIGN KEY ([GeonameId]) REFERENCES [place].[ActiveGeonames] ([GeonameId]) ON DELETE CASCADE
);

