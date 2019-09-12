CREATE TABLE [dbo].[AspNetRoles] (
    [Id]   NVARCHAR (128) NOT NULL,
    [Name] NVARCHAR (256) NOT NULL,
    [NotificationDescription] NVARCHAR(MAX) NULL, 
    [IsPublic] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);

