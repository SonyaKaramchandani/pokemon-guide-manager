CREATE TABLE [surveillance].[EventTransLog] (
	[ModifiedDate]             DATETIMEOFFSET  NOT NULL,
	[Action]                   NVARCHAR(128)   NOT NULL,
	[EventId]                  INT             NOT NULL,
	[EventTitle]               NVARCHAR(200)   NULL,
	[StartDate]                DATE            NULL,
	[EndDate]                  DATE            NULL,
	[LastUpdatedDate]          DATETIME        NULL,
	[PriorityId]               INT             NULL,
	[IsPublished]              BIT             NULL,
	[Summary]                  NVARCHAR(max)   NULL,
	[Notes]                    NVARCHAR(max)   NULL,
	[DiseaseId]                INT             NULL,
	[CreatedDate]              DATETIME        NULL,
	[EventMongoId]             NVARCHAR(128)   NULL,
	[LastUpdatedByUserName]    NVARCHAR(64)    NULL,
	[IsLocalOnly]              BIT             NULL,
	[SpeciesId]                INT             NULL,
  CONSTRAINT [PK_surveillance.EventTransLog] PRIMARY KEY CLUSTERED (
    [EventId],
	  [ModifiedDate]
  )
);
GO
