CREATE TABLE [surveillance].[EventPriorities] (
    [PriorityId]    INT           NOT NULL,
    [PriorityTitle] VARCHAR (100) NULL,
    CONSTRAINT [PK_EventPriorities] PRIMARY KEY CLUSTERED ([PriorityId] ASC)
);

