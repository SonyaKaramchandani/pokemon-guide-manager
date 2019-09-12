CREATE TABLE [zebra].[EventDestinationGridV3] (
    [EventId] INT           NOT NULL,
    [GridId]  NVARCHAR (12) NOT NULL,
    CONSTRAINT [PK_EventDestinationGridV3] PRIMARY KEY CLUSTERED ([EventId] ASC, [GridId] ASC),
    CONSTRAINT [FK_EventDestinationGridV3_EventId] FOREIGN KEY ([EventId]) REFERENCES [surveillance].[Event] ([EventId]),
    CONSTRAINT [FK_EventDestinationGridV3_GridId] FOREIGN KEY ([GridId]) REFERENCES [bd].[HUFFMODEL25KMWORLDHEXAGON] ([gridId])
);


GO
CREATE NONCLUSTERED INDEX [idx_GridId]
    ON [zebra].[EventDestinationGridV3]([GridId] ASC);

