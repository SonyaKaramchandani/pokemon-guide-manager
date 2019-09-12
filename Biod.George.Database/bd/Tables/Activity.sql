CREATE TABLE [bd].[Activity] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [activity]    VARCHAR (64)  NOT NULL,
    [description] VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

