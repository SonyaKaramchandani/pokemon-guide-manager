CREATE TABLE [disease].[AgentTypes] (
    [AgentTypeId] INT           NOT NULL,
    [AgentType]   VARCHAR (256) NOT NULL,
    CONSTRAINT [PK_AgentTypes] PRIMARY KEY CLUSTERED ([AgentTypeId] ASC)
);

