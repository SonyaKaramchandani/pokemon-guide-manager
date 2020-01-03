CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                       NVARCHAR (128) NOT NULL,
    [FirstName]                NVARCHAR (MAX) NULL,
    [LastName]                 NVARCHAR (MAX) NULL,
    [Organization]             NVARCHAR (MAX) NULL,
    [Location]                 NVARCHAR (MAX) NULL,
    [GeonameId]                INT            NOT NULL,
	[AoiGeonameIds]			   VARCHAR(256) NOT NULL DEFAULT '',
    [GridId]                   NVARCHAR (MAX) NULL,
    [SmsNotificationEnabled]   BIT            NOT NULL,
    [NewCaseNotificationEnabled] BIT            NOT NULL DEFAULT 1,
	[NewOutbreakNotificationEnabled] BIT      NOT NULL DEFAULT 1,
	[PeriodicNotificationEnabled] BIT      NOT NULL DEFAULT 1,
	[WeeklyOutbreakNotificationEnabled] BIT      NOT NULL DEFAULT 1,
    [Email]                    NVARCHAR (256) NULL,
    [EmailConfirmed]           BIT            NOT NULL,
    [PasswordHash]             NVARCHAR (MAX) NULL,
    [SecurityStamp]            NVARCHAR (MAX) NULL,
    [PhoneNumber]              NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed]     BIT            NOT NULL,
    [TwoFactorEnabled]         BIT            NOT NULL,
    [LockoutEndDateUtc]        DATETIME       NULL,
    [LockoutEnabled]           BIT            NOT NULL,
    [AccessFailedCount]        INT            NOT NULL,
    [UserName]                 NVARCHAR (256) NOT NULL,
    [UserGroupId]              INT            NULL,
    [DoNotTrackEnabled]        BIT            NOT NULL DEFAULT 0,
	[OnboardingCompleted]      BIT            NOT NULL DEFAULT 0,
    [RefreshToken] VARCHAR(600) NULL, 
    [RefreshTokenCreatedDate] DATETIMEOFFSET NULL, 
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.AspNetUsers_dbo.UserGroup_GroupId] FOREIGN KEY ([UserGroupId]) REFERENCES [dbo].[UserGroup] ([Id])
);


GO

-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-10
-- Description:	Tracks the delete on dbo.AspNetUsers and save the transctions in dbo.UserTransLog
-- =============================================

CREATE TRIGGER dbo.utr_UserTransLog_deleted
ON [dbo].[AspNetUsers]
AFTER DELETE 
AS
	insert into dbo.UserTransLog(UserId, ModifiedUTCDatetime, ModificationDescription)
	select Id, GETUTCDATE(), 'Deleted' from deleted
GO

-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-10
-- Description:	Tracks the insert on dbo.AspNetUsers and save the transctions in dbo.UserTransLog
-- =============================================

CREATE TRIGGER dbo.utr_UserTransLog_inserted
ON [dbo].[AspNetUsers]
AFTER INSERT 
AS
	insert into dbo.UserTransLog(UserId, ModifiedUTCDatetime, ModificationDescription)
	select Id, GETUTCDATE(), 'Inserted new user' from inserted
GO

-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-10
-- Description:	Tracks the update on dbo.AspNetUsers and save the transctions in dbo.UserTransLog
-- =============================================


CREATE TRIGGER utr_UserTransLog_updated
ON [dbo].[AspNetUsers]
AFTER UPDATE 
AS
BEGIN
	Declare @str varchar(max)='Modified:'
		  
	if update(FirstName) Set @str=@str + ' FirstName,' 
	if update(LastName) Set @str=@str + ' LastName,' 
	if update(Organization) Set @str=@str + ' Organization,' 
	if update([Location]) Set @str=@str + ' Location,' 
	if update(GeonameId) Set @str=@str + ' GeonameId,' 
	if update(GridId) Set @str=@str + ' GridId,' 
	if update(SmsNotificationEnabled) Set @str=@str + ' SmsNotificationEnabled,' 
	if update(NewCaseNotificationEnabled) Set @str=@str + ' NewCaseNotificationEnabled,' 
	if update(Email) Set @str=@str + ' Email,' 
	if update(EmailConfirmed) Set @str=@str + ' EmailConfirmed,' 
	if update(PasswordHash) Set @str=@str + ' PasswordHash,' 
	if update(SecurityStamp) Set @str=@str + ' SecurityStamp,' 
	if update(PhoneNumber) Set @str=@str + ' PhoneNumber,' 
	if update(PhoneNumberConfirmed) Set @str=@str + ' PhoneNumberConfirmed,' 
	if update(TwoFactorEnabled) Set @str=@str + ' TwoFactorEnabled,' 
	if update(LockoutEndDateUtc) Set @str=@str + ' LockoutEndDateUtc,' 
	if update(LockoutEnabled) Set @str=@str + ' LockoutEnabled,' 
	if update(AccessFailedCount) Set @str=@str + ' AccessFailedCount,' 
	if update(UserName) Set @str=@str + ' UserName,' 
	if update(UserGroupId) Set @str=@str + ' UserGroupId,' 
	if update(DoNotTrackEnabled) Set @str=@str + ' DoNotTrackEnabled,' 
	--remove last ,
	Set @str=LEFT(@str, len(@str)-1)

	insert into dbo.UserTransLog(UserId, ModifiedUTCDatetime, ModificationDescription)
	select Id, GETUTCDATE(), @str
	from inserted
END