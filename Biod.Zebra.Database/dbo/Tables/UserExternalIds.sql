﻿CREATE TABLE [dbo].[UserExternalIds]
(
	[ExternalName]          NVARCHAR (128) NOT NULL,
	[ExternalId]            NVARCHAR (256) NOT NULL,
	[UserId]                NVARCHAR (128) NULL,
	[LastCommunicationDate] DATETIMEOFFSET (7) NOT NULL

	CONSTRAINT [PK_dbo.UserExternalIds] PRIMARY KEY CLUSTERED ([ExternalName], [ExternalId] ASC),
	CONSTRAINT [FK_dbo.UserExternalIds_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
)
