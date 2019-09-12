CREATE TABLE [bd].[LastJsonStrs] (
    [Id]              INT            NOT NULL,
    [Description]     VARCHAR (100)  NULL,
    [JsonStr]         NVARCHAR (MAX) NOT NULL,
    [LastUpdatedDate] DATETIME       NULL,
    CONSTRAINT [PK_LastJsonStrs] PRIMARY KEY CLUSTERED ([Id] ASC)
);

