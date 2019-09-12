CREATE TABLE [dbo].[UserTransLog] (
    [UserId]                  NVARCHAR (128) NOT NULL,
    [ModifiedUTCDatetime]     DATETIME       NOT NULL,
    [ModificationDescription] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_UserTransLog] PRIMARY KEY CLUSTERED ([UserId] ASC, [ModifiedUTCDatetime] ASC)
);

