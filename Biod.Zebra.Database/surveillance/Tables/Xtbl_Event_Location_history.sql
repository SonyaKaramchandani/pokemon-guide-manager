Create TABLE [surveillance].Xtbl_Event_Location_history
(EventId int NOT NULL,
EventDateType int NOT NULL, --1:last time, 2:last week
GeonameId int NOT NULL,
EventDate date NOT NULL,
SuspCases int,
ConfCases int,
RepCases int,
Deaths int,
CONSTRAINT PK_Xtbl_Event_Location_history PRIMARY KEY CLUSTERED (EventId, GeonameId, EventDateType),
CONSTRAINT FK_Xtbl_Event_Location_history_Event FOREIGN KEY (EventId)
	REFERENCES [surveillance].[Event](EventId) ON DELETE CASCADE,
CONSTRAINT FK_Xtbl_Event_Location_history_Geoname FOREIGN KEY (GeonameId)
	REFERENCES [place].[ActiveGeonames](GeonameId) ON DELETE CASCADE
);

