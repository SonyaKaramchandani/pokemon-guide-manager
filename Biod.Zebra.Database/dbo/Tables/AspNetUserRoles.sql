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

CREATE TRIGGER [dbo].[utr_UserRolesTransLog_deleted]
ON [dbo].[AspNetUserRoles]
AFTER DELETE 
AS
Begin
	insert into dbo.UserRolesTransLog(UserId, RoleId, ModifiedUTCDatetime, Description)
	select f1.UserId, f1.RoleId, GETUTCDATE(), 'Deleted ' + f2.[Name]
	from deleted as f1, [dbo].[AspNetRoles] as f2
	where f1.RoleId=f2.Id

-- re-map types for effected users
Update u
set u.userTypeId = isnull((select top 1 UPPER(r.Id) from dbo.AspNetUserRoles ur join dbo.AspNetRoles r on ur.RoleId =r.Id where r.IsPublic =1 and ur.UserId = u.Id), '879C9D59-ADC6-4ACB-9902-3B0B1C1D7146')
From dbo.UserProfile u
inner join deleted d on u.Id = d.UserId

End
GO

-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-10
-- Description:	Tracks the insert on dbo.AspNetUserRoles and save the transctions in dbo.UserRolesTransLog
-- Modified 2020-01: saves old/new values
-- =============================================

CREATE TRIGGER [dbo].[utr_UserRolesTransLog_inserted]
ON [dbo].[AspNetUserRoles]
AFTER INSERT 
AS
Begin
	insert into dbo.UserRolesTransLog(UserId, RoleId, ModifiedUTCDatetime, Description)
	select f1.UserId, f1.RoleId, GETUTCDATE(), 'Inserted new user role ' + f2.[Name]
	from inserted as f1, [dbo].[AspNetRoles] as f2
	where f1.RoleId=f2.Id

-- re-map types for effected users
Update u
set u.userTypeId = isnull((select top 1 UPPER(r.Id) from dbo.AspNetUserRoles ur join dbo.AspNetRoles r on ur.RoleId =r.Id where r.IsPublic =1 and ur.UserId = u.Id), '879C9D59-ADC6-4ACB-9902-3B0B1C1D7146')
From dbo.UserProfile u
inner join inserted i on u.Id = i.UserId

End
GO

-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-10
-- Description:	Tracks the update on dbo.AspNetUserRoles and save the transctions in dbo.UserRolesTransLog
-- Modified 2020-01: saves old/new values
-- =============================================

CREATE TRIGGER [dbo].[utr_UserRolesTransLog_updated]
ON [dbo].[AspNetUserRoles]
AFTER UPDATE 
AS
Begin
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

-- re-map types for effected users
	Update u
	set u.userTypeId = isnull((select top 1 UPPER(r.Id) from dbo.AspNetUserRoles ur join dbo.AspNetRoles r on ur.RoleId =r.Id where r.IsPublic =1 and ur.UserId = u.Id), '879C9D59-ADC6-4ACB-9902-3B0B1C1D7146')
	From dbo.UserProfile u
	inner join inserted i on u.Id = i.UserId

End
