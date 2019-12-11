CREATE TABLE [surveillance].[EventTransLog] (
  [LogId]                    INT IDENTITY(1,1) NOT NULL,
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
  PRIMARY KEY CLUSTERED ([LogId])
);
GO
