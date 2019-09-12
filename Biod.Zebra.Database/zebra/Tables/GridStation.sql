CREATE TABLE [zebra].[GridStation] (
    [GridId]        NVARCHAR (12)   NOT NULL,
    [StationId]     INT             NOT NULL,
    [Probability]   DECIMAL (10, 8) NULL,
    [ValidFromDate] DATE            NOT NULL,
    [LastModified]  DATETIME        NULL,
    CONSTRAINT [PK_GridStation] PRIMARY KEY CLUSTERED ([GridId] ASC, [StationId] ASC, [ValidFromDate] ASC),
    CONSTRAINT [FK_GridStation_GridId] FOREIGN KEY ([GridId]) REFERENCES [bd].[HUFFMODEL25KMWORLDHEXAGON] ([gridId]),
    CONSTRAINT [FK_GridStation_StationId] FOREIGN KEY ([StationId]) REFERENCES [zebra].[Stations] ([StationId])
);


GO
CREATE NONCLUSTERED INDEX [idx_GridStation_ValidFromDate]
    ON [zebra].[GridStation]([ValidFromDate] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_GridStation_StationId]
    ON [zebra].[GridStation]([StationId] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_GridStation_Probability]
    ON [zebra].[GridStation]([Probability] ASC);

