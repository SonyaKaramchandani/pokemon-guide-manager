CREATE TABLE [surveillance].[Event] (
    [EventId]               INT           NOT NULL,
    [EventTitle]            VARCHAR (200) NOT NULL,
    [StartDate]             DATE          NULL,
    [EndDate]               DATE          NULL,
    [LastUpdatedDate]       DATETIME      NULL,
    [PriorityId]            INT           NULL,
    [IsPublished]           BIT           NULL,
    [Summary]               VARCHAR (MAX) NULL,
    [Notes]                 VARCHAR (MAX) NULL,
    [DiseaseId]             INT           NULL,
    [CreatedDate]           DATETIME      NULL,
    [EventMongoId]          VARCHAR (128) NULL,
    [LastUpdatedByUserName] VARCHAR (64)  NULL,
	IsLocalOnly bit NOT NULL Default (0),
    [HasOutlookReport] BIT   , 
    PRIMARY KEY CLUSTERED ([EventId] ASC),
    CONSTRAINT [FK_Event_PriorityId] FOREIGN KEY ([PriorityId]) REFERENCES [surveillance].[EventPriorities] ([PriorityId])
);

