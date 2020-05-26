CREATE TABLE [dbo].[UserEmailNotification]
(
	[Id]            INT             IDENTITY(1,1) NOT NULL,
	[UserId]        NVARCHAR(128)   NOT NULL,
	[AoiGeonameIds]	VARCHAR(MAX)    NOT NULL DEFAULT '',
	[UserEmail]     NVARCHAR(256)   NOT NULL,
	[EmailType]     INT             NOT NULL,
	[EventId]       INT             NULL,
	[Content]       NVARCHAR(MAX)   NOT NULL,
	[SentDate]      DATETIMEOFFSET  NOT NULL,
	[Title]         NVARCHAR(256)   NOT NULL DEFAULT '',
	[Summary]       NVARCHAR(MAX)   NULL,

	CONSTRAINT [PK_dbo.UserEmailNotification] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.UserEmailNotification_UserProfile_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserProfile] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.UserEmailNotification_dbo.UserEmailType_EmailTypeId] FOREIGN KEY ([EmailType]) REFERENCES [dbo].[UserEmailType] ([Id]),
	CONSTRAINT [FK_dbo.UserEmailNotification_surveillance.Event_EventId] FOREIGN KEY ([EventId]) REFERENCES [surveillance].[Event] ([EventId]) ON DELETE CASCADE
)
