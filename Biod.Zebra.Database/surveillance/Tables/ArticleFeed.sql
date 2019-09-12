CREATE TABLE [surveillance].[ArticleFeed] (
    [ArticleFeedId]   INT          NOT NULL,
    [ArticleFeedName] VARCHAR (50) NULL,
	DisplayName varchar(50) NULL,
	SeqId int NULL,
	FullName varchar(500) NULL,
    CONSTRAINT [PK_ArticleFeed] PRIMARY KEY CLUSTERED ([ArticleFeedId] ASC)
);

