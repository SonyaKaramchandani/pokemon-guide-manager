CREATE TABLE zebra.EventPrevalence_history (
	EventId int NOT NULL,
	MinPrevelance float NOT NULL,
	MaxPrevelance float NOT NULL,
	EventMonth int NOT NULL
CONSTRAINT PK_EventPrevalence_history PRIMARY KEY CLUSTERED (EventId),
CONSTRAINT [FK_EventPrevalence_history_EventId] FOREIGN KEY (EventId) REFERENCES [surveillance].[Event](EventId)
);

GO
