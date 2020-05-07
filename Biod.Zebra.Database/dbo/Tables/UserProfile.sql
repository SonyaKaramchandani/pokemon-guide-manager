CREATE TABLE [dbo].[UserProfile](
	[Id] [nvarchar](128) NOT NULL,
	[FirstName] NVARCHAR (256) NULL,
    [LastName]  NVARCHAR (256) NULL,
	[Email] NVARCHAR (256) NULL,
	[Location] [nvarchar](max) NULL,
	[GeonameId] [int] NULL,
	[AoiGeonameIds] [nvarchar](max) NULL DEFAULT '',
	[GridId] [nvarchar](50) NULL,
	[UserTypeId] UNIQUEIDENTIFIER NOT NULL,
	[SmsNotificationEnabled] BIT NOT NULL,
	[NewCaseNotificationEnabled] [bit] NOT NULL DEFAULT 1,
	[NewOutbreakNotificationEnabled] [bit] NOT NULL DEFAULT 1,
	[PeriodicNotificationEnabled] [bit] NOT NULL DEFAULT 1,
	[WeeklyOutbreakNotificationEnabled] [bit] NOT NULL DEFAULT 1,
	[DoNotTrackEnabled] [bit] NOT NULL DEFAULT 0,
	[OnboardingCompleted] [bit] NOT NULL DEFAULT 0
	CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_UserProfile_UserTypes] FOREIGN KEY ([UserTypeId]) REFERENCES [dbo].[UserTypes] ([Id])
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
