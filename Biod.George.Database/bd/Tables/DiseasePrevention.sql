CREATE TABLE [bd].[DiseasePrevention] (
    [id]            INT           IDENTITY (1, 1) NOT NULL,
    [diseaseId]     INT           NOT NULL,
    [type]          INT           NOT NULL,
    [riskReduction] FLOAT (53)    DEFAULT ((0.0)) NOT NULL,
    [availability]  VARCHAR (64)  NULL,
    [category]      INT           NOT NULL,
    [travel]        BIT           DEFAULT ((0)) NOT NULL,
    [oral]          BIT           DEFAULT ((0)) NOT NULL,
    [duration]      VARCHAR (64)  NULL,
    [notes]         VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([category]) REFERENCES [bd].[ModifierCategory] ([id]) ON UPDATE CASCADE,
    FOREIGN KEY ([diseaseId]) REFERENCES [bd].[Disease] ([diseaseId]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([type]) REFERENCES [bd].[PreventionType] ([id]) ON UPDATE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [diseasePrevention_diseaseId]
    ON [bd].[DiseasePrevention]([diseaseId] ASC);

