CREATE TABLE zebra.EventImportationRisksByUser (
	UserId nvarchar(128) NOT NULL,
	LocalSpread int NOT NULL,
	EventId int NOT NULL,
	MinProb decimal(5,4),
	MaxProb decimal(5,4),
	MinVolume decimal(10,3),
	MaxVolume decimal(10,3)
CONSTRAINT PK_EventImportationRisksByUser PRIMARY KEY CLUSTERED (UserId, EventId),
CONSTRAINT [FK_EventImportationRisksByUser_UserId] FOREIGN KEY (UserId) REFERENCES [dbo].[UserProfile](Id) ON DELETE cascade,
CONSTRAINT [FK_EventImportationRisksByUser_EventId] FOREIGN KEY (EventId) REFERENCES [surveillance].[Event](EventId) ON DELETE cascade
);

GO

CREATE INDEX idx_EventImportationRisksByUser_EventId ON zebra.EventImportationRisksByUser(EventId ASC);
GO
