CREATE TABLE [dbo].[UserTypes]
(
  [Id]                      UNIQUEIDENTIFIER  NOT NULL, 
  [Name]                    NVARCHAR(256)     NOT NULL, 
  [NotificationDescription] NVARCHAR(MAX)     NULL,
  CONSTRAINT [PK_UserTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE UNIQUE INDEX [IX_UserTypes_Name] ON [dbo].[UserTypes] ([Name])
GO
