CREATE TABLE [zebra].[EventSourceGridSpreadMd] (
    [EventId] INT           NOT NULL,
    [GridId]  NVARCHAR (12) NOT NULL,
    [Cases]   INT           NOT NULL,
    [MinCases]   INT        NOT NULL,
    [MaxCases]   INT        NOT NULL,
    CONSTRAINT [PK_EventSourceGridSpreadMd] PRIMARY KEY CLUSTERED ([EventId] ASC, [GridId] ASC),
    CONSTRAINT [FK_EventSourceGridSpreadMd_EventId] FOREIGN KEY ([EventId]) REFERENCES [surveillance].[Event] ([EventId]),
    CONSTRAINT [FK_EventSourceGridSpreadMd_GridId] FOREIGN KEY ([GridId]) REFERENCES [bd].[HUFFMODEL25KMWORLDHEXAGON] ([gridId])
);


GO
CREATE NONCLUSTERED INDEX [idx_GridId]
    ON [zebra].[EventSourceGridSpreadMd]([GridId] ASC);

