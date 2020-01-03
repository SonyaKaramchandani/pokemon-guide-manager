CREATE TABLE [zebra].[EventDestinationGridSpreadMd] (
    [EventId] INT           NOT NULL,
    [GridId]  NVARCHAR (12) NOT NULL,
    CONSTRAINT [PK_EventDestinationGridSpreadMd] PRIMARY KEY CLUSTERED ([EventId] ASC, [GridId] ASC),
    CONSTRAINT [FK_EventDestinationGridSpreadMd_EventId] FOREIGN KEY ([EventId]) REFERENCES [surveillance].[Event] ([EventId]),
    CONSTRAINT [FK_EventDestinationGridSpreadMd_GridId] FOREIGN KEY ([GridId]) REFERENCES [bd].[HUFFMODEL25KMWORLDHEXAGON] ([gridId])
);


GO
CREATE NONCLUSTERED INDEX [idx_GridId]
    ON [zebra].[EventDestinationGridSpreadMd]([GridId] ASC);

