Create TABLE [surveillance].[SuggestedEvent](
	SuggestedEventId varchar(30) NOT NULL,
	EventTitle varchar(200) NOT NULL,
	SystemLastModifiedDate datetime NOT NULL,
	DiseaseId int NOT NULL,
	Highlights varchar(max),
	CONSTRAINT [PK_SuggestedEvent] PRIMARY KEY CLUSTERED (SuggestedEventId),
	CONSTRAINT [FK_SuggestedEvent_DiseaseId] FOREIGN KEY (DiseaseId) REFERENCES [disease].[Diseases] (DiseaseId)
);

