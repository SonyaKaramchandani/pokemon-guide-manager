CREATE TABLE [bd].[TransmissionMode] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [mode]        VARCHAR (64)  NOT NULL,
    [multiplier]  FLOAT (53)    NOT NULL,
    [description] VARCHAR (MAX) NULL,
    [preventions] VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

