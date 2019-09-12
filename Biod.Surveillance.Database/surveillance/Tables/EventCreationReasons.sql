CREATE TABLE [surveillance].[EventCreationReasons] (
    [ReasonId]   INT           NOT NULL,
    [ReasonName] VARCHAR (100) NULL,
    CONSTRAINT [PK_EventCreationReasons] PRIMARY KEY CLUSTERED ([ReasonId] ASC)
);

