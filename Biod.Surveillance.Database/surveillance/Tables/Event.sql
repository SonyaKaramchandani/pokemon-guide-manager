CREATE TABLE [surveillance].[Event] (
    [EventId]         INT           IDENTITY (1, 1) NOT NULL,
    [EventTitle]      VARCHAR (200) NOT NULL,
    [StartDate]       DATE          NULL,
    [EndDate]         DATE          NULL,
    [LastUpdatedDate] DATETIME      NULL,
    [PriorityId]      INT           NULL,
    [IsPublished]     BIT           NULL,
	[IsPublishedChangesToApi]     BIT           NULL,
    [Summary]         VARCHAR (MAX) NULL,
    [Notes]           VARCHAR (MAX) NULL,
    [DiseaseId]       INT           NULL,
    [CreatedDate]     DATETIME      NULL,
	EventMongoId      VARCHAR(128)  NULL,
	LastUpdatedByUserName varchar(64),
	IsLocalOnly bit NOT NULL Default (0),
	SpeciesId INT NOT NULL Default (1),
    PRIMARY KEY CLUSTERED ([EventId] ASC),
    CONSTRAINT FK_Event_PriorityId FOREIGN KEY (PriorityId) REFERENCES [surveillance].[EventPriorities](PriorityId), 
    CONSTRAINT FK_Event_SpeciesId FOREIGN KEY (SpeciesId) REFERENCES disease.Species(SpeciesId)
);

