CREATE TABLE [zebra].[DiseaseEventDestinationAirport] (
    [DiseaseId]              INT             NOT NULL,
    [DestinationStationId] INT             NOT NULL,
    [Volume]               INT          NULL,
	MinProb				DECIMAL(5, 4)	NULL, 
	MaxProb				DECIMAL(5, 4)	NULL,
	MinExpVolume		DECIMAL(10, 3)		NULL, 
	MaxExpVolume		DECIMAL(10, 3)		NULL
    CONSTRAINT [PK_DiseaseEventDestinationAirport] PRIMARY KEY CLUSTERED ([DiseaseId] ASC, [DestinationStationId] ASC),
    CONSTRAINT [FK_DiseaseEventDestinationAirport_EventId] FOREIGN KEY ([DiseaseId]) REFERENCES [disease].[Diseases] ([DiseaseId])
);
GO
CREATE NONCLUSTERED INDEX [idx_DestinationStationId]
    ON [zebra].[DiseaseEventDestinationAirport]([DestinationStationId] ASC);


