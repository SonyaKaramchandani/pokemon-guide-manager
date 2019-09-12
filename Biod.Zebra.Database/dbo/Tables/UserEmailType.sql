CREATE TABLE [dbo].[UserEmailType] (
    [Id]   INT            NOT NULL,
    [Type] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.UserEmailType] PRIMARY KEY CLUSTERED ([Id] ASC)
);