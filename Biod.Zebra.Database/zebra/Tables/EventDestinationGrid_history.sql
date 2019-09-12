Create TABLE zebra.EventDestinationGrid_history(
	EventId INT NOT NULL,
	GridId nvarchar(12) NOT NULL, --highly connected to event by air traffic
CONSTRAINT PK_EventDestinationGrid_history PRIMARY KEY CLUSTERED (EventId, GridId),
CONSTRAINT [FK_EventDestinationGrid_history_EventId] FOREIGN KEY (EventId) REFERENCES [surveillance].[Event](EventId),
CONSTRAINT [FK_EventDestinationGrid_history_GridId] FOREIGN KEY (GridId) REFERENCES [bd].[HUFFMODEL25KMWORLDHEXAGON](gridId)
);

GO
CREATE NONCLUSTERED INDEX [idx_GridId]
    ON [zebra].EventDestinationGrid_history([GridId] ASC);


