CREATE TABLE [disease].[Agents] (
    [AgentId]     INT           NOT NULL,
    [Agent]       VARCHAR (2000) NOT NULL,
    [AgentTypeId] INT           NOT NULL,
    CONSTRAINT [PK_Agents] PRIMARY KEY CLUSTERED ([AgentId] ASC),
    CONSTRAINT [FK_Agents_AgentTypeId] FOREIGN KEY ([AgentTypeId]) REFERENCES [disease].[AgentTypes] ([AgentTypeId]) ON DELETE CASCADE
);

