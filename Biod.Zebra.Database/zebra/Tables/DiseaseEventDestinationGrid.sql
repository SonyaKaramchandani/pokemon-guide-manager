CREATE TABLE [zebra].[DiseaseEventDestinationGrid] (
    [DiseaseId] INT           NOT NULL,
    [GridId]  NVARCHAR (12) NOT NULL,
    CONSTRAINT [PK_DiseaseEventDestinationGrid] PRIMARY KEY CLUSTERED ([DiseaseId] ASC, [GridId] ASC),
    CONSTRAINT [FK_DiseaseEventDestinationGrid_EventId] FOREIGN KEY ([DiseaseId]) REFERENCES [disease].[Diseases] ([DiseaseId]),
    CONSTRAINT [FK_DiseaseEventDestinationGrid_GridId] FOREIGN KEY ([GridId]) REFERENCES [bd].[HUFFMODEL25KMWORLDHEXAGON] ([gridId])
);


GO
CREATE NONCLUSTERED INDEX [idx_GridId]
    ON [zebra].[DiseaseEventDestinationGrid]([GridId] ASC);

