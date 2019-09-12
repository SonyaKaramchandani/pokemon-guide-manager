CREATE TABLE [dbo].[UserLoginTrans] (
	[UserLoginTransID] [int] IDENTITY(1,1) NOT NULL,
	[UserId] NVARCHAR (128) NOT NULL,
	[LoginDateTime] [datetime] NOT NULL DEFAULT (getutcdate()),
    CONSTRAINT [PK_dbo.UserLoginTrans] PRIMARY KEY CLUSTERED ([UserLoginTransID] ASC),
    CONSTRAINT [FK_UserLoginTrans_AspNetUsers] FOREIGN KEY ([UserId]) 
		REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
);

GO
CREATE NONCLUSTERED INDEX [idx_UserId]
    ON [dbo].[UserLoginTrans]([UserId] ASC);
