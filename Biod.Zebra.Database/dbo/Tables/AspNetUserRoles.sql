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
-- =============================================

CREATE TRIGGER dbo.utr_UserRolesTransLog_deleted
ON [dbo].AspNetUserRoles
AFTER DELETE 
AS
	insert into dbo.UserRolesTransLog(UserId, RoleId, ModifiedUTCDatetime, Description)
	select UserId, RoleId, GETUTCDATE(), 'Deleted' from deleted
GO

-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-10
-- Description:	Tracks the insert on dbo.AspNetUserRoles and save the transctions in dbo.UserRolesTransLog
-- =============================================

CREATE TRIGGER dbo.utr_UserRolesTransLog_inserted
ON [dbo].AspNetUserRoles
AFTER INSERT 
AS
	insert into dbo.UserRolesTransLog(UserId, RoleId, ModifiedUTCDatetime, Description)
	select UserId, RoleId, GETUTCDATE(), 'Inserted new user role' from inserted
GO

-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-10
-- Description:	Tracks the update on dbo.AspNetUserRoles and save the transctions in dbo.UserRolesTransLog
-- =============================================

CREATE TRIGGER dbo.utr_UserRolesTransLog_updated
ON [dbo].AspNetUserRoles
AFTER UPDATE 
AS
	if update(UserId)
		insert into dbo.UserRolesTransLog(UserId, RoleId, ModifiedUTCDatetime, Description)
		select UserId, RoleId, GETUTCDATE(), 'Inserted new user role' from inserted
	else if update(RoleId)
		insert into dbo.UserRolesTransLog(UserId, RoleId, ModifiedUTCDatetime, Description)
		select UserId, RoleId, GETUTCDATE(), 'Role modified' from inserted