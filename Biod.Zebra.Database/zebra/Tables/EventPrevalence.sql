CREATE TABLE zebra.EventPrevalence (
	EventId int NOT NULL,
	MinPrevelance float NOT NULL,
	MaxPrevelance float NOT NULL,
	EventMonth int NOT NULL
CONSTRAINT PK_EventPrevalence PRIMARY KEY CLUSTERED (EventId),
CONSTRAINT [FK_EventPrevalence_EventId] FOREIGN KEY (EventId) REFERENCES [surveillance].[Event](EventId)
);

GO
