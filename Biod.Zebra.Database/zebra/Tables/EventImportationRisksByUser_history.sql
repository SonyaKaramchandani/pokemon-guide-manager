CREATE TABLE zebra.EventImportationRisksByUser_history (
	UserId nvarchar(128) NOT NULL,
	LocalSpread int NOT NULL,
	EventId int NOT NULL,
	MinProb decimal(5,4),
	MaxProb decimal(5,4),
	MinVolume decimal(10,3),
	MaxVolume decimal(10,3)
CONSTRAINT PK_EventImportationRisksByUser_history PRIMARY KEY CLUSTERED (UserId, EventId),
CONSTRAINT [FK_EventImportationRisksByUser_history_UserId] FOREIGN KEY (UserId) REFERENCES [dbo].[AspNetUsers](Id) ON DELETE cascade,
CONSTRAINT [FK_EventImportationRisksByUser_history_EventId] FOREIGN KEY (EventId) REFERENCES [surveillance].[Event](EventId) ON DELETE cascade
);

GO
