CREATE TABLE [zebra].[DiseaseSourceAirport] (
    [DiseaseId]         INT            NOT NULL,
    [SourceStationId] INT            NOT NULL,
	Probability       DECIMAL (10,6)
    CONSTRAINT [PK_DiseaseSourceAirport] PRIMARY KEY CLUSTERED ([DiseaseId] ASC, [SourceStationId] ASC)
    ,CONSTRAINT [FK_DiseaseSourceAirport_DiseaseId] FOREIGN KEY ([DiseaseId]) REFERENCES [disease].[Diseases] ([DiseaseId]) ON DELETE CASCADE
    ,CONSTRAINT [FK_DiseaseSourceAirportt_StationId] FOREIGN KEY ([SourceStationId]) REFERENCES [zebra].[Stations] ([StationId]) ON DELETE CASCADE
);

