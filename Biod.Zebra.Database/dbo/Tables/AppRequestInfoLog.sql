CREATE TABLE [dbo].[AppRequestInfoLog] (
	[LogTransId] [int] IDENTITY(1,1) NOT NULL,
	[RequestIPAddress] NVARCHAR (MAX) NOT NULL,
	[IsPrivateIpAddress] BIT NOT NULL,
	[LogDateTime] [datetime] NOT NULL DEFAULT (getutcdate())
);
GO

