CREATE TABLE [dbo].[ActivityLog] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [Date]            DATETIME       NOT NULL,
    [Thread]          VARCHAR (255)  NOT NULL,
    [Level]           VARCHAR (50)   NOT NULL,
    [Logger]          VARCHAR (255)  NOT NULL,
    [Message]         NVARCHAR (MAX) NOT NULL,
    [Exception]       VARCHAR (MAX)  NULL,
    [UserName]        NVARCHAR (128) NULL,
    [HostAddress]     VARCHAR (255)  NULL,
    [Browser]         VARCHAR (255)  NULL,
    [ServerName]      VARCHAR (255)  NULL,
    [Url]             VARCHAR (2048) NULL,
    [ApplicationName] VARCHAR (50)   NULL,
    CONSTRAINT [PK_ActivityLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [idx_ActivityLog_Date]
    ON [dbo].[ActivityLog]([Date] ASC);