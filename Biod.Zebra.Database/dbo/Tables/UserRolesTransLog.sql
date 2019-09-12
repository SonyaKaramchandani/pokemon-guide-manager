CREATE TABLE [dbo].[UserRolesTransLog] (
    [UserId]              NVARCHAR (128) NOT NULL,
    [RoleId]              NVARCHAR (128) NOT NULL,
    [ModifiedUTCDatetime] DATETIME       NOT NULL,
    [Description]         NVARCHAR (200) NULL,
    CONSTRAINT [PK_UserRolesTransLog] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC, [ModifiedUTCDatetime] ASC)
);

