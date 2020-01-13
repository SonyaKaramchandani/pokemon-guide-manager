Create TABLE [surveillance].[Xtbl_SuggestedEvent_Location](
	SuggestedEventId varchar(30) NOT NULL,
	GeonameId int NOT NULL
	CONSTRAINT [PK_Xtbl_SuggestedEvent_Location] PRIMARY KEY CLUSTERED (SuggestedEventId, GeonameId),
	CONSTRAINT [FK_Xtbl_SuggestedEvent_Location_GeonameId] FOREIGN KEY (GeonameId) 
		REFERENCES [place].[Geonames] ([GeonameId]),
	CONSTRAINT FK_Xtbl_SuggestedEvent_Location_SuggestedEventId FOREIGN KEY (SuggestedEventId) 
		REFERENCES [surveillance].[SuggestedEvent] (SuggestedEventId) ON DELETE CASCADE
);
GO

CREATE NONCLUSTERED INDEX [idx_Xtbl_SuggestedEvent_Location_GeonameId] ON [surveillance].Xtbl_SuggestedEvent_Location(GeonameId ASC)
GO

