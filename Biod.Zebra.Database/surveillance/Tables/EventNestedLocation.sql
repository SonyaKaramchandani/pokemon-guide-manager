CREATE TABLE [surveillance].[EventNestedLocation] (
    [EventId]      INT  NOT NULL,
    [GeonameId]    INT  NOT NULL,
    [EventDate]    DATE NOT NULL,
    [SuspCases]    INT  NULL,
    [ConfCases]    INT  NULL,
    [RepCases]     INT  NULL,
    [Deaths]       INT  NULL,
    [NewSuspCases] INT  NULL,
    [NewConfCases] INT  NULL,
    [NewRepCases]  INT  NULL,
    [NewDeaths]    INT  NULL,
    CONSTRAINT [PK_EventNestedLocation] PRIMARY KEY CLUSTERED ([EventId] ASC, [GeonameId] ASC, [EventDate] ASC),
    CONSTRAINT [FK_EventNestedLocation_Event] FOREIGN KEY ([EventId]) REFERENCES [surveillance].[Event] ([EventId]) ON DELETE CASCADE,
    CONSTRAINT [FK_EventNestedLocation_Geoname] FOREIGN KEY ([GeonameId]) REFERENCES [place].[Geonames] ([GeonameId]) ON DELETE CASCADE
);

GO

CREATE NONCLUSTERED INDEX [idx_EventNestedLocation_GeonameId] ON [surveillance].[EventNestedLocation]([GeonameId] ASC)
GO

CREATE NONCLUSTERED INDEX [idx_EventNestedLocation_EventDate] ON [surveillance].[EventNestedLocation]([EventDate] DESC)
GO
