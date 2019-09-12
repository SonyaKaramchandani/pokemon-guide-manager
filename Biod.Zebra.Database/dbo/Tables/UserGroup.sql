CREATE TABLE [dbo].[UserGroup] (
    [Id]   INT            IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.UserGroup] PRIMARY KEY CLUSTERED ([Id] ASC)
);

