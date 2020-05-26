CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                                NVARCHAR (128)     NOT NULL,
    [FirstName]                         NVARCHAR (MAX)     NULL,
    [LastName]                          NVARCHAR (MAX)     NULL,
    [Organization]                      NVARCHAR (MAX)     NULL,
    [Location]                          NVARCHAR (MAX)     NULL,
    [GeonameId]                         INT                NOT NULL,
    [AoiGeonameIds]                     NVARCHAR (MAX)     DEFAULT ('') NOT NULL,
    [GridId]                            NVARCHAR (MAX)     NULL,
    [SmsNotificationEnabled]            BIT                NOT NULL,
    [NewCaseNotificationEnabled]        BIT                DEFAULT ((1)) NOT NULL,
    [NewOutbreakNotificationEnabled]    BIT                DEFAULT ((1)) NOT NULL,
    [PeriodicNotificationEnabled]       BIT                DEFAULT ((1)) NOT NULL,
    [WeeklyOutbreakNotificationEnabled] BIT                DEFAULT ((1)) NOT NULL,
    [Email]                             NVARCHAR (256)     NULL,
    [EmailConfirmed]                    BIT                NOT NULL,
    [PasswordHash]                      NVARCHAR (MAX)     NULL,
    [SecurityStamp]                     NVARCHAR (MAX)     NULL,
    [PhoneNumber]                       NVARCHAR (MAX)     NULL,
    [PhoneNumberConfirmed]              BIT                NOT NULL,
    [TwoFactorEnabled]                  BIT                NOT NULL,
    [LockoutEndDateUtc]                 DATETIME           NULL,
    [LockoutEnabled]                    BIT                NOT NULL,
    [AccessFailedCount]                 INT                NOT NULL,
    [UserName]                          NVARCHAR (256)     NOT NULL,
    [UserGroupId]                       INT                NULL,
    [DoNotTrackEnabled]                 BIT                DEFAULT ((0)) NOT NULL,
    [OnboardingCompleted]               BIT                DEFAULT ((0)) NOT NULL,
    [RefreshToken]                      VARCHAR (600)      NULL,
    [RefreshTokenCreatedDate]           DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUsers_dbo.UserGroup_GroupId] FOREIGN KEY ([UserGroupId]) REFERENCES [dbo].[UserGroup] ([Id])
);






GO

-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-10
-- Description:	Tracks the delete on dbo.AspNetUsers and save the transctions in dbo.UserTransLog
-- =============================================

CREATE TRIGGER [dbo].[utr_UserTransLog_deleted]
ON [dbo].[AspNetUsers]
AFTER DELETE 
AS
Begin
	insert into dbo.UserTransLog(UserId, ModifiedUTCDatetime, ModificationDescription)
	select Id, GETUTCDATE(), 'Deleted' from deleted

	-- user profile
	DELETE p
	FROM dbo.UserProfile p
	INNER JOIN deleted d on d.id = p.Id
End
GO

-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-10
-- Description:	Tracks the insert on dbo.AspNetUsers and save the transctions in dbo.UserTransLog
-- =============================================

CREATE TRIGGER [dbo].[utr_UserTransLog_inserted]
ON [dbo].[AspNetUsers]
AFTER INSERT 
AS
Begin
--no count
	insert into dbo.UserTransLog(UserId, ModifiedUTCDatetime, ModificationDescription)
	select Id, GETUTCDATE(), 'Inserted new user' from inserted

--user profile

INSERT INTO [dbo].[UserProfile]
           ([Id]
           ,[FirstName]
           ,[LastName]
           ,[Email]
           ,[Location]
           ,[GeonameId]
           ,[AoiGeonameIds]
           ,[GridId]
		   ,[UserTypeId]
           ,[SmsNotificationEnabled]
           ,[NewCaseNotificationEnabled]
           ,[NewOutbreakNotificationEnabled]
           ,[PeriodicNotificationEnabled]
           ,[WeeklyOutbreakNotificationEnabled]
           ,[DoNotTrackEnabled]
           ,[OnboardingCompleted])
SELECT [Id]
           ,[FirstName]
           ,[LastName]
           ,[Email]
           ,[Location]
           ,[GeonameId]
           ,[AoiGeonameIds]
           ,[GridId]
		   ,'879C9D59-ADC6-4ACB-9902-3B0B1C1D7146'--default to other
           ,[SmsNotificationEnabled]
           ,[NewCaseNotificationEnabled]
           ,[NewOutbreakNotificationEnabled]
           ,[PeriodicNotificationEnabled]
           ,[WeeklyOutbreakNotificationEnabled]
           ,[DoNotTrackEnabled]
           ,[OnboardingCompleted]
		   FROM inserted
End
GO

-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-10
-- Description:	Tracks the update on dbo.AspNetUsers and save the transctions in dbo.UserTransLog
-- =============================================


CREATE TRIGGER [dbo].[utr_UserTransLog_updated]
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

	--user profile
	UPDATE P
	SET
	p.[FirstName] = u.[FirstName]
           ,p.[LastName] = u.[LastName]
           ,p.[Email] = u.[Email]
           ,p.[Location] = u.[Location]
		   ,p.[GeonameId] = u.[GeonameId]
           ,p.[AoiGeonameIds] = u.[AoiGeonameIds]
           ,p.[GridId] = u.[GridId]
           ,p.[SmsNotificationEnabled] = u.[SmsNotificationEnabled]
           ,p.[NewCaseNotificationEnabled] = u.[NewCaseNotificationEnabled]
           ,p.[NewOutbreakNotificationEnabled] = u.[NewOutbreakNotificationEnabled]
           ,p.[PeriodicNotificationEnabled] = u.[PeriodicNotificationEnabled]
           ,p.[WeeklyOutbreakNotificationEnabled] = u.[WeeklyOutbreakNotificationEnabled]
           ,p.[DoNotTrackEnabled] = u.[DoNotTrackEnabled]
           ,p.[OnboardingCompleted] = u.[OnboardingCompleted]
		 FROM dbo.UserProfile p
		 INNER JOIN inserted u on u.id = p.Id
END