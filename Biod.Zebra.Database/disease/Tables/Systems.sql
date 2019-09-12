﻿CREATE TABLE [disease].[Systems] (
    [SystemId]     INT           NOT NULL,
    [System]       VARCHAR (100) NOT NULL,
    [Notes]        VARCHAR (500) NULL,
    [LastModified] DATETIME      NULL,
    CONSTRAINT [PK_Systems] PRIMARY KEY CLUSTERED ([SystemId] ASC)
);

