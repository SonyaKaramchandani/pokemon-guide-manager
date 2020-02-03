CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] NVARCHAR (128) NOT NULL,
    [RoleId] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO

-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-10
-- Description:	Tracks the delete on dbo.AspNetUserRoles and save the transctions in dbo.UserRolesTransLog
-- Modified 2020-01: saves old/new values
-- =============================================

CREATE TRIGGER dbo.utr_UserRolesTransLog_deleted
ON [dbo].AspNetUserRoles
AFTER DELETE 
AS
	insert into dbo.UserRolesTransLog(UserId, RoleId, ModifiedUTCDatetime, Description)
	select f1.UserId, f1.RoleId, GETUTCDATE(), 'Deleted ' + f2.[Name]
	from deleted as f1, [dbo].[AspNetRoles] as f2
	where f1.RoleId=f2.Id
GO

-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-10
-- Description:	Tracks the insert on dbo.AspNetUserRoles and save the transctions in dbo.UserRolesTransLog
-- Modified 2020-01: saves old/new values
-- =============================================

CREATE TRIGGER dbo.utr_UserRolesTransLog_inserted
ON [dbo].AspNetUserRoles
AFTER INSERT 
AS
	insert into dbo.UserRolesTransLog(UserId, RoleId, ModifiedUTCDatetime, Description)
	select f1.UserId, f1.RoleId, GETUTCDATE(), 'Inserted new user role ' + f2.[Name]
	from inserted as f1, [dbo].[AspNetRoles] as f2
	where f1.RoleId=f2.Id
GO

-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-10
-- Description:	Tracks the update on dbo.AspNetUserRoles and save the transctions in dbo.UserRolesTransLog
-- Modified 2020-01: saves old/new values
-- =============================================

CREATE TRIGGER dbo.utr_UserRolesTransLog_updated
ON [dbo].AspNetUserRoles
AFTER UPDATE 
AS
	--new
	insert into dbo.UserRolesTransLog(UserId, RoleId, ModifiedUTCDatetime, Description)
	select f1.UserId, f1.RoleId, GETUTCDATE(), 'Updated new user role ' + f2.[Name]
	from inserted as f1, [dbo].[AspNetRoles] as f2
	where f1.RoleId=f2.Id
	--old
	insert into dbo.UserRolesTransLog(UserId, RoleId, ModifiedUTCDatetime, Description)
	select f1.UserId, f1.RoleId, GETUTCDATE(), 'Updated old user role ' + f2.[Name]
	from deleted as f1, [dbo].[AspNetRoles] as f2
	where f1.RoleId=f2.Id
