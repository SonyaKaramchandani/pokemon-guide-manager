CREATE TABLE [disease].[Xtbl_Disease_Agents] (
    [DiseaseId]  INT NOT NULL,
    [AgentId] INT NOT NULL,
    CONSTRAINT [PK_Xtbl_Disease_Agents] PRIMARY KEY CLUSTERED ([DiseaseId] ASC, [AgentId] ASC),
    CONSTRAINT [FK_Xtbl_Disease_Agents_DiseaseId] FOREIGN KEY ([DiseaseId]) REFERENCES [disease].[Diseases] ([DiseaseId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Xtbl_Disease_Agents_AgentId] FOREIGN KEY ([AgentId]) REFERENCES [disease].[Agents] ([AgentId]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [idx_Xtbl_Disease_Agents_AgentId] ON [disease].[Xtbl_Disease_Agents](AgentId ASC);

GO

