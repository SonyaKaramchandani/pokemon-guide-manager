CREATE TABLE zebra.UserAois_history(
	UserId nvarchar(128) NOT NULL,
	AoiGeonameIds varchar(max) NOT NULL,
CONSTRAINT PK_UserAois_history PRIMARY KEY CLUSTERED (UserId),
CONSTRAINT [FK_UserAois_history_UserId] FOREIGN KEY (UserId) REFERENCES [dbo].[UserProfile](Id) ON DELETE cascade
	);

