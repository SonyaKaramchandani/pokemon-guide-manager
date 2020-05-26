CREATE TABLE [surveillance].[EventLocation] (
    [EventId]   INT  NOT NULL,
    [GeonameId] INT  NOT NULL,
    [EventDate] DATE NOT NULL,
    [SuspCases] INT  NULL,
    [ConfCases] INT  NULL,
    [RepCases]  INT  NULL,
    [Deaths]    INT  NULL,
    CONSTRAINT [PK_EventLocation] PRIMARY KEY CLUSTERED ([EventId] ASC, [GeonameId] ASC, [EventDate] ASC),
    CONSTRAINT [FK_EventLocation_Event] FOREIGN KEY ([EventId]) REFERENCES [surveillance].[Event] ([EventId]) ON DELETE CASCADE,
    CONSTRAINT [FK_EventLocation_Geoname] FOREIGN KEY ([GeonameId]) REFERENCES [place].[Geonames] ([GeonameId]) ON DELETE CASCADE
);

GO

CREATE NONCLUSTERED INDEX [idx_EventLocation_GeonameId] ON [surveillance].[EventLocation]([GeonameId] ASC)
GO

CREATE NONCLUSTERED INDEX [idx_EventLocation_EventDate] ON [surveillance].[EventLocation]([EventDate] DESC)
GO
