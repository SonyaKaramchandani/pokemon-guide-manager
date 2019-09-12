CREATE TABLE [bd].[Condition] (
    [conditionId] INT           NOT NULL,
    [category]    INT           NOT NULL,
    [condition]   VARCHAR (64)  NOT NULL,
    [question]    VARCHAR (256) NOT NULL,
    [description] VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([conditionId] ASC),
    FOREIGN KEY ([category]) REFERENCES [bd].[ModifierCategory] ([id]) ON UPDATE CASCADE
);

