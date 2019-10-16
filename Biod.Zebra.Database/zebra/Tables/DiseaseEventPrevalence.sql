CREATE TABLE zebra.DiseaseEventPrevalence (
	DiseaseId int NOT NULL,
	MinPrevelance float NULL,
	MaxPrevelance float NULL,
	EventMonth int NOT NULL
CONSTRAINT PK_DiseaseEventPrevalence PRIMARY KEY CLUSTERED (DiseaseId),
CONSTRAINT [FK_DiseaseEventPrevalence_EventId] FOREIGN KEY (DiseaseId) REFERENCES [disease].[Diseases](DiseaseId)
);

